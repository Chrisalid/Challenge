namespace Challenge.Application.DTOs.Request;

public class CreateOrderItemRequest
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
}
