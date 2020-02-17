using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.AppSettings;
using IdentityServer.Data;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserDbContext _userDbContex;

        public IdentityService(
            UserManager<IdentityUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            UserDbContext userDbContex)
            => (_userManager, _jwtSettings, _tokenValidationParameters, _userDbContex) = (userManager, jwtSettings, tokenValidationParameters, userDbContex);

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exists" }
                };

            var isAuthenticUser = await _userManager.CheckPasswordAsync(existingUser, password);

            if (!isAuthenticUser)
                return new AuthenticationResult
                {
                    Errors = new[] { "User/Password combination is incorrect" }
                };

            return await GetUserAuthenticationInfoAsync(existingUser);
        }

        public async Task<AuthenticationResult> RefreshAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid token" }
                };

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };

            var storedRefreshToken = await _userDbContex.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "This token does not exist" } };

            if (storedRefreshToken.ExpiryDate > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This refresh token has not expired yet" } };

            if (storedRefreshToken.IsInvalidated)
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };

            if (storedRefreshToken.IsUsed)
                return new AuthenticationResult { Errors = new[] { "This refresh token is already been used" } };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedRefreshToken.JwtId == jti)
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match the jti" } };

            storedRefreshToken.IsUsed = true;
            _userDbContex.RefreshTokens.Update(storedRefreshToken);
            await _userDbContex.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await GetUserAuthenticationInfoAsync(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(string name, string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User already exists" }
                };

            var newUser = new IdentityUser
            {
                UserName = name,
                Email = email
            };
            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };

            return await GetUserAuthenticationInfoAsync(newUser);
        }

        private async Task<AuthenticationResult> GetUserAuthenticationInfoAsync(IdentityUser newUser)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, newUser.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHanlder = new JwtSecurityTokenHandler();
            var token = tokenHanlder.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                Token = GetRefreshToken(),
                JwtId = token.Id,
                UserId = newUser.Id,
                ExpiryDate = DateTime.UtcNow,
                CreationDate = DateTime.UtcNow.AddDays(10)
            };

            await _userDbContex.RefreshTokens.AddAsync(refreshToken);
            await _userDbContex.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHanlder.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityToken(validatedToken))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityToken(SecurityToken validatedToken)
            => validatedToken is JwtSecurityToken jwtSecutiryToken && jwtSecutiryToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCulture);

        private string GetRefreshToken()
        {
            var refreshTokenBytes = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

            return Convert.ToBase64String(refreshTokenBytes);
        }
    }
}
