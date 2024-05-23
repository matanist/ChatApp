namespace ChatApp.Data;

public class Message: BaseEntity
{
    public string Content { get; set; }
    public string? UserId { get; set; }
    public User User { get; set; }
    public string? GroupId { get; set; }
    public Group Group { get; set; }
}
