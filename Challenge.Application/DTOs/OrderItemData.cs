using Challenge.Domain.Entities;

namespace Challenge.Application.DTOs;

public class OrderItemData
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
