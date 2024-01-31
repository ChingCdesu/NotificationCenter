namespace NotificationCenter.Modals;

public class NotificationMessage
{
    public string Title { get; set; } = string.Empty;
    
    public string Body { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    
    public string? intentUrl { get; set; }
    
    public string? webUrl { get; set; }
    
    public int? badgeAddNum { get; set; }
    
    public int? badgeSetnum { get; set; }
}
