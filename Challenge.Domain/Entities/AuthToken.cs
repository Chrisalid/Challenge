namespace Challenge.Domain.Entities;

public class AuthToken : BaseEntity
{
    public string Token { get; set; }

    public long UserId { get; set; }

    public DateTime ExpiresAt { get; set; }

    public User User { get; set; }

    public static AuthToken Create(AuthTokenModel model)
    {
        AuthToken authToken = new();

        authToken.SetToken(model.Token);
        authToken.SetUserId(model.UserId);
        authToken.SetExpiresAt(model.ExpiresAt);

        authToken.SetCreatedBy(model.UpdateUserId);
        authToken.SetCreated(DateTime.UtcNow);
        authToken.SetUpdatedBy(model.UpdateUserId);
        authToken.SetUpdated(DateTime.UtcNow);

        return authToken;
    }

    public void SetToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));

        Token = token;
    }

    public void SetUserId(long userId)
    {
        if (userId >= 0)
            throw new ArgumentException("UserId must be greater than zero.", nameof(userId));

        UserId = userId;
    }

    public void SetExpiresAt(DateTime expiresAt)
    {
        ExpiresAt = expiresAt;
    }

    public record AuthTokenModel
    (
        string Token,
        long UserId,
        DateTime ExpiresAt,
        long UpdateUserId
    );
}
