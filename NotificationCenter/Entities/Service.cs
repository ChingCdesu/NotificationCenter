using NotificationCenter.Abstract;
using NotificationCenter.Enums;

namespace NotificationCenter.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ServiceType Type { get; set; } = ServiceType.Unknown;

    public ServiceSecret Secret { get; } = null!;
    
    public string PackageIdentifier { get; set; } = string.Empty;
    
    public ICollection<ServiceAccount> ServiceAccounts { get; } = new List<ServiceAccount>();
    
    public ICollection<NotificationEndpoint> NotificationEndpoints { get; } = new List<NotificationEndpoint>();
    
    public ICollection<Template> Templates { get; } = new List<Template>();
}