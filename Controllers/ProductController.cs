using Microsoft.AspNetCore.Mvc;
using prog7311.Services;
using prog7311.Models;
using System;
using System.Linq;

/*
 * This is the Product Controller class. It is responsible for handeling all
 * product related actions.
 */

namespace prog7311.Controllers
{
    public class ProductController : Controller
    {
        // service for product operations
        private readonly IProductService _productService;
        // service for farmer related operations
        private readonly IFarmerService _farmerService;

        // Primary constructor
        public ProductController(IProductService productService, IFarmerService farmerService)
        {
            _productService = productService;
            _farmerService = farmerService;
        }

        // get method for the /Product/Add route
        // it returns the form for adding a new product
        [HttpGet]
        public IActionResult Add()
        {
            // verify that the user has the farmer cookie
            if (Request.Cookies["UserRole"] != "Farmer" || string.IsNullOrEmpty(Request.Cookies["UserEmail"]))
                return RedirectToAction("Login", "Account");
            // Return the page
            return View();
        }

        // Post method for the /Product/Add route
        // Handles the form submission for adding a new product 
        [HttpPost]
        public IActionResult Add(string name, string category, DateTime productionDate)
        {
            // Verify that the user has the farmer cookie
            if (Request.Cookies["UserRole"] != "Farmer" || string.IsNullOrEmpty(Request.Cookies["UserEmail"]))
                return RedirectToAction("Login", "Account");

            // Get the farmer's email for the cookie
            var farmer = _farmerService.GetFarmerByEmail(Request.Cookies["UserEmail"]);
            if (farmer == null)
            {
                // If the email was not found redirect back to the login
                return RedirectToAction("Login", "Account");
            }

            // Create the new product object using the data submitted in the form
            var product = new Product
            {
                FarmerId = farmer.Id,
                Name = name,
                Category = category,
                ProductionDate = productionDate
            };

            // add the product using the product service
            var (success, message) = _productService.AddProduct(product);
            if (success)
            {
                // if succesful redirect to the products page
                return RedirectToAction("MyProducts");
            }

            // Show the error to the user
            ViewBag.Error = message;
            return View();
        }

        // Displays a list of all of the products for a specific farmer
        public IActionResult ListByFarmer(int farmerId, DateTime? startDate, DateTime? endDate, string category)
        {
            // Ensure the user is an employee
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            // Get the farmer using the ID
            var farmer = _farmerService.GetAllFarmers().FirstOrDefault(f => f.Id == farmerId);
            if (farmer == null) return NotFound();

            // Get all of the products for that farmer
            var products = _productService.GetProductsByFarmer(farmerId);
            // Apply the filters
            if (startDate.HasValue)
                products = products.Where(p => p.ProductionDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                products = products.Where(p => p.ProductionDate <= endDate.Value).ToList();
            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category).ToList();

            // Pass the filter values and the farmer's info to the view
            ViewBag.Farmer = farmer;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Category = category;
            return View(products);
        }

        // Method for employees to search for products by nanme, category and farmer
        [HttpGet]
        public IActionResult Search(string name, string category, int? farmerId)
        {
            // Check that the user is an employee
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            // Retrieve all of the farmers
            var farmers = _farmerService.GetAllFarmers();
            // Get all of the products using the filters
            var products = _productService.SearchProducts(name, category, farmerId);

            // Pass the values to the view
            ViewBag.Farmers = farmers;
            ViewBag.SelectedFarmerId = farmerId;
            ViewBag.Name = name;
            ViewBag.Category = category;
            return View(products);
        }

        // Lists the products owned by the currently logged in farmer
        [HttpGet]
        public IActionResult MyProducts()
        {
            // Check that the users is a farmer
            if (Request.Cookies["UserRole"] != "Farmer" || string.IsNullOrEmpty(Request.Cookies["UserEmail"]))
                return RedirectToAction("Login", "Account");

            // Retrieve the farmers using the email cookie
            var farmer = _farmerService.GetFarmerByEmail(Request.Cookies["UserEmail"]);
            if (farmer == null) return RedirectToAction("Login", "Account");

            // Get all of the products for the farmer
            var products = _productService.GetProductsByFarmer(farmer.Id);
            ViewBag.Farmer = farmer; 
            // Reuse the ListByFarmer view to display the products
            return View("ListByFarmer", products);
        }
    }
} 