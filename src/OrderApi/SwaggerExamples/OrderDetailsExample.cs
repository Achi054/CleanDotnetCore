using OrderApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace OrderApi.SwaggerExamples
{
    public class OrderDetailsExample : IExamplesProvider<OrderDetails>
    {
        public OrderDetails GetExamples()
        {
            return new OrderDetails
            {
                Id = 1
            };
        }
    }
}
