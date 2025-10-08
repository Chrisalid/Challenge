using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Mapping;

public class OrderMapper
{
    public CreateOrderResponse CreateMap(Order order)
    {
        return new()
        {
            OrderId = order.Id,
            OrderName = order.Name
        };
    }

    public UpdateOrderResponse UpdateMap(Order order)
    {
        return new()
        {
            OrderId = order.Id,
            OrderName = order.Name,
            Status = order.Status
        };
    }

    public GetOrderResponse GetMap(Order order)
    {
        return new()
        {
            OrderId = order.Id,
            Name = order.Name,
            OrderItems = order.OrderItems.Select(oi => new GetOrderItemResponse
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                IndividualAmount = oi.IndividualAmount
            })
        };
    }

    public IEnumerable<GetOrderResponse> GetListMap(IEnumerable<Order> order)
    {
        return order.Select(o => new GetOrderResponse
        {
            OrderId = o.Id,
            Name = o.Name,
            OrderItems = o.OrderItems.Select(oi => new GetOrderItemResponse
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                IndividualAmount = oi.IndividualAmount
            })
        });
    }
}
