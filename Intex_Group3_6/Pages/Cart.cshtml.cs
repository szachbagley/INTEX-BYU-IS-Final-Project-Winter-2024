using Intex_Group3_6.Infrastructure;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Intex_Group3_6.Pages
{
    public class CartModel : PageModel
    {
        private IDataRepo _dataRepo;
        private UserManager<IdentityUser> _userManager;


        public CartModel(IDataRepo temp, UserManager<IdentityUser> userManager)
        {
            _dataRepo = temp;
            _userManager = userManager;
        }
        public Cart? Cart { get; set; }

        public User? LoggedInUser { get; set; }

        public string ReturnUrl { get; set; } = "/";



        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser != null) 
            {
                LoggedInUser = _dataRepo.GetUserByEmail(identityUser.Email);
            }
            if (LoggedInUser != null)
            {
                return Page();
            }
            else { return new ViewResult { ViewName = "PleaseLogIn" }; }

        }

        public async Task<IActionResult> OnPostAsync(int productId, string returnUrl)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser != null)
            {
                LoggedInUser = _dataRepo.GetUserByEmail(identityUser.Email);
            }
            Product product = _dataRepo.Products.
                FirstOrDefault(x => x.productId == productId);

            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            
            if (LoggedInUser != null)
            {
                return RedirectToPage(new { returnUrl = returnUrl });
            }
            else { return new ViewResult { ViewName = "PleaseLogIn" }; }
        }
    }
}
