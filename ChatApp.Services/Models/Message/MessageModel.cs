namespace ChatApp.Services;

public record MessageModel
{
    public string Content { get; set; }
    public int? UserId { get; set; }
    public int? GroupId { get; set; }
}
