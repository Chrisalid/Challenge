namespace Challenge.Application.DTOs.Response;

public class GetOrderItemResponse
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal IndividualAmount { get; set; }
}