namespace Challenge.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void SetCreatedBy(long? userId) => CreatedBy = userId;
    public void SetCreated(DateTime created) => CreatedAt = created;
    public void SetUpdatedBy(long? userId) => UpdatedBy = userId;
    public void SetUpdated(DateTime updated) => UpdatedAt = updated;
    public void SetDeleted(DateTime? deleted) => DeletedAt = deleted;
}
