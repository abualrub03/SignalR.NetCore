﻿using Microsoft.AspNetCore.Mvc;
using SignalR.Hubs;
using SignalR.Models;
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
            var a = new SignalRProvider.AccountProvider().spListAllOnlineAccounts();
            return a;
        }



    
        public IActionResult Index( Entities.Account dp)
        {
            var a = new SignalRProvider.AccountProvider().spListAllOnlineAccounts();
            var vm = new ViewModel.HomePageViewModel();
            vm.account = dp;
            vm.accounts = a;
            return View("Index",vm);
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
            return View();
        }
        [HttpPost]
        public IActionResult SignInRequest(Entities.Account account)
        {
            var dp = new SignalRProvider.AccountProvider();
            var acc = dp.spSignInRequest(account);
            if (dp != null)
            { 
                return Index(acc);
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
		public IActionResult Sign_Up()
		{
			return View();
		}













		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
