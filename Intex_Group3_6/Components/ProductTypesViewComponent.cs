using Intex_Group3_6.Models;
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

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProductType = RouteData?.Values["productType"];
            var productTypes = new ProductFilterOptions
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

            return View(productTypes);
        }
    }
}