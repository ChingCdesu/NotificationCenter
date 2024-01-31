using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public ICollection<User> Users { get; } = new List<User>();
    
    public bool SystemRole { get; set; } = false;
    
    public ulong Permissions { get; set; } = 0;
}