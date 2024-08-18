namespace ChatApp.Data.Models;

public record MessageHistoryModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SenderUsername { get; set; }
    public string? ReceiverUsername { get; set; }
    public string? GroupName { get; set; }
    public bool MessageForPrivateChat { get; set; }
}