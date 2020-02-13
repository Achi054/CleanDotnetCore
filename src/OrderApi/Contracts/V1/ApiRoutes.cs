namespace OrderApi.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public class Order
        {
            public const string OrderBase = Base + "/order";

            public const string Get = OrderBase;
            public const string GetById = OrderBase + "/{id}";
            public const string Create = OrderBase + "/create";
            public const string Update = OrderBase + "/{id}";
            public const string Delete = OrderBase + "/{id}";
        }
    }
}
