using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Interfaces.Services;

public interface IClientService
{
    Task<CreateClientResponse> CreateClient(CreateClientRequest createClientRequest);
    Task<UpdateClientResponse> UpdateClient(UpdateClientRequest updateClientRequest, long loggedUserId);
    Task DeleteClient(long id, long loggedUserId);
    Task<GetClientResponse> GetClientByIdAsync(long clientId, long loggedUserId);
    Task<IEnumerable<GetClientResponse>> GetClientsAsync(long loggedUserId);
}
