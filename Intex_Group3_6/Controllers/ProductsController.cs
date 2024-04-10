using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Intex_Group3_6.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private IDataRepo _repo;

        public ProductsController(IDataRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Products()
        {
            var productData = _repo.Products;

            return View(productData);
        }
    }
}
