using Challenge.Domain.Enums;

namespace Challenge.Application.DTOs.Response;

public class UpdateOrderResponse
{
    public long OrderId { get; set; }
    public string OrderName { get; set; }
    public OrderStatus Status { get; set; }
}
