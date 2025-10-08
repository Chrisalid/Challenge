using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Interfaces.Services;

public interface IProductService
{
    Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest, long loggedUserId);
    Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest updateProductRequest, long loggedUserId);
    Task DeleteProduct(long id, long loggedUserId);
    Task<GetProductResponse> GetProductByIdAsync(long productId, long loggedUserId);
    Task<IEnumerable<GetProductResponse>> GetProductsAsync(long loggedUserId);
}
