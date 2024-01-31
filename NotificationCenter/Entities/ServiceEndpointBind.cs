namespace NotificationCenter.Entities;

public class ServiceEndpointBind
{
    public Guid NotificationEndpointId { get; set; } = Guid.Empty;
    
    public Guid ServiceId { get; set; } = Guid.Empty;
}