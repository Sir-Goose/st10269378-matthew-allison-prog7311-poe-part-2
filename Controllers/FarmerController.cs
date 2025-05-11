using Microsoft.AspNetCore.Mvc;
using prog7311.Services;
using prog7311.Models;

namespace prog7311.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IFarmerService _farmerService;

        public FarmerController(IFarmerService farmerService)
        {
            _farmerService = farmerService;
        }

        public IActionResult List()
        {
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            var farmers = _farmerService.GetAllFarmers();
            return View(farmers);
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

            var (success, message) = _farmerService.AddFarmer(model);
            if (success)
            {
                TempData["Success"] = "Farmer added successfully!";
                return RedirectToAction("List");
            }
            
            ViewBag.Error = message;
            return View(model);
        }
    }
} 