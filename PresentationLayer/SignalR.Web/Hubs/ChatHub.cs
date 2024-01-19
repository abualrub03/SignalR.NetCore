using Microsoft.AspNetCore.SignalR;
using System;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToUser(string user, string receiverConnectionId, string message)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, message);
        }

        public string GetConnectionId(int Id) {
            
            var conID = Context.ConnectionId;
            var dp = new SignalRProvider.AccountProvider().spStartConnection(Id, conID);

            return conID;

        }
        public void EndClientConnection(int Id) {
            var dp = new SignalRProvider.AccountProvider().spEndConnection(Id);

    
        }
    }
}
