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

        public IActionResult ListByFarmer(int farmerId)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            using (var db = new AppDbContext())
            {
                var farmer = db.Farmers.FirstOrDefault(f => f.Id == farmerId);
                if (farmer == null) return NotFound();
                var products = db.Products.Where(p => p.FarmerId == farmerId).ToList();
                ViewBag.Farmer = farmer;
                return View(products);
            }
        }

        [HttpGet]
        public IActionResult Search(string name, string category, int? farmerId)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");
            using (var db = new AppDbContext())
            {
                var farmers = db.Farmers.ToList();
                var products = db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                    products = products.Where(p => p.Name.Contains(name));
                if (!string.IsNullOrEmpty(category))
                    products = products.Where(p => p.Category.Contains(category));
                if (farmerId.HasValue)
                    products = products.Where(p => p.FarmerId == farmerId.Value);
                ViewBag.Farmers = farmers;
                ViewBag.SelectedFarmerId = farmerId;
                ViewBag.Name = name;
                ViewBag.Category = category;
                return View(products.ToList());
            }
        }
    }
} 