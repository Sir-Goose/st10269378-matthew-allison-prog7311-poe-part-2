using Microsoft.AspNetCore.Mvc;
using prog7311.Repository;
using prog7311.Models;
using System.Linq;

namespace prog7311.Controllers
{
    public class FarmerController : Controller
    {
        public IActionResult List()
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            using (var db = new AppDbContext())
            {
                var farmers = db.Farmers.ToList();
                return View(farmers);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string email, string password)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            using (var db = new AppDbContext())
            {
                if (!db.Farmers.Any(f => f.Email == email))
                {
                    db.Farmers.Add(new Farmer { Name = name, Email = email, Password = password });
                    db.SaveChanges();
                    ViewBag.Success = true;
                }
                else
                {
                    ViewBag.Error = "A farmer with this email already exists.";
                }
            }
            return View();
        }
    }
} 