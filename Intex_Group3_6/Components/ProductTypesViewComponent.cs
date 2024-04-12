using Intex_Group3_6.Models;
using Intex_Group3_6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Intex_Group3_6.Components
{
    public class ProductTypesViewComponent : ViewComponent
    {
        private IDataRepo _dataRepo;


        public ProductTypesViewComponent(IDataRepo temp)
        {
            _dataRepo = temp;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductPageViewModel model)
        {
            ViewBag.SelectedProductType = RouteData?.Values["productType"];
            model.FilterOptions = new ProductFilterOptions
            {
                PrimaryColors = _dataRepo.Products
                    .Select(x => x.primaryColor)
                    .Distinct()
                    .ToList(),
                Categories = _dataRepo.Products
                    .Select(x => x.category1)
                    .Distinct()
                    .ToList()
            };

            return View(model);
        }
    }
}