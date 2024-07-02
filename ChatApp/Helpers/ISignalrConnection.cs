using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp;

public interface ISignalrConnection
{
    HubConnection StartConnection();
    bool IsConnected();
}
