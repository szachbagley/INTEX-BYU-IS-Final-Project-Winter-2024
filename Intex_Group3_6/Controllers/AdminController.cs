using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        model.PaginationInfo.ItemsPerPage = pageSize;
        model.PageSizes = new SelectList(new[] { "10", "20", "50" }
                .Select(x => new SelectListItem { Value = x, Text = x}),
    "Value", "Text", pageSize.ToString());
        
        return View(model);
    }

    public IEnumerable<SelectListItem> PageSizes { get; set; }
}