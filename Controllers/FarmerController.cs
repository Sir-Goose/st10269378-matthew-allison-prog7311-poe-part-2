using Microsoft.AspNetCore.Mvc;
using prog7311.Services;
using prog7311.Models;

/*
 * This is the Farmer Controller, it is responsible for handling
 * all of the farmer related website actions.
 */

namespace prog7311.Controllers
{
    public class FarmerController : Controller
    {
        // Declaring the farmer service
        private readonly IFarmerService _farmerService;

        // Primary constructor
        public FarmerController(IFarmerService farmerService)
        {
            _farmerService = farmerService;
        }

        // Returns a view with a list of all the farmers if the
        // user happens to be logged in as an employee
        public IActionResult List()
        {
            // check user is an employee
            if (Request.Cookies["UserRole"] != "Employee")
                // if not redirect to login page
                return RedirectToAction("Login", "Account");

            // get list of all the farmers
            var farmers = _farmerService.GetAllFarmers();
            return View(farmers); // return the view
        }

        // Get method to display the form for adding new farmers
        [HttpGet]
        public IActionResult Add()
        {
            // check that the user has the employee cookie
            if (Request.Cookies["UserRole"] != "Employee")
                // if not then redirect to login
                return RedirectToAction("Login", "Account");
            // return the add farmer view with a new farmer model
            return View(new Farmer());
        }

        // Post method to handle the submission of the add farmer form.
        [HttpPost]
        public IActionResult Add(Farmer model)
        {
            // check that the user is an employee
            if (Request.Cookies["UserRole"] != "Employee")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid) // validate the model state
            {
                // Log the validation errors if any
                Console.WriteLine("POST Add: ModelState is INVALID.");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Console.WriteLine($"  Field: {key}");
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"    Error: {error.ErrorMessage}");
                        }
                    }
                }
                // Pass on the error messages to the view
                ViewBag.Error = "Validation failed.";
                return View(model);
            }

            // add the farmer using the farmer service
            var (success, message) = _farmerService.AddFarmer(model);
            if (success)
            {
                TempData["Success"] = "Farmer added successfully!";
                return RedirectToAction("List");
            }
            
            // if error pass it on to the view
            ViewBag.Error = message;
            return View(model);
        }
    }
} 