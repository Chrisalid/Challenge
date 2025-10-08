using Challenge.Domain.Enums;

namespace Challenge.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; }
    public UserRole Role { get; private set; }

    public ICollection<AuthToken> AuthToken { get; set; }
    public Client? Client { get; set; }

    public static User Create(UserModel model)
    {
        User user = new();

        user.SetName(model.Name);
        user.SetEmail(model.Email);
        user.SetPassword(model.Password);

        user.SetCreatedBy(model.UpdateUserId);
        user.SetCreated(DateTime.UtcNow);
        user.SetUpdatedBy(model.UpdateUserId);
        user.SetUpdated(DateTime.UtcNow);

        return user;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        Email = email;
    }

    

    public void SetIsActive(bool isActive)
    {
        if (!isActive)
            throw new ArgumentException("IsActive cannot be false.", nameof(isActive));

        IsActive = isActive;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        Password = password;
    }
    
    public record UserModel(
        string Name,
        string Email,
        string Password,
        UserRole Role,
        long? UpdateUserId
    );
}
