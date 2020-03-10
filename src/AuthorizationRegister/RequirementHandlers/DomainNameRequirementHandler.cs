using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationRegister.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationRegister.RequirementHandlers
{
    internal class DomainNameRequirementHandler : AuthorizationHandler<DomainNameRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DomainNameRequirement requirement)
        {
            var userEmail = context.User?.FindFirst(ClaimTypes.Email).Value;

            if (userEmail.EndsWith(requirement.DomainName))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
