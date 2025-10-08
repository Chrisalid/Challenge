using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Interfaces.Services;

public interface IOrderService
{
    Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, long loggedUserId);
    Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, long loggedUserId);
    Task DeleteOrder(long id, long loggedUserId);
    Task<GetOrderResponse> GetOrderByIdAsync(long orderId, long loggedUserId);
    Task<IEnumerable<GetOrderResponse>> GetOrdersAsync(long loggedUserId);
}
