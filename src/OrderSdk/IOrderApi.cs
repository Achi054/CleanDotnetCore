using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Contracts.V1;
using OrderApi.Models;
using Refit;

namespace OrderSdk
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/" + ApiRoutes.Order.Get)]
        Task<ApiResponse<IEnumerable<OrderDetails>>> Get();

        [Post("/" + ApiRoutes.Order.GetById)]
        Task<ApiResponse<OrderDetails>> GetById(int id);

        [Post("/" + ApiRoutes.Order.Create)]
        Task<ApiResponse<OrderDetails>> Create([Body]OrderDetails orderDetails);

        [Put("/" + ApiRoutes.Order.Update)]
        Task<ApiResponse<OrderDetails>> Update(int id, [Body]OrderDetails orderDetails);

        [Delete("/" + ApiRoutes.Order.Delete)]
        Task<ApiResponse<OrderDetails>> Delete(int id);
    }
}
