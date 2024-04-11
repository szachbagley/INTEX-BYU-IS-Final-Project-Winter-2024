using Intex_Group3_6.Infrastructure;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Intex_Group3_6.Pages
{
    public class CartModel : PageModel
    {
        private IDataRepo _dataRepo;


        public CartModel(IDataRepo temp)
        {
            _dataRepo = temp;
        }
        public Cart? Cart { get; set; }

        public string ReturnUrl { get; set; } = "/";



        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product product = _dataRepo.Products.
                FirstOrDefault(x => x.productId == productId);

            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new { returnUrl = returnUrl });
           
        }
    }
}
