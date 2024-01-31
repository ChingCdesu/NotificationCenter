namespace NotificationCenter.Entities;

public class ServiceAccountDeviceBind
{
    public Guid ServiceAccountId { get; set; } = Guid.Empty;
    
    public Guid DeviceId { get; set; } = Guid.Empty;
}