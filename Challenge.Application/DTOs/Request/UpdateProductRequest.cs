namespace Challenge.Application.DTOs.Request;

public class UpdateProductRequest
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public int Inventory { get; set; }
}
