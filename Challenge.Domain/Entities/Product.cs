namespace Challenge.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Value { get; private set; }
    public int Inventory { get; private set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }

    public static Product Create(ProductModel model)
    {
        Product product = new();

        product.SetName(model.Name);
        product.SetDescription(model.Description);
        product.SetValue(model.Value);
        product.SetInventory(model.Inventory);

        product.SetCreatedBy(model.UpdateUserId);
        product.SetCreated(DateTime.UtcNow);
        product.SetUpdatedBy(model.UpdateUserId);
        product.SetUpdated(DateTime.UtcNow);

        return product;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
    }

    public void SetInventory(int inventory)
    {
        if (inventory <= 0)
            throw new ArgumentException("Inventory cannot be less than or equal to zero.", nameof(inventory));

        Inventory = inventory;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));

        Description = description;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0.0m)
            throw new ArgumentException("Value must be greater than zero.", nameof(value));

        Value = value;
    }

    public record ProductModel
    (
        string Name,
        string Description,
        decimal Value,
        int Inventory,
        long UpdateUserId
    );
}
