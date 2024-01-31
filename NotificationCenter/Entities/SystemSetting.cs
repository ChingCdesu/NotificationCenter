using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class SystemSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    
    public string? Value { get; set; } = string.Empty;
}