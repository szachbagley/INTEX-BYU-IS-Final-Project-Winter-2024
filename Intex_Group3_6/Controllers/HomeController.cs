using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;

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
        var joinedData = from AvgRating in _repo.
                         join Product in _context.Products
                         on AvgRating.productId equals Product.productId
                         select new
                         {
                             ProductName = Product.productName,
                             ProductImg = Product.imgLink,
                             ProductPrice = Product.price,
                             ProductId = Product.productId,
                             AvgRating = AvgRating.avgRating
                         };

        var joinedList = joinedData.ToArray();




        var top10 = joinedList.OrderByDescending(r => r.AvgRating).Take(10);

        return View(top10);
    } 
    
    public IActionResult Privacy()
    {
        return View();
    }
}