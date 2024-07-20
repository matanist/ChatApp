using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp;
// [Authorize]
public class ChatHub : Hub
{
    private readonly IDictionary<string, string> _connectedUsers;

    public ChatHub(IDictionary<string, string> connectedUsers)
    {
        _connectedUsers = connectedUsers;
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
        if (userExists)
        {
            //await Clients.User(connectionId).SendAsync("ReceiveMessageFromUser", user, message);
            await Clients.Client(connectionId).SendAsync("ReceiveMessageFromUser", user, message);
            //return "Mesaj gönderildi"; //! Eklendi
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessageFromUser", "System", "User is not connected");
            //return "Kullanıcı bağlı değil"; //! Eklendi
        }

    }
    public async Task SendMessageToGroup(string group, string message)
    {
        await Clients.Group(group).SendAsync("ReceiveMessageFromGroup", message);
    }
}
