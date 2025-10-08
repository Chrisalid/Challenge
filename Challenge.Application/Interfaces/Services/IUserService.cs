using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Interfaces.Services;

public interface IUserService
{
    Task<UserLoginResponse> UserLogin(UserLoginRequest userLoginRequest);
    Task<CreateUserResponse> CreateUserMaster(CreateUserRequest createUserRequest, long loggedUserId);
    Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest, long loggedUserId);
    Task DeleteUser(long id, long loggedUserId);
    Task<GetUserResponse> GetUserByIdAsync(long userId, long loggedUserId);
    Task<IEnumerable<GetUserResponse>> GetUsersAsync(long loggedUserId);
}
