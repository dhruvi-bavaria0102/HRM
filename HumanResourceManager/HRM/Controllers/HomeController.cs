using HRM.BAL.Manager;
using HRM.Data.Models;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(
                   UserManager<AppUser> userManager,
                   SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
          
            return View();
        }

        //Login if user is authorised user
        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {

            //login functionality  
            var user = await _userManager.FindByEmailAsync(appUser.Email);

            if (user != null)
            {
                //sign in  
                var signInResult = await _signInManager.PasswordSignInAsync(user, appUser.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }

        //Register for being an authorised user
        [HttpPost]
        public async Task<IActionResult> Register(AppUser appUser)
        {
            //register functionality  

            var user = new AppUser
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Name = appUser.Name,
                DateOfBirth = appUser.DateOfBirth,
                Password = appUser.Password
            };

            var result = await _userManager.CreateAsync(user, user.Password);


            if (result.Succeeded)
            {
                // User sign  
                // sign in   
                var signInResult = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            return View();
        }

        //Logout from application
        public async Task<IActionResult> LogOut(string username, string password)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }
    }
}
