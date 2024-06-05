namespace ChatApp.Services;

public record MessageUpdateModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int? UserId { get; set; }
    public int? GroupId { get; set; }
}
