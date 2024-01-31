namespace NotificationCenter.Entities;

public class RoleUserBind
{
    public Guid RoleId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;
}