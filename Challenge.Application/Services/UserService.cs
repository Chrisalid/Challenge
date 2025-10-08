using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Mapping;
using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.Application.Services;

public class UserService(IUnitOfWorkRepository unitOfWorkRepository, IConfiguration configuration) : IUserService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
    private readonly IConfiguration _configuration = configuration;
    private UserMapper _mapper = new();

    public async Task<CreateUserResponse> CreateUserMaster(CreateUserRequest createUserRequest, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        await _unitOfWorkRepository.Begin();

        try
        {
            var userModel = new User.UserModel(
            createUserRequest.Name,
            createUserRequest.Email,
            createUserRequest.Password,
            UserRole.User,
            loggedUserId
            );

            var user = User.Create(userModel);

            user.Id = await userRepository.CreateAsync(user);

            await _unitOfWorkRepository.Commit();

            return _mapper.CreateMap(user);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest updateUserRequest, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var userToUpdate = await userRepository.GetById(updateUserRequest.UserId) ?? throw new Exception();

        await _unitOfWorkRepository.Begin();

        try
        {
            bool isChange = false;

            if (!string.IsNullOrWhiteSpace(updateUserRequest.Email) && !string.Equals(updateUserRequest.Email, userToUpdate.Email))
            {
                userToUpdate.SetEmail(updateUserRequest.Email);
                isChange = true;
            }

            if (!string.IsNullOrWhiteSpace(updateUserRequest.Name) && !string.Equals(updateUserRequest.Name, userToUpdate.Name))
            {
                userToUpdate.SetName(updateUserRequest.Name);
                isChange = true;
            }

            if (isChange)
            {
                userToUpdate.SetUpdatedBy(loggedUser.Id);
                userToUpdate.SetUpdated(DateTime.UtcNow);

                await userRepository.UpdateAsync(userToUpdate);
            }

            await _unitOfWorkRepository.Commit();

            return _mapper.UpdateMap(userToUpdate);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task DeleteUser(long id, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var userToDelete = await userRepository.GetById(id);

        await _unitOfWorkRepository.Begin();

        try
        {
            userToDelete.SetDeleted(DateTime.UtcNow);
            userToDelete.SetUpdatedBy(loggedUser.Id);
            userToDelete.SetUpdated(DateTime.UtcNow);

            await userRepository.DeleteAsync(userToDelete);

            await _unitOfWorkRepository.Commit();
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<GetUserResponse> GetUserByIdAsync(long userId, long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var user = await userRepository.GetById(userId);

        return _mapper.GetMap(user);
    }

    public async Task<IEnumerable<GetUserResponse>> GetUsersAsync(long loggedUserId)
    {
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var users = await userRepository.GetList();

        return _mapper.GetListMap(users);
    }

    public async Task<UserLoginResponse> UserLogin(UserLoginRequest userLoginRequest)
    {
        var userRepository = _unitOfWorkRepository.Users;
        var authTokenRepository = _unitOfWorkRepository.AuthTokens;

        if (string.IsNullOrWhiteSpace(userLoginRequest.Email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(UserLoginRequest));

        var user = await userRepository.GetByEmail(userLoginRequest.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(userLoginRequest.Password, user.Password))
            return null;

        var tokenNotExpired = await authTokenRepository.GetByUserId(user.Id);
        if (tokenNotExpired is not null)
            return _mapper.LoginMap(tokenNotExpired.Token);

        var token = GenerateJwtToken(user, out DateTime expiresAt);

        await _unitOfWorkRepository.Begin();
        try
        {
            var authTokenModel = new AuthToken.AuthTokenModel(
                token,
                user.Id,
                expiresAt,
                user.Id
            );

            var authToken = AuthToken.Create(authTokenModel);

            await authTokenRepository.CreateAsync(authToken);

            var loginResponse = new UserLoginResponse { Token = token };

            await _unitOfWorkRepository.Commit();
            return _mapper.LoginMap(token);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    private string GenerateJwtToken(User user, out DateTime expirationDate)
    {
        expirationDate = DateTime.UtcNow.AddHours(8);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, nameof(user.Role))
    };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expirationDate,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
