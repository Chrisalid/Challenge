using Challenge.Domain.Entities;

namespace Challenge.Application.Interfaces.Services;

public interface IProductService
{
    Task<Product> CreateProduct();
    Task<Product> GetProductByIdAsync(long productId);
    Task<IEnumerable<Product>> GetProductsAsync();
}
