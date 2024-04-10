using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.AccessControl;

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
        // Retrieve top 10 reviewed LEGO sets
        IQueryable<Object> top10Sets = _repo.GetTop10ReviewedSets();

        return View(top10Sets);
    } 
    
    public IActionResult Privacy()
    {
        return View();
    }
}