using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prog7311.Models;

/*
 * This is the Home Controller class. It is the entry controller
 * for the web app.
 */

namespace prog7311.Controllers;

public class HomeController : Controller
{
    // declare a logger instance 
    private readonly ILogger<HomeController> _logger;

    // Primary constructor
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Action method for the default route.
    // Returns the Index view
    public IActionResult Index()
    {
        return View();
    }

    // Action method for handling errors
    // Is disabled when the site is running in production
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
