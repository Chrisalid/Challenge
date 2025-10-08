namespace Challenge.Application.DTOs.Request;

public class UpdateOrderRequest
{
    public long OrderId { get; set; }
    public bool Paid { get; set; }
}
