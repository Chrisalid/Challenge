using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Mapping;
using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Challenge.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Challenge.Application.Services;

public class ProductService(IUnitOfWorkRepository unitOfWorkRepository) : IProductService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
    private readonly ProductMapper _mapper = new();

    public async Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest, long loggedUserId)
    {
        var productRepository = _unitOfWorkRepository.Products;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var productModel = new Product.ProductModel(
            createProductRequest.Name,
            createProductRequest.Description,
            createProductRequest.Value,
            createProductRequest.Inventory,
            loggedUser.Id
        );

        await _unitOfWorkRepository.Begin();
        try
        {
            var product = Product.Create(productModel);

            product.Id = await productRepository.CreateAsync(product);
            await _unitOfWorkRepository.Commit();

            return _mapper.CreateMap(product);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest updateProductRequest, long loggedUserId)
    {
        var productRepository = _unitOfWorkRepository.Products;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var productToUpdate = await productRepository.GetById(updateProductRequest.ProductId);

        await _unitOfWorkRepository.Begin();
        try
        {
            bool isChange = false;

            if (!string.IsNullOrWhiteSpace(updateProductRequest.Name) && !string.Equals(updateProductRequest.Name, productToUpdate.Name))
            {
                productToUpdate.SetName(updateProductRequest.Name);
                isChange = true;
            }

            if (!string.IsNullOrWhiteSpace(updateProductRequest.Description) && !string.Equals(updateProductRequest.Description, productToUpdate.Description))
            {
                productToUpdate.SetDescription(updateProductRequest.Description);
                isChange = true;
            }

            if (productToUpdate.Value != updateProductRequest.Value)
            {
                productToUpdate.SetValue(updateProductRequest.Value);
                isChange = true;
            }

            if (productToUpdate.Inventory != updateProductRequest.Inventory)
            {
                productToUpdate.SetInventory(updateProductRequest.Inventory);
                isChange = true;
            }

            if (isChange)
            {
                productToUpdate.SetUpdatedBy(loggedUser.Id);
                productToUpdate.SetUpdated(DateTime.UtcNow);

                await productRepository.UpdateAsync(productToUpdate);
            }

            await _unitOfWorkRepository.Commit();

            return _mapper.UpdateMap(productToUpdate);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task DeleteProduct(long id, long loggedUserId)
    {
        var productRepository = _unitOfWorkRepository.Products;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var productToUpdate = await productRepository.GetById(id);

        await _unitOfWorkRepository.Begin();
        try
        {
            productToUpdate.SetDeleted(DateTime.UtcNow);
            productToUpdate.SetUpdated(DateTime.UtcNow);
            productToUpdate.SetUpdatedBy(loggedUser.Id);

            await productRepository.DeleteAsync(productToUpdate);
            await _unitOfWorkRepository.Commit();
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<GetProductResponse> GetProductByIdAsync(long productId, long loggedUserId)
    {
        var productRepository = _unitOfWorkRepository.Products;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var product = await productRepository.GetById(productId);

        return _mapper.GetMap(product);
    }

    public async Task<IEnumerable<GetProductResponse>> GetProductsAsync(long loggedUserId)
    {
        var productRepository = _unitOfWorkRepository.Products;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var products = await productRepository.GetList();

        return _mapper.GetListMap(products);
    }
}
