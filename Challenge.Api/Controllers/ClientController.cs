using Challenge.Api.Controllers.Base;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers;

public class ClientController(IClientService userService) : BaseController
{
    private readonly IClientService _userService = userService;

    [HttpPost]
    [Authorize(Roles="User")]
    public async Task<ActionResult<CreateClientResponse>> CreateClient(CreateClientRequest createClientRequest)
    {
        try
        {
            var createClientResponse = await _userService.CreateClient(createClientRequest);

            return createClientResponse is not null
                ? Ok(createClientResponse)
                : NotFound(new { Message = "Creation Client Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<UpdateClientResponse>> UpdateClient(UpdateClientRequest updateClientRequest)
    {
        try
        {
            var updateClientResponse = await _userService.UpdateClient(updateClientRequest, GetCurrentUserId());

            return updateClientResponse is not null
                ? Ok(updateClientResponse)
                : NotFound(new { Message = "Update Client Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<GetClientResponse>> GetClient(long userId)
    {
        try
        {
            var getClientResponse = await _userService.GetClientByIdAsync(userId, GetCurrentUserId());

            return getClientResponse is not null
                ? Ok(getClientResponse)
                : NotFound(new { Message = "Get Client Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="User,Master")]
    public async Task<ActionResult<ICollection<GetClientResponse>>> GetClientList()
    {
        try
        {
            var getClientsResponse = await _userService.GetClientsAsync(GetCurrentUserId());

            return getClientsResponse is not null
                ? Ok(getClientsResponse)
                : NotFound(new { Message = "Get Clients Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
