using Challenge.Api.Controllers.Base;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers;

public class OrderController(IOrderService userService) : BaseController
{
    private readonly IOrderService _userService = userService;

    [HttpPost]
    [Authorize(Roles="User")]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        try
        {
            var createOrderResponse = await _userService.CreateOrder(createOrderRequest, GetCurrentUserId());

            return createOrderResponse is not null
                ? Ok(createOrderResponse)
                : NotFound(new { Message = "Creation Order Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<UpdateOrderResponse>> UpdateOrder(UpdateOrderRequest updateOrderRequest)
    {
        try
        {
            var updateOrderResponse = await _userService.UpdateOrder(updateOrderRequest, GetCurrentUserId());

            return updateOrderResponse is not null
                ? Ok(updateOrderResponse)
                : NotFound(new { Message = "Update Order Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<GetOrderResponse>> GetOrder(long userId)
    {
        try
        {
            var getOrderResponse = await _userService.GetOrderByIdAsync(userId, GetCurrentUserId());

            return getOrderResponse is not null
                ? Ok(getOrderResponse)
                : NotFound(new { Message = "Get Order Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<ICollection<GetOrderResponse>>> GetOrderList()
    {
        try
        {
            var getOrdersResponse = await _userService.GetOrdersAsync(GetCurrentUserId());

            return getOrdersResponse is not null
                ? Ok(getOrdersResponse)
                : NotFound(new { Message = "Get Orders Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
