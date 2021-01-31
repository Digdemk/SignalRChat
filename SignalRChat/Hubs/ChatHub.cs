using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("UserConnected", Context.ConnectionId);

            var id = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("UserDiconnected", Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(/*string user,*/ string message)
        {

            string msg = message.Trim();

            await Clients.All.SendAsync("ReceiveMessage", msg);
            //await Clients.Others.SendAsync("ReceiveMessage", msg);

            //await Clients.Client(connectionId).SendAsync("ReceiveMessage", msg);

        }

        public async Task SendMessageToUser(string message, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }
}
