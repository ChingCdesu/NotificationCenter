using NotificationCenter.Abstract;
using NotificationCenter.Enums;

namespace NotificationCenter.Entities;

public class Notification : BaseEntity
{
    public Guid NotificationEndpointId { get; set; } = Guid.Empty;
    
    public NotificationEndpoint NotificationEndpoint { get; } = null!;
    
    public Guid ServiceId { get; set; } = Guid.Empty;
    
    public Service Service { get; } = null!;
    
    public Guid TemplateId { get; set; } = Guid.Empty;
    
    public Template Template { get; } = null!;
    
    public Guid DeviceId { get; set; } = Guid.Empty;
    
    public Device Device { get; } = null!;
    
    public Guid ServiceAccountId { get; set; } = Guid.Empty;
    
    public ServiceAccount ServiceAccount { get; } = null!;
    
    public NotificationStatus Status { get; set; } = NotificationStatus.Unknown;
}