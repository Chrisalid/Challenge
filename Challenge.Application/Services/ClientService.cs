using Challenge.Application.DTOs.Request;
using Challenge.Application.DTOs.Response;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Mapping;
using Challenge.Domain.Entities;
using Challenge.Domain.Enums;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Utility;
using Microsoft.Extensions.Configuration;

namespace Challenge.Application.Services;

public class ClientService(IUnitOfWorkRepository unitOfWorkRepository) : IClientService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
    private readonly ClientMapper _mapper = new();

    public async Task<CreateClientResponse> CreateClient(CreateClientRequest createClientRequest)
    {
        var clientRepository = _unitOfWorkRepository.Clients;
        var userRepository = _unitOfWorkRepository.Users;
        
        if (string.IsNullOrWhiteSpace(createClientRequest.Email))
            throw new Exception();
        if (string.IsNullOrWhiteSpace(createClientRequest.Password))
            throw new Exception();

        var user = userRepository.GetByEmail(createClientRequest.Email);
        if (user is not null)
            throw new Exception();

        var client = clientRepository.GetByEmail(createClientRequest.Email);
        if (client is not null)
            throw new Exception();

        var userModel = new User.UserModel(
            createClientRequest.Name,
            createClientRequest.Email,
            createClientRequest.Password,
            UserRole.User,
            ConfigHelper.DefaultUserId
        );

        await _unitOfWorkRepository.Begin();

        try
        {
            var newUser = User.Create(userModel);
            newUser.Id = await userRepository.CreateAsync(newUser);

            var clientModel = new Client.ClientModel(
                newUser.Id,
                createClientRequest.Name,
                createClientRequest.Phone,
                createClientRequest.Email,
                newUser.Id
            );

            var newClient = Client.Create(clientModel);
            newClient.Id = await clientRepository.CreateAsync(newClient);

            await _unitOfWorkRepository.Commit();

            return _mapper.CreateMap(newClient);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<UpdateClientResponse> UpdateClient(UpdateClientRequest updateClientRequest, long loggedUserId)
    {
        var clientRepository = _unitOfWorkRepository.Clients;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var clientToUpdate = await clientRepository.GetById(updateClientRequest.ClientId);

        if (clientToUpdate.UserId != loggedUserId)
            throw new Exception();

        await _unitOfWorkRepository.Begin();
        try
        {
            bool isChange = false;

            if (!string.IsNullOrWhiteSpace(updateClientRequest.Email) && !string.Equals(updateClientRequest.Email, clientToUpdate.Email))
            {
                clientToUpdate.SetEmail(updateClientRequest.Email);
                isChange = true;
            }

            if (!string.IsNullOrWhiteSpace(updateClientRequest.Name) && !string.Equals(updateClientRequest.Name, clientToUpdate.Name))
            {
                clientToUpdate.SetName(updateClientRequest.Name);
                isChange = true;
            }

            if (isChange)
            {
                clientToUpdate.SetUpdatedBy(loggedUser.Id);
                clientToUpdate.SetUpdated(DateTime.UtcNow);

                await clientRepository.UpdateAsync(clientToUpdate);
            }

            await _unitOfWorkRepository.Commit();

            return _mapper.UpdateMap(clientToUpdate);
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task DeleteClient(long id, long loggedUserId)
    {
        var clientRepository = _unitOfWorkRepository.Clients;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var clientToDelete = await clientRepository.GetById(id);
        if (clientToDelete.UserId != loggedUserId && Equals(UserRole.User, loggedUser.Role))
            throw new Exception();

        await _unitOfWorkRepository.Begin();
        try
        {
            clientToDelete.SetDeleted(DateTime.UtcNow);
            clientToDelete.SetUpdated(DateTime.UtcNow);
            clientToDelete.SetUpdatedBy(loggedUser.Id);

            await clientRepository.DeleteAsync(clientToDelete);
            await _unitOfWorkRepository.Commit();
        }
        catch
        {
            await _unitOfWorkRepository.Rollback();
            throw;
        }
    }

    public async Task<GetClientResponse> GetClientByIdAsync(long clientId, long loggedUserId)
    {
        var clientRepository = _unitOfWorkRepository.Clients;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive)
            throw new Exception();

        var client = await clientRepository.GetById(clientId);
        if (client.UserId != loggedUserId && Equals(UserRole.User, loggedUser.Role))
            throw new Exception();

        return _mapper.GetMap(client);
    }

    public async Task<IEnumerable<GetClientResponse>> GetClientsAsync(long loggedUserId)
    {
        var clientRepository = _unitOfWorkRepository.Clients;
        var userRepository = _unitOfWorkRepository.Users;

        var loggedUser = await userRepository.GetById(loggedUserId) ?? throw new Exception();

        if (!loggedUser.IsActive || !Equals(UserRole.Master, loggedUser.Role))
            throw new Exception();

        var clients = await clientRepository.GetList();

        return _mapper.GetListMap(clients);
    }
}
