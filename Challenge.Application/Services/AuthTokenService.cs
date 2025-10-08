using Challenge.Application.Interfaces.Services;
using Challenge.Domain.Interfaces.Repositories;

namespace Challenge.Application.Services;

public class AuthTokenService(IUnitOfWorkRepository unitOfWorkRepository) : IAuthTokenService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var authTokenRepository = _unitOfWorkRepository.AuthTokens;
        try
        {
            return await authTokenRepository.IsTokenValidAsync(token);
        }
        catch { throw; }
    }
}