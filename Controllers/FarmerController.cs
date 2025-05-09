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
            return View(new Farmer());
        }

        [HttpPost]
        public IActionResult Add(Farmer model)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using (var db = new AppDbContext())
                {
                    if (!db.Farmers.Any(f => f.Email == model.Email))
                    {
                        db.Farmers.Add(model);
                        db.SaveChanges();
                        ViewBag.Success = true;
                        return View(new Farmer());
                    }
                    else
                    {
                        ViewBag.Error = "A farmer with this email already exists.";
                        return View(model);
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                return View(model);
            }
        }
    }
} 