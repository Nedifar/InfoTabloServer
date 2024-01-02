using Microsoft.AspNetCore.SignalR;

namespace InfoTablo.WebApi.Services
{
    public class ScheduleHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
