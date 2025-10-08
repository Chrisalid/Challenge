using System;
using Challenge.Api.Controllers.Base;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers;

[ApiController]
public class UserController(IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<ActionResult<UserLoginResponse>> LoginPerson(UserLoginRequest jsonLoginRequest)
    {
        try
        {
            var loginResponse = await _userService.UserLogin(jsonLoginRequest);

            return loginResponse is not null && !string.IsNullOrWhiteSpace(loginResponse.Token)
                ? Ok(new { loginResponse.Token })
                : NotFound(new { Message = "Invalid login credentials." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest createUserRequest)
    {
        try
        {
            var createUserResponse = await _userService.CreateUserMaster(createUserRequest, GetCurrentUserId());

            return createUserResponse is not null
                ? Ok(createUserResponse)
                : NotFound(new { Message = "Creation User Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<UpdateUserResponse>> UpdateUser(UpdateUserRequest updateUserRequest)
    {
        try
        {
            var updateUserResponse = await _userService.UpdateUser(updateUserRequest, GetCurrentUserId());

            return updateUserResponse is not null
                ? Ok(updateUserResponse)
                : NotFound(new { Message = "Update User Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<GetUserResponse>> GetUser(long userId)
    {
        try
        {
            var getUserResponse = await _userService.GetUserByIdAsync(userId, GetCurrentUserId());

            return getUserResponse is not null
                ? Ok(getUserResponse)
                : NotFound(new { Message = "Get User Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles="Master")]
    public async Task<ActionResult<ICollection<GetUserResponse>>> GetUserList()
    {
        try
        {
            var getUsersResponse = await _userService.GetUsersAsync(GetCurrentUserId());

            return getUsersResponse is not null
                ? Ok(getUsersResponse)
                : NotFound(new { Message = "Get Users Data Is Invalid." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
