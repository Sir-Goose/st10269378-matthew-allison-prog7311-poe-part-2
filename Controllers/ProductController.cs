using Microsoft.AspNetCore.Mvc;
using prog7311.Repository;
using prog7311.Models;
using System;
using System.Linq;

namespace prog7311.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            if (Request.Cookies["UserRole"] != "Farmer" || string.IsNullOrEmpty(Request.Cookies["UserEmail"]))
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name, string category, DateTime productionDate)
        {
            if (Request.Cookies["UserRole"] != "Farmer" || string.IsNullOrEmpty(Request.Cookies["UserEmail"]))
                return RedirectToAction("Login", "Account");

            using (var db = new AppDbContext())
            {
                var farmer = db.Farmers.FirstOrDefault(f => f.Email == Request.Cookies["UserEmail"]);
                if (farmer == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var product = new Product
                {
                    FarmerId = farmer.Id,
                    Name = name,
                    Category = category,
                    ProductionDate = productionDate
                };
                db.Products.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("Add", new { success = true });
        }
    }
} 