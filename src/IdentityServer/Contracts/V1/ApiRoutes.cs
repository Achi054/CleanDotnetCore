namespace IdentityServer.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public class Identity
        {
            public const string IdentityBase = Base + "/identity";

            public const string Login = IdentityBase + "/login";
            public const string Register = IdentityBase + "/register";
            public const string Refresh = IdentityBase + "/refresh";
        }
    }
}
