using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Intex_Group3_6.Controllers
{
    public class ProductsController : Controller
    {
        private IDataRepo _repo;

        public ProductsController(IDataRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Products(string productType)
        {

            IQueryable<Product> productData = _repo.Products;

            // Filter products based on selected color or category
            if (!string.IsNullOrEmpty(productType))
            {
                productData = productData.Where(x => x.primaryColor == productType || x.category1 == productType);
            }

            // Order and take the top 9 products
            productData = productData.OrderBy(x => x.productName).Take(37);

            // Pass filtered product data to the view as IQueryable
            ViewData["ProductFilterOptions"] = new ProductFilterOptions
            {
                PrimaryColors = _repo.Products.Select(x => x.primaryColor).Distinct().ToList(),
                Categories = _repo.Products.Select(x => x.category1).Distinct().ToList()
            };



            return View(productData); // Pass productData directly
        }

        [HttpGet]
        public IActionResult ProductDetail(string productName)
        {
            ProductDetailViewModel productDetail = _repo.GetProductDetail(productName);

            return View("ProductDetail", productDetail);
        }
    }
}
