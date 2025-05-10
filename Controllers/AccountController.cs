using Microsoft.AspNetCore.Mvc;
using prog7311.Services;

namespace prog7311.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var (success, role, userEmail) = _authService.ValidateLogin(email, password);
            if (success)
            {
                HttpContext.Response.Cookies.Append("UserEmail", userEmail);
                HttpContext.Response.Cookies.Append("UserRole", role);
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            _authService.Logout();
            HttpContext.Response.Cookies.Delete("UserEmail");
            HttpContext.Response.Cookies.Delete("UserRole");
            return RedirectToAction("Login");
        }
    }
} 