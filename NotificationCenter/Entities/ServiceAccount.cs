using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class ServiceAccount : BaseEntity
{
    public Guid ServiceId { get; set; } = Guid.Empty;
    
    // service send this to NotificationCenter, help NotificationCenter to identify which account receive this message
    public string ServiceIdentifier { get; set; } = string.Empty;
    
    public ICollection<Device> Devices { get; } = new List<Device>();
    
    public Service Service { get; } = null!;
}