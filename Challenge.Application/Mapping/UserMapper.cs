using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Mapping;

public class UserMapper
{
    public CreateUserResponse CreateMap(User user)
    {
        return new()
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive
        };
    }

    public UpdateUserResponse UpdateMap(User user)
    {
        return new()
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive
        };
    }

    public GetUserResponse GetMap(User user)
    {
        return new()
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            IsActive = user.IsActive
        };
    }

    public IEnumerable<GetUserResponse> GetListMap(IEnumerable<User> users)
    {
        return users.Select(u => new GetUserResponse
        {
            UserId = u.Id,
            Name = u.Name,
            Email = u.Email,
            IsActive = u.IsActive
        });
    }

    public UserLoginResponse LoginMap(string token)
    {
        return new UserLoginResponse()
        {
            Token = token
        };
    }
}
