namespace Challenge.Application.DTOs.Request;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
}
