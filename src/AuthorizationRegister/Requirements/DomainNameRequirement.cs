using Microsoft.AspNetCore.Authorization;

namespace AuthorizationRegister.Requirements
{
    internal class DomainNameRequirement : IAuthorizationRequirement
    {
        public string DomainName { get; }

        public DomainNameRequirement(string domainName) => DomainName = domainName;
    }
}
