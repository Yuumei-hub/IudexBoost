using IudexBoost.Business.Interfaces;
using IudexBoost.ProjectServices.Services;
using IudexBoost.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IudexBoost.Controllers
{
    public class AccountController : Controller
    {
        UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            /*var userdata=_userService.GetById(int.Parse(userId));

            ViewBag.UserData = userdata;*/
            return View();
        }
    }
}
