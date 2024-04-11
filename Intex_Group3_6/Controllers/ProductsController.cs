using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;  

namespace Intex_Group3_6.Controllers
{
    public class ProductsController : Controller
    {
        private IDataRepo _repo;

        public ProductsController(IDataRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Products()
        {
            var productData = _repo.Products
                .OrderBy(x => x.productName)
                .Take(9);

            return View(productData);
        }

        [HttpGet]
        public IActionResult ProductDetail(string productName)
        {
            ProductDetailViewModel productDetail = _repo.GetProductDetail(productName);

            return View("ProductDetail", productDetail);
        }
    }
}
