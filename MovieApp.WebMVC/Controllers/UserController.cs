using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MovieApp.WebMVC.Models.Users;

namespace MovieApp.WebMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model) {
            var result = UserRepository.GetInstance().LoginUser(model);

            if (result.IsSuccess)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, model.Id),
                    new Claim(ClaimTypes.Role, model.IsAdmin.ToString() )
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            else {
                model.Error = result.Error;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {

            var result = UserRepository.GetInstance().RegisterUser(model);
            if (result.IsSuccess)
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Username = model.Username;
                loginModel.Password = model.Password;
                return Login(loginModel);
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}