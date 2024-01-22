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
        public async Task SendToUser(int senderId, int receiverId, string message)
        {
            var acc = new SignalRProvider.AccountProvider().returnAccountOnId(receiverId) ;
            await Clients.Client(acc.ConnectionId).SendAsync("ReceiveMessage", senderId, message);
        }
        public string GetConnectionId(int Id) {
            var conID = Context.ConnectionId;
            return conID;
        }
        public void EndClientConnection(int Id) {
            var dp = new SignalRProvider.AccountProvider().spEndConnection(Id);    
        }
    }
}
