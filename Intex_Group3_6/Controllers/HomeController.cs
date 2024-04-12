using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.AccessControl;

using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using Microsoft.AspNetCore.Identity;
using Intex_Group3_6.Infrastructure;



namespace Intex_Group3_6.Controllers;

// Inherits from Controller, making it part of the MVC architecture to handle HTTP requests
public class HomeController : Controller
{
    // Private field to hold the repository interface for data operations
    private IDataRepo _repo;

    // Provides access to HTTP context, useful for managing sessions and other HTTP-specific functionalities
    private readonly IHttpContextAccessor _httpContextAccessor;

    // Manages user-related operations in ASP.NET Core Identity, like user creation, deletion, searching, etc.
    private UserManager<IdentityUser> _userManager;

    // Constructor that injects the required services
    public HomeController(IDataRepo repo, UserManager<IdentityUser> userManager, IHttpContextAccessor temp)
    {
        _repo = repo;
        _userManager = userManager;
        _httpContextAccessor = temp;
    }

    // Action method for handling requests to the home page
    public IActionResult Index()
    {
        // Retrieve user data from the session using a custom extension method GetJson
        var userData = _httpContextAccessor.HttpContext.Session.GetJson<User>("UserData");

        // If user data is found in the session, process it
        if (userData != null)
        { 
            var userId = userData.userId; // Extract the user ID from session data
            var recProducts = _repo.GetUserRec(userId).ToList(); // Get recommended products for the user
            ViewBag.recProducts = recProducts; // Pass recommended products to the view through ViewBag
        }
        
        // Retrieve top 10 reviewed LEGO sets from the repository
        var top10Sets = _repo.GetRatingsWithPictures();

        // Return the Index view with the top 10 sets as the model
        return View(top10Sets);
    } 
    
    // Action method for the Privacy policy page
    public IActionResult Privacy()
    {
        return View(); // Returns the Privacy view
    }

    // Action method for the About page
    public IActionResult About()
    {
        return View(); // Returns the About view
    }

    // Action method for the Order Confirmation page
    public IActionResult OrderConfirmation()
    {
        return View(); // Returns the OrderConfirmation view
    }
}
