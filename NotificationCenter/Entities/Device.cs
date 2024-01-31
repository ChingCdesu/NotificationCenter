using NotificationCenter.Abstract;
using NotificationCenter.Enums;

namespace NotificationCenter.Entities;

public class Device : BaseEntity
{
    public string DeviceToken { get; set; } = string.Empty;

    public DeviceFamily DeviceFamily { get; set; } = DeviceFamily.Unknown;
    
    public string SystemVersion { get; set; } = string.Empty;
    
    public string DistroVersion { get; set; } = string.Empty;
    
    public string DeviceModel { get; set; } = string.Empty;
    
    public ICollection<ServiceAccount> ServiceAccounts { get; } = new List<ServiceAccount>();
}