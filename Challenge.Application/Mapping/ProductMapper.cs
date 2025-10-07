using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Mapping;

public class ProductMapper
{
    public CreateProductResponse Map(Product product)
    {
        return new()
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Value = product.Value
        };
    }
}
