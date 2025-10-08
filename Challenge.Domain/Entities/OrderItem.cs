namespace Challenge.Domain.Entities;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal IndividualAmount { get; private set; }

    public Order Order { get; set; }
    public Product Product { get; set; }

    public static OrderItem Create(OrderItemModel model)
    {
        OrderItem orderItem = new();

        orderItem.SetOrderId(model.OrderId);
        orderItem.SetProductId(model.ProductId);
        orderItem.SetQuantity(model.Quantity);
        orderItem.SetIndividualAmount(model.IndividualAmount);

        orderItem.SetCreatedBy(model.UpdateUserId);
        orderItem.SetCreated(DateTime.UtcNow);
        orderItem.SetUpdatedBy(model.UpdateUserId);
        orderItem.SetUpdated(DateTime.UtcNow);

        return orderItem;
    }

    public void SetOrderId(long orderId)
    {
        if (orderId >= 0)
            throw new ArgumentException("ClientId must be greater than zero.", nameof(orderId));

        OrderId = orderId;
    }

    public void SetProductId(long productId)
    {
        if (productId >= 0)
            throw new ArgumentException("ProductId must be greater than zero.", nameof(productId));

        ProductId = productId;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity >= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        Quantity = quantity;
    }

    public void SetIndividualAmount(decimal individualAmount)
    {
        if (individualAmount <= 0.0m)
            throw new ArgumentException("IndividualAmount must be greater than zero.", nameof(individualAmount));

        IndividualAmount = individualAmount;
    }

    public record OrderItemModel(
        long OrderId,
        long ProductId,
        int Quantity,
        decimal IndividualAmount,
        long UpdateUserId
    );
}
