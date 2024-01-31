namespace NotificationCenter.Entities;

public class TemplateParameterBind
{
    public Guid TemplateId { get; set; } = Guid.Empty;
    
    public Guid ParameterId { get; set; } = Guid.Empty;
}