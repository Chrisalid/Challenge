namespace Challenge.Application.DTOs.Response;

public class GetOrderResponse
{
    public long OrderId { get; set; }
    public string Name { get; set; }
    public IEnumerable<GetOrderItemResponse> OrderItems { get; set; }
}