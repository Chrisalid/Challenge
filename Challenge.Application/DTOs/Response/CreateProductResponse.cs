namespace Challenge.Application.DTOs.Response;

public class CreateProductResponse
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
}
