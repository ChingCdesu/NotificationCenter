using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class ServiceSecret : BaseEntity
{
    public Guid ServiceId { get; set; } = Guid.NewGuid();

    public string HashedServiceSecret { get; set; } = string.Empty;
    
    public Service Service { get; set; } = null!;
}