using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp;
// [Authorize]
public class ChatHub : Hub
{
    private readonly IDictionary<string, string> _connectedUsers;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public ChatHub(IDictionary<string, string> connectedUsers, IMessageService messageService, IUserService userService)
    {
        _connectedUsers = connectedUsers;
        _messageService = messageService;
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {


        HttpContext context = Context.GetHttpContext();
        var token = context.Request.Headers["Authorization"];
        var user = Context.User.Identity.Name;
        if (!_connectedUsers.ContainsKey(user))
            _connectedUsers.TryAdd(user, Context.ConnectionId);
        else
        {
            _connectedUsers[user] = Context.ConnectionId;
        }

        await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId);
    }
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessageFromAll", user, message);
    }
    public async Task SendMessageToUser(string username, string message)
    {
        var userExists = _connectedUsers.TryGetValue(username, out var connectionId);
        var user = Context.User.Identity.Name;
        var userSender = await _userService.GetUserByUsernameAsync(user);
        var userReceiver = await _userService.GetUserByUsernameAsync(username);
        var newMessage = new Message
        {
            Content = message,
            UserId = userSender.Id,
            CreatedAt = DateTime.Now,
            GroupId = null,
            IsDeleted = false,
            UpdatedAt = DateTime.Now,
            ReceiverUserId = userReceiver.Id,
            ReceiverGroupId = null
        };
        if (userExists)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessageFromUser", user, message);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessageFromUser", "System", "User is not connected");
        }
        await _messageService.AddAsync(newMessage);

    }
    public async Task SendMessageToGroup(string group, string message)
    {
        await Clients.Group(group).SendAsync("ReceiveMessageFromGroup", message);
    }
}
