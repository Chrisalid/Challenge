namespace Challenge.Domain.Entities;

public class Client : BaseEntity
{
    public long UserId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public User User { get; set; }
    public ICollection<Order> Order { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    public static Client Create(ClientModel model)
    {
        Client client = new();

        client.SetUserId(model.UserId);
        client.SetName(model.Name);
        client.SetPhone(model.Phone);
        client.SetEmail(model.Email);

        client.SetCreatedBy(model.UpdateUserId);
        client.SetCreated(DateTime.UtcNow);
        client.SetUpdatedBy(model.UpdateUserId);
        client.SetUpdated(DateTime.UtcNow);

        return client;
    }

    public void SetUserId(long userId)
    {
        if (userId >= 0)
            throw new ArgumentException("UserId must be greater than zero.", nameof(userId));

        UserId = userId;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
    }

    public void SetPhone(string? phone)
    {
        Phone = phone;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        Email = email;
    }

    public record ClientModel
    (
        long UserId,
        string Name,
        string? Phone,
        string Email,
        long UpdateUserId
    );
}
