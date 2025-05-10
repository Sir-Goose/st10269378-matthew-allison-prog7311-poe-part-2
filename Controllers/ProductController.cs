using Microsoft.AspNetCore.Mvc;
using prog7311.Services;
using prog7311.Models;
using System;

namespace prog7311.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFarmerService _farmerService;

        public ProductController(IProductService productService, IFarmerService farmerService)
        {
            _productService = productService;
            _farmerService = farmerService;
        }

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

            var farmer = _farmerService.GetFarmerByEmail(Request.Cookies["UserEmail"]);
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

            var (success, message) = _productService.AddProduct(product);
            if (success)
            {
                return RedirectToAction("Add", new { success = true });
            }

            ViewBag.Error = message;
            return View();
        }

        public IActionResult ListByFarmer(int farmerId)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            var farmer = _farmerService.GetFarmerByEmail(Request.Cookies["UserEmail"]);
            if (farmer == null) return NotFound();

            var products = _productService.GetProductsByFarmer(farmerId);
            ViewBag.Farmer = farmer;
            return View(products);
        }

        [HttpGet]
        public IActionResult Search(string name, string category, int? farmerId)
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            var farmers = _farmerService.GetAllFarmers();
            var products = _productService.SearchProducts(name, category, farmerId);

            ViewBag.Farmers = farmers;
            ViewBag.SelectedFarmerId = farmerId;
            ViewBag.Name = name;
            ViewBag.Category = category;
            return View(products);
        }
    }
} 