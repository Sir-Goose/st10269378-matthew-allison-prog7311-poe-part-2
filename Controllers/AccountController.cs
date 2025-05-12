using Microsoft.AspNetCore.Mvc;
using prog7311.Services;

/*
 * This is the Account Controller class. Its responsibility is to handle authentication. There is a login http
 * get method a login post method and a logout action method.
 */

namespace prog7311.Controllers
{
    public class AccountController : Controller
    {
        // declaring dependency injected authentication service to validate user credentials
        private readonly IAuthenticationService _authService;

        // Authentication service constructor
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        // Handle get requests made to /Account/Login
        // Returns the login view to the user
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handle post requests made to /Account/Login
        // Takes in user credentials from the login form on the page.
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // validate user credentials
            var (success, role, userEmail) = _authService.ValidateLogin(email, password);
            if (success)
            {
                // If validation is successful set the cookies for the user
                HttpContext.Response.Cookies.Append("UserEmail", userEmail);
                HttpContext.Response.Cookies.Append("UserRole", role);
                // redirect the user back to the home page
                return RedirectToAction("Index", "Home");
            }
            
            // Display an error when authentication was not successful
            ViewBag.Error = "Invalid email or password.";
            return View(); 
        }

        // Logs out the user
        public IActionResult Logout()
        {
            _authService.Logout(); // call the authentication service to do the logout logic
            HttpContext.Response.Cookies.Delete("UserEmail"); // delete the email cookie 
            HttpContext.Response.Cookies.Delete("UserRole"); // delete the role cookie
            return RedirectToAction("Login"); // return to the login page
        }
    }
} 