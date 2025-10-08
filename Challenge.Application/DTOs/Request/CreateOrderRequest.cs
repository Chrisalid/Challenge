namespace Challenge.Application.DTOs.Request;

public class CreateOrderRequest
{
    public long ClientId { get; set; }
    public string Name { get; set; }
    public List<CreateOrderItemRequest> OrderItems { get; set; }
}
