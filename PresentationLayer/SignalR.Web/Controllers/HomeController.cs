using Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SignalR.Hubs;
using System;
using System.Data;
using System.Diagnostics;
using ViewModel;
namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public int CheckActivity (int Id)
        {
            var result = new SignalRProvider.AccountProvider().CheckActivity (Id);
            if(result.IsActive == 1)
            {
				return 1;
            }
            else
            {
                return 0;
            }
		}

		[HttpGet]
        public void UpdateLastActivity(int Id)
        {
            var a = new SignalRProvider.AccountProvider().UpdateLastActivity(Id);
        }
        [HttpGet]
        public List<Entities.Account> ActiveList()
        {
            var a = new SignalRProvider.AccountProvider().spListAllAccounts();
            return a;
        }
        public IActionResult DashBoard( Entities.Account dp)
        {
            var a = new SignalRProvider.AccountProvider().spListAllAccounts();
            var vm = new ViewModel.HomePageViewModel();
            vm.account = dp;
            vm.accounts = a;
            return View("DashBoard",vm);
        }
        [HttpGet]
        public IActionResult _Users(int Id,string filterMode)
		{
            Account dp = new Account();
            dp.Id = Id;
			var vm = new ViewModel.HomePageViewModel();
			vm.account = dp;
            vm.users = new SignalRProvider.AccountProvider().ListAccountsAndUnSeenMessages(Id);
            vm.IIusers = new List<iiUserViewModel>();
            foreach(var i in vm.users)
            {
                var iuser = new iiUserViewModel();
                iuser.Id = i.Id;
                iuser.PhoneNumber = i.PhoneNumber;
                iuser.UserName = i.UserName;
                iuser.Email = i.Email;
                iuser.Password = i.Password;
                iuser.FullName = i.FullName;
                iuser.ConnectionId = i.ConnectionId;
                iuser.IsActive = i.IsActive;
				iuser.UnSeenMessages = i.UnSeenMessages;
				var m = new SignalRProvider.MessageProvider().GetLastMessage(senderId: i.Id, recieverId: Id);
                if (m != null)
                {
                    iuser.LastMessage = m.messageContent;
                    iuser.LastMessageDateTime = m.messageDateTime;
                }
                vm.IIusers.Add(iuser);
            }
            if(filterMode == "FilterReadOnly")
            {
				var temp = new List<iiUserViewModel>();
				foreach (var i in vm.IIusers)
                {
                    if(i.UnSeenMessages <= 0 ) {
						temp.Add(i);
					}
                }
				vm.IIusers = temp;

			}
			else if(filterMode == "FilterUnreadOnly")
            {
				var temp = new List<iiUserViewModel>();

				foreach (var i in vm.IIusers)
				{
					if (i.UnSeenMessages > 0)
					{
						temp.Add(i);
					}
				}
				vm.IIusers = temp;

			}
			return PartialView("_Users", vm);
		}
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUpRequest(Entities.Account account)
        {
            var dp = new SignalRProvider.AccountProvider();
            dp.spSignUpAccount(account);
            return SignIn();
        }
		[Route("/")]
		public IActionResult SignIn()
        {
            return View("SignIn");
        }

    

        [HttpPost]
        public IActionResult SignInRequest(Entities.Account account)
        {
            var dp = new SignalRProvider.AccountProvider();
            var acc = dp.spSignInRequest(account);
            if (dp != null)
            { 
                return DashBoard(acc);
            }
            else
            {
                return SignIn();
            }
        }
       
		[HttpGet]
        public IActionResult SignOut(int Id)
        {
            var dp = new SignalRProvider.AccountProvider();
            var acc = dp.spEndConnection(Id);
            return SignIn();
        }

		public IActionResult Sign_In()
		{
			return View();
		}
        [HttpPost]
        public void SetConId(string conId , int userId )
		{
            new SignalRProvider.AccountProvider().ConnectUser(conId, userId);
		}
		public IActionResult Sign_Up()
		{
			return View();
		}
        [HttpPost]
        public IActionResult _Chat(int accountId , int friendId)
		{
            var a = new ViewModel.HomePageViewModel();
            Account i = new Account(); 
            i.Id = accountId;
            var ii = new SignalRProvider.AccountProvider().returnAccountOnId(friendId);
            var list = new List<Account>();
            list.Add(ii);
			a.account = i;
            a.accounts = list;
			a.listMessages = new SignalRProvider.MessageProvider().ListAllChatMessage(accountId, friendId).OrderBy(x => x.messageDateTime).ToList();
			return PartialView("_Chat",a);
		}
        
        [HttpPost]
        public void userOpenedChat(int accountId , int friendId)
		{
            var result = new SignalRProvider.MessageProvider().userOpenedChat(accountId, friendId);
		}
    }
}
