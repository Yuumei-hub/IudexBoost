using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using IudexBoost.ProjectServices.Services;

namespace IudexBoost.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginForm( User user)
        {
            var authenticatedUser = _userService.AuthenticateUser(user.Email, user.Password);
            if (authenticatedUser == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Index","Login");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,authenticatedUser.Username),
                new Claim(ClaimTypes.Email,authenticatedUser.Email),
                new Claim(ClaimTypes.NameIdentifier,authenticatedUser.UserId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Index", "Default");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterForm(User user)
        {
            bool isRegistered = _userService.RegisterUser(user);

            if(!isRegistered)
            {
                ViewBag.ErrorMessage = "This username already exists.";
                return View("Index");
            }

            return View("Index");
        }

        /*Changes made:
            Dependency Injection of UserService:

            The LoginController now receives an instance of UserService via its constructor, allowing it to use the business logic encapsulated in UserService.
            Authentication Logic Moved to UserService:

            The direct database queries in LoginForm have been replaced with a call to AuthenticateUser, which handles the logic of verifying the email and password.
            User Registration Logic Moved to UserService:

            The RegisterForm method now uses the RegisterUser method of UserService, which checks for existing users and registers new ones if possible.*/
    }
}
