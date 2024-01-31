using NotificationCenter.Abstract;
using NotificationCenter.Enums;

namespace NotificationCenter.Entities;

public class Parameter : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ParameterType Type { get; set; } = ParameterType.Unknown;
    
    public string Remark { get; set; } = string.Empty;
    
    public string Format { get; set; } = string.Empty;

    public bool System { get; set; } = false;
    
    public ICollection<Template> Templates { get; } = new List<Template>();
}