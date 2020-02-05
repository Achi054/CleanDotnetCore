using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OAuthServer.Constants;

namespace OAuthServer
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type,
            string client_id,
            string redirect_uri,
            string scope,
            string state)
        {
            var query = new QueryBuilder();
            query.Add("redirect_uri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(
            string username,
            string password,
            string redirect_uri,
            string state)
        {
            var query = new QueryBuilder();
            query.Add("code", username);
            query.Add("state", state);

            return Redirect($"{redirect_uri}{query.ToString()}");
        }

        public async Task<IActionResult> Token(
            string grant_type,
            string code,
            string redirect_uri,
            string client_id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "054"),
                new Claim(JwtRegisteredClaimNames.UniqueName, "Sujith Acharya"),
            };

            var securitySignature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.SecretKey));
            var signedCredential = new SigningCredentials(securitySignature, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                TokenConstants.Issuer,
                TokenConstants.Audience,
                claims, DateTime.Now,
                DateTime.Now.AddHours(1),
                signedCredential);

            var jsonToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var responseObject = new
            {
                access_token = jsonToken,
                token_type = "Bearer",
                raw_claim = "OAuth Application"
            };

            var serializedJsonObject = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseObject));

            await Response.Body.WriteAsync(serializedJsonObject, 0, serializedJsonObject.Length);

            return Redirect(redirect_uri);
        }
    }
}
