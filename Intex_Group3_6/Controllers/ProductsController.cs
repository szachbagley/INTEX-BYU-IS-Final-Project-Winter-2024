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

        public IActionResult Products(string? productType, int pageNum, int pageSize)
        {
            IQueryable<Product> productData = _repo.Products;

            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            int total = productType == null ? _repo.Products.Count() : _repo.Products.Where(x => x.primaryColor == productType || x.category1 == productType).Count();

            // Filter products based on selected color or category
            if (!string.IsNullOrEmpty(productType))
            {
                productData = productData.Where(x => x.primaryColor == productType || x.category1 == productType);
            }

            // Order and take the top 9 products
            productData = productData.OrderBy(x => x.productName).Skip((pageNum - 1) * pageSize).Take(pageSize);

            // Pass filtered product data to the view as IQueryable
            ViewData["ProductFilterOptions"] = new ProductFilterOptions
            {
                PrimaryColors = _repo.Products.Select(x => x.primaryColor).Distinct().ToList(),
                Categories = _repo.Products.Select(x => x.category1).Distinct().ToList()
            };

            var productPage = new ProductPageViewModel
            {
                Products = productData,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = total
                },
                CurrentProductType = productType
            };
            return View(productPage); // Pass productData directly
        }

        [HttpGet]
        public IActionResult ProductDetail(string productName)
        {
            ProductDetailViewModel productDetail = _repo.GetProductDetail(productName);

            return View("ProductDetail", productDetail);
        }
    }
}
