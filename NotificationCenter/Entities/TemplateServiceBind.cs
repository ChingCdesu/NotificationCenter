namespace NotificationCenter.Entities;

public class TemplateServiceBind
{
    public Guid TemplateId { get; set; } = Guid.Empty;
    
    public Guid ServiceId { get; set; } = Guid.Empty;
}