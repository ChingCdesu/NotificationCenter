using NotificationCenter.Abstract;
using NotificationCenter.Enums;

namespace NotificationCenter.Entities;

public class NotificationEndpoint : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public DeviceFamily DeviceFamily { get; set; } = DeviceFamily.Unknown;

    public string AppId { get; set; } = string.Empty;

    public string AppSecret { get; set; } = string.Empty;

    public ICollection<Service> Services { get; } = new List<Service>();
}