using Entities;
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
        public async Task SendToUser(int senderId, int receiverId ,string image, string message, string type)
        {   if (type == "text")
            {
                Message ms = new Message();
                ms.messageSenderId = senderId;
                ms.messageRecieverId = receiverId;
                ms.messageDateTime = DateTime.UtcNow;
                ms.messageContent = message;
                ms.messageStatus = "send";
                ms.messageType = "text";
                ms.messagePathIfExist = "";

                new SignalRProvider.MessageProvider().NewMessage(ms);
                var acc = new SignalRProvider.AccountProvider().returnAccountOnId(ms.messageRecieverId);
				await Clients.Client(acc.ConnectionId).SendAsync("ReceiveMessage", ms.messageSenderId, ms.messageContent);
			}
            else{

				var acc = new SignalRProvider.AccountProvider().returnAccountOnId(receiverId);
				await Clients.Client(acc.ConnectionId).SendAsync("ReceiveImage", senderId, image, message);


			}
		}
        public async Task SendToUserImg(int senderId, int receiverId, IFormFile files)
        {


			var acc = new SignalRProvider.AccountProvider().returnAccountOnId(2);
			await Clients.Client(acc.ConnectionId).SendAsync("ReceiveMessage", 1, "dsf");
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
