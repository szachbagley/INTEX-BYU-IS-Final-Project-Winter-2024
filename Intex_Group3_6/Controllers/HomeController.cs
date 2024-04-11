using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;

namespace Intex_Group3_6.Controllers;

public class HomeController : Controller
{
    private IDataRepo _repo;
    private UserManager<IdentityUser> _userManager;


    public HomeController(IDataRepo repo, UserManager<IdentityUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }



    public IActionResult Index()
    {
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
        
    public async Task<IActionResult> UserDetailTestAsync()
    {
        var identityUser = await _userManager.GetUserAsync(User);
        if (identityUser != null)
        {
            var user = _repo.GetUserByEmail(identityUser.Email);
            return View(user);
        }
        else { return View("Index"); }
        

        

    }
}