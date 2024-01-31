using NotificationCenter.Abstract;

namespace NotificationCenter.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string AuthenticationSchema { get; set; } = "Local";
}