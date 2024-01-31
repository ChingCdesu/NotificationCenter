namespace NotificationCenter.Abstract;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime? createdAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? updatedAt { get; set; } = DateTime.UtcNow;
}
