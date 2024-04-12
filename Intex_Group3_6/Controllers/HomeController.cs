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

public class HomeController : Controller
{
    private IDataRepo _repo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private UserManager<IdentityUser> _userManager;

    public HomeController(IDataRepo repo, UserManager<IdentityUser> userManager, IHttpContextAccessor temp)
    {
        _repo = repo;
        _userManager = userManager;
        _httpContextAccessor = temp;
    }



    public IActionResult Index()
    {
        var userData = _httpContextAccessor.HttpContext.Session.GetJson<User>("UserData");

        if (userData != null)
        { 
            var userId = userData.userId;
            var recProducts = _repo.GetUserRec(userId).ToList();
            ViewBag.recProducts = recProducts; 

        }
        // Retrieve top 10 reviewed LEGO sets
        var top10Sets = _repo.GetRatingsWithPictures();

        return View(top10Sets);
    } 
    
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult OrderConfirmation()
    {
        return View();
    }
}