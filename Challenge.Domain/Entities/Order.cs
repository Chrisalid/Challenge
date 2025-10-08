using Challenge.Domain.Enums;

namespace Challenge.Domain.Entities;

public class Order : BaseEntity
{
    public long ClientId { get; private set; }
    public string Name { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }

    public Client Client { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    public static Order Create(OrderModel model)
    {
        Order order = new();

        order.SetClientId(model.ClientId);
        order.SetName(model.Name);
        order.SetStatus(model.Status);
        order.SetTotalAmount(model.TotalAmount);

        order.SetCreatedBy(model.UpdateUserId);
        order.SetCreated(DateTime.UtcNow);
        order.SetUpdatedBy(model.UpdateUserId);
        order.SetUpdated(DateTime.UtcNow);

        return order;
    }

    public void SetClientId(long clientId)
    {
        if (clientId >= 0)
            throw new ArgumentException("ClientId must be greater than zero.", nameof(clientId));

        ClientId = clientId;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
    }

    public void SetStatus(OrderStatus status)
    {
        if (!Enum.IsDefined(status))
            throw new ArgumentException("Invalid Status.", nameof(status));

        Status = status;
    }

    public void SetTotalAmount(decimal totalAmount)
    {
        if (totalAmount <= 0.0m)
            throw new ArgumentException("TotalAmount must be greater than zero.", nameof(totalAmount));

        TotalAmount = totalAmount;
    }

    public record OrderModel
    (
        long ClientId,
        string Name,
        OrderStatus Status,
        decimal TotalAmount,
        long UpdateUserId
    );
}
