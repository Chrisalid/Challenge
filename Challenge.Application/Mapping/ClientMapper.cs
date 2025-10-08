using Challenge.Application.DTOs.Response;
using Challenge.Domain.Entities;

namespace Challenge.Application.Mapping;

public class ClientMapper
{
    public CreateClientResponse CreateMap(Client client)
    {
        return new()
        {
            ClientId = client.Id,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone
        };
    }

    public UpdateClientResponse UpdateMap(Client client)
    {
        return new()
        {
            ClientId = client.Id,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone
        };
    }

    public GetClientResponse GetMap(Client client)
    {
        return new()
        {
            ClientId = client.Id,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone
        };
    }

    public IEnumerable<GetClientResponse> GetListMap(IEnumerable<Client> clients)
    {
        return clients.Select(c => new GetClientResponse
        {
            ClientId = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone
        });
    }
}
