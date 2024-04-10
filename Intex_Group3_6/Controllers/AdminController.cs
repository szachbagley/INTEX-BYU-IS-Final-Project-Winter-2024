using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;

namespace Intex_Group3_6.Controllers;


public class AdminController : Controller
{
    private IDataRepo _repo;

    public AdminController(IDataRepo repo)
    {
        _repo = repo;
    }

    public IActionResult AdminOrders(int pageNum = 1)
    {
        var model = _repo.GetOrders(pageNum);

        return View(model);
    }
    
    public IActionResult AdminUsers(int pageNum = 1)
    {
        var model = _repo.GetUsers(pageNum);

        return View(model);
    }
    
    public IActionResult AdminProducts(int pageNum = 1, int pageSize = 10)
    {
        var model = _repo.GetProducts(pageNum, pageSize);
        ViewBag.PageSize = pageSize;
        return View(model);
    }
}