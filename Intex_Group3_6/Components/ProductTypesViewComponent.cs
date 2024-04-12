using Intex_Group3_6.Models.ViewModels;
﻿// Necessary namespaces for functionality
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

// Define the namespace this component belongs to
namespace Intex_Group3_6.Components
{
    // Define a view component which is a reusable component in ASP.NET MVC
    public class ProductTypesViewComponent : ViewComponent
    {
        // A private field to hold a reference to the data repository interface
        private IDataRepo _dataRepo;

        // Constructor that accepts a dependency injected data repository
        public ProductTypesViewComponent(IDataRepo temp)
        {
            _dataRepo = temp;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductPageViewModel model)
        // The default method that is invoked when the component is used in a view
        {
            // Store the current 'productType' from the route data into ViewBag for access in the view
            ViewBag.SelectedProductType = RouteData?.Values["productType"];
            model.FilterOptions = new ProductFilterOptions
            // Create a new instance of ProductFilterOptions containing distinct colors and categories from products
            {
                // Query the data repository for products, select distinct primary colors, and convert to a list
                PrimaryColors = _dataRepo.Products
                    .Select(x => x.primaryColor)
                    .Distinct()
                    .ToList(),
                
                // Query the data repository for products, select distinct categories, and convert to a list
                Categories = _dataRepo.Products
                    .Select(x => x.category1)
                    .Distinct()
                    .ToList()
            };

            return View(model);
            // Return the view for this component, passing the productTypes model to the view
        }
    }
}