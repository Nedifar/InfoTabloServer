using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace InfoTabloServer.SiganlR
{
    public class GoodHubGbl : Hub
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
