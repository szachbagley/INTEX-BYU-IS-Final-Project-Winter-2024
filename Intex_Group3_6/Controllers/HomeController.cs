using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;

namespace Intex_Group3_6.Controllers;

public class HomeController : Controller
{
    private IDataRepo _repo;

    public HomeController(IDataRepo repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        return View();
    } 
    
    public IActionResult Privacy()
    {
        return View();
    }
}