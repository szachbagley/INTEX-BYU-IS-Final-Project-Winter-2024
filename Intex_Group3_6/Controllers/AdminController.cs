using Intex_Group3_6.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex_Group3_6.Controllers;


public class AdminController : Controller
{
    private IDataRepo _repo;
    private readonly IHttpContextAccessor _sessionUserData;

    public AdminController(IDataRepo repo, UserManager<IdentityUser> userManager, IHttpContextAccessor temp)
    {
        _repo = repo;
        _sessionUserData = temp;
    }
    
    
    public IActionResult AdminOrders(int pageNum = 1)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var model = _repo.GetOrders(pageNum);
            return View(model);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    public IActionResult AdminUsers(int pageNum = 1)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var model = _repo.GetUsers(pageNum);
            return View(model);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    public IActionResult AdminProducts(int pageNum = 1, int pageSize = 10)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var model = _repo.GetProducts(pageNum, pageSize);

            model.PaginationInfo.ItemsPerPage = pageSize;
            model.PageSizes = new SelectList(new[] { "10", "20", "50" }
                    .Select(x => new SelectListItem { Value = x, Text = x }),
                "Value", "Text", pageSize.ToString());

            return View(model);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }


    public IEnumerable<SelectListItem> PageSizes { get; set; }

    [HttpGet]
    public IActionResult EditProduct(int id)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var product = _repo.GetProductById(id);
            return View(product);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
        _repo.UpdateProduct(product);
        _repo.SaveChanges();
        return RedirectToAction("AdminProducts");
    }

    [HttpGet]
    public IActionResult DeleteProduct(int id)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var product = _repo.GetProductById(id);
            return View(product);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);
        _repo.SaveChanges();
        return RedirectToAction("AdminProducts");
    }

    [HttpGet]
    public IActionResult DeleteOrder(int id)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            var order = _repo.GetOrderById(id);
            return View(order);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult DeleteOrder(Order order)
    {
        _repo.DeleteOrder(order);
        _repo.SaveChanges();
        return RedirectToAction("AdminOrders");
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");
        
        if (userData is null)
        {
            return View("PleaseLogIn");
        }
        if (userData.role == "Admin")
        {
            return View();
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        _repo.AddProduct(product);
        _repo.SaveChanges();
        return RedirectToAction("AdminProducts");
    }
}