using System;
using Challenge.Api.Controllers.Base;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers;

[ApiController]
public class ProductController(IProductService userService) : BaseController
{
    private readonly IProductService _userService = userService;

    [HttpPost]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductRequest createProductRequest)
    {
        try
        {
            var createProductResponse = await _userService.CreateProduct(createProductRequest, GetCurrentUserId());

            return createProductResponse is not null
                ? Ok(createProductResponse)
                : NotFound(new { Message = "Creation Product Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(UpdateProductRequest updateProductRequest)
    {
        try
        {
            var updateProductResponse = await _userService.UpdateProduct(updateProductRequest, GetCurrentUserId());

            return updateProductResponse is not null
                ? Ok(updateProductResponse)
                : NotFound(new { Message = "Update Product Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<GetProductResponse>> GetProduct(long userId)
    {
        try
        {
            var getProductResponse = await _userService.GetProductByIdAsync(userId, GetCurrentUserId());

            return getProductResponse is not null
                ? Ok(getProductResponse)
                : NotFound(new { Message = "Get Product Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<ICollection<GetProductResponse>>> GetProductList()
    {
        try
        {
            var getProductsResponse = await _userService.GetProductsAsync(GetCurrentUserId());

            return getProductsResponse is not null
                ? Ok(getProductsResponse)
                : NotFound(new { Message = "Get Products Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
