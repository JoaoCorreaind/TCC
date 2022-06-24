using Microsoft.AspNetCore.SignalR;


namespace WebApplication1.Models.Tools
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;

        public void NewMessage(string userName, string message)
        {
            Clients.All.SendAsync("newMessage", userName, message);
        }
    }
}
