using Microsoft.AspNetCore.Mvc;
using prog7311.Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace prog7311.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var employee = db.Employees.FirstOrDefault(e => e.Email == email && e.Password == password);
                var farmer = db.Farmers.FirstOrDefault(f => f.Email == email && f.Password == password);
                if (employee != null)
                {
                    HttpContext.Response.Cookies.Append("UserEmail", employee.Email);
                    HttpContext.Response.Cookies.Append("UserRole", "Employee");
                    return RedirectToAction("Index", "Home");
                }
                else if (farmer != null)
                {
                    HttpContext.Response.Cookies.Append("UserEmail", farmer.Email);
                    HttpContext.Response.Cookies.Append("UserRole", "Farmer");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid email or password.";
                    return View();
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("UserEmail");
            HttpContext.Response.Cookies.Delete("UserRole");
            return RedirectToAction("Login");
        }
    }
} 