using Microsoft.AspNetCore.SignalR;

namespace FRIchat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message, string image)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message, image);
    }

    public async Task DeleteMessage(string odgovorId)
    {
        await Clients.All.SendAsync("DeleteForAll", odgovorId);
    }
    
    
}