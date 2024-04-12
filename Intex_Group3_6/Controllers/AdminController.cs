// Required namespaces for the controller's functionality.
using Intex_Group3_6.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex_Group3_6.Controllers;

// Inherits from Controller to handle HTTP requests and return responses.
public class AdminController : Controller
{
    // Repository interface for data operations.
    private IDataRepo _repo;

    // Provides access to HTTP context to manage session data.
    private readonly IHttpContextAccessor _sessionUserData;

    // Constructor with dependency injection for the data repository and session data accessor.
    public AdminController(IDataRepo repo, UserManager<IdentityUser> userManager, IHttpContextAccessor temp)
    {
        _repo = repo;
        _sessionUserData = temp;
    }

    // Retrieves and displays a paginated list of orders if the user is an admin.
    public IActionResult AdminOrders(int pageNum = 1)
    {
        // Attempts to retrieve user data from session.
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        // Redirects to login view if no user data found.
        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        // Checks if the user role is "Admin" and displays orders.
        if (userData.role == "Admin")
        {
            var model = _repo.GetOrders(pageNum);
            return View(model);
        }
        // Redirects to the home page if not an admin.
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    // Retrieves and displays a paginated list of users if the user is an admin.
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

    // Retrieves and displays a paginated list of products with adjustable page sizes if the user is an admin.
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
            // Sets selectable page size options for the view.
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

    // Stores selectable page size options.
    public IEnumerable<SelectListItem> PageSizes { get; set; }

    [HttpGet]
    public IActionResult EditProduct(int id)
    {
        // Attempt to retrieve the current user's data from the session.
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        // If no user data is available, redirect to the login view.
        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        // Check if the logged-in user has 'Admin' role.
        if (userData.role == "Admin")
        {
            // Fetch the product details from the repository using the provided ID.
            var product = _repo.GetProductById(id);
            // Return the EditProduct view populated with the product's details.
            return View(product);
        }
        else
        {
            // If the user is not an admin, redirect to the Index view.
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult EditProduct(Product product)
    {
        // Update the product in the repository with the provided product details.
        _repo.UpdateProduct(product);
        // Commit the changes to the database.
        _repo.SaveChanges();
        // Redirect to the AdminProducts view after the update.
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
        // Delete the specified product from the repository.
        _repo.DeleteProduct(product);
        // Save the changes to the database.
        _repo.SaveChanges();
        // Redirect to the AdminProducts view to show the updated product list.
        return RedirectToAction("AdminProducts");
    }

    [HttpGet]
    public IActionResult EditUser(int id)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        if (userData.role == "Admin")
        {
            // Fetch user details by user ID for editing.
            var user = _repo.GetUserById(id);
            return View(user);
        }
        else
        {
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult EditUser(User user)
    {
        // Update user information in the repository.
        _repo.UpdateUser(user);
        // Persist changes to the database.
        _repo.SaveChanges();
        // Redirect to the AdminUsers view.
        return RedirectToAction("AdminUsers");
    }

    [HttpGet]
    public IActionResult DeleteUser(int id)
    {
        // Attempts to retrieve the current user's data from the session.
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        // If no user data is found, the user is redirected to a login view.
        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        // Checks if the logged-in user has the 'Admin' role.
        if (userData.role == "Admin")
        {
            // Retrieves the user details from the repository using the provided ID.
            var user = _repo.GetUserById(id);
            // Returns the DeleteUser view, passing the user details for confirmation.
            return View(user);
        }
        else
        {
            // If the user is not an admin, redirects to the Index view.
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult DeleteUser(User user)
    {
        // Deletes the specified user from the repository.
        _repo.DeleteUser(user);
        // Saves the changes to the database.
        _repo.SaveChanges();
        // Redirects to the AdminUsers view to show the updated user list.
        return RedirectToAction("AdminUsers");
    }

    [HttpGet]
    public IActionResult DeleteOrder(int id)
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        // Ensures the user is logged in and has the necessary role.
        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        if (userData.role == "Admin")
        {
            // Retrieves the order details from the repository using the provided ID.
            var order = _repo.GetOrderById(id);
            // Returns the DeleteOrder view, passing the order details for confirmation.
            return View(order);
        }
        else
        {
            // Redirects to the Index view if the user is not authorized.
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult DeleteOrder(Order order)
    {
        // Deletes the specified order from the repository.
        _repo.DeleteOrder(order);
        // Commits the changes to the database.
        _repo.SaveChanges();
        // Redirects to the AdminOrders view to show the updated order list.
        return RedirectToAction("AdminOrders");
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        var userData = _sessionUserData.HttpContext.Session.GetJson<User>("UserData");

        // Checks if the user is logged in and has admin privileges.
        if (userData is null)
        {
            return View("PleaseLogIn");
        }

        if (userData.role == "Admin")
        {
            // Displays the AddProduct view for entering new product details.
            return View();
        }
        else
        {
            // Redirects to the Index view if the user is not authorized.
            return new ViewResult { ViewName = "Index" };
        }
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        // Adds a new product to the repository.
        _repo.AddProduct(product);
        // Saves the changes to the database.
        _repo.SaveChanges();
        // Redirects to the AdminProducts view to show the updated product list.
        return RedirectToAction("AdminProducts");
    }
}