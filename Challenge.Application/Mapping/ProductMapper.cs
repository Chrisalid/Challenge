using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Mapping;

public class ProductMapper
{
    public CreateProductResponse CreateMap(Product product)
    {
        return new()
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Value = product.Value,
            Inventory = product.Inventory
        };
    }

    public UpdateProductResponse UpdateMap(Product product)
    {
        return new()
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Value = product.Value,
            Inventory = product.Inventory
        };
    }

    public GetProductResponse GetMap(Product product)
    {
        return new()
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Value = product.Value,
            Inventory = product.Inventory
        };
    }

    public IEnumerable<GetProductResponse> GetListMap(IEnumerable<Product> products)
    {
        return products.Select(p => new GetProductResponse
        {
            ProductId = p.Id,
            Name = p.Name,
            Description = p.Description,
            Value = p.Value,
            Inventory = p.Inventory
        });
    }
}
