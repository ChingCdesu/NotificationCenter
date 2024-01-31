using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class Template : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public ICollection<Parameter> Parameters { get; } = new List<Parameter>();
    
    public ICollection<Service> Services { get; } = new List<Service>();
}