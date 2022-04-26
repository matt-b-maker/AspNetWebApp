using Microsoft.AspNetCore.SignalR;

namespace LearningAspNetWebApp.Hubs
{
    public class PlayerHub : Hub
    {
        public async Task SendMessageAsync(string username, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
