namespace Challenge.Application.Interfaces.Services;

public interface IAuthTokenService
{
    Task<bool> ValidateTokenAsync(string token);
}