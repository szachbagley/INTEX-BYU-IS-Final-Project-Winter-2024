using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex_Group3_6.Controllers
{
    // Inherits from Controller base class, typical for handling HTTP requests in MVC
    public class ProductsController : Controller
    {
        private IDataRepo _repo;  // Dependency injection to obtain a data repository

        // Constructor that initializes the data repository
        public ProductsController(IDataRepo repo)
        {
            _repo = repo;
        }

        // Action method for displaying a page of products
        // Accepts optional parameters for product type, page number, and number of items per page
        public IActionResult Products(string? productType, int pageNum, int pageSize)
        {
            // Get the queryable of all products
            IQueryable<Product> productData = _repo.Products;

            // Validate or set default values for pagination
            if (pageNum <= 0)
            {
                pageNum = 1;  // Set default page number
            }
            if (pageSize <= 0)
            {
                pageSize = 5;  // Set default page size
            }

            // Determine the total number of items depending on whether a product type has been specified
            int total = productType == null ? 
                        _repo.Products.Count() : 
                        _repo.Products.Where(x => x.primaryColor == productType || x.category1 == productType).Count();

            // Filter products based on the productType (by color or category)
            if (!string.IsNullOrEmpty(productType))
            {
                productData = productData.Where(x => x.primaryColor == productType || x.category1 == productType);
            }

            // Order the products by name, and paginate the results
            productData = productData.OrderBy(x => x.productName)
                                     .Skip((pageNum - 1) * pageSize)
                                     .Take(pageSize);

            // Prepare a list of product filter options for displaying in the view
            ViewData["ProductFilterOptions"] = new ProductFilterOptions
            {
                PrimaryColors = _repo.Products.Select(x => x.primaryColor).Distinct().ToList(),
                Categories = _repo.Products.Select(x => x.category1).Distinct().ToList()
            };

            // Construct the view model with pagination details and the filtered product data
            var productPage = new ProductPageViewModel
            {
                Products = productData,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = total,
                    CurrentProductType = productType
                },
                
            };

            // Return the view with the view model
            return View(productPage);
        }

        // Action method to display details for a specific product
        [HttpGet]
        public IActionResult ProductDetail(string productName)
        {
            // Retrieve product details using the product name
            ProductDetailViewModel productDetail = _repo.GetProductDetail(productName);

            // Render the ProductDetail view with the product details
            return View("ProductDetail", productDetail);
        }
    }
}
