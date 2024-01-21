using Entities;
using Microsoft.AspNetCore.Mvc;
using SignalR.Hubs;
using System;
using System.Data;
using System.Diagnostics;
namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
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
        public IActionResult _Users(int Id)
		{
            Account dp = new Account();
            dp.Id = Id;
			var a = new SignalRProvider.AccountProvider().spListAllAccounts();
			var vm = new ViewModel.HomePageViewModel();
			vm.account = dp;
			vm.accounts = a;
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
            return View("HomePage");
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
        [HttpPost]
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
			return PartialView("_Chat",a);
		}
    }
}
