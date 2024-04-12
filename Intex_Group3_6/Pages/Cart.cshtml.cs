using Intex_Group3_6.Infrastructure;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Intex_Group3_6.Pages
{
    public class CartModel : PageModel, IDisposable
    {
        private IDataRepo _dataRepo;
        private UserManager<IdentityUser> _userManager;
        private readonly InferenceSession _session;


        public CartModel(IDataRepo temp, UserManager<IdentityUser> userManager)
        {
            _dataRepo = temp;
            _userManager = userManager;
            _session = new InferenceSession("fraud_model3.onnx");
        }
        public Cart? Cart { get; set; }

        public User? LoggedInUser { get; set; }

        public Order? Order { get; set; }

        public List<Cart.CartLine> CartLines { get; set; }

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

        public IActionResult OnPostCheckout(Order order, List<Cart.CartLine> cartLines)
        {
            DateTime datetime = DateTime.Now;
            order.transactionDate = datetime;
            order.dayOfWeek = datetime.DayOfWeek.ToString();
            order.time = datetime.Hour;
            order = Predict(order);

            _dataRepo.AddOrder(order);
            _dataRepo.SaveChanges();
            for (int i = 0; i < cartLines.Count; i++)
            {
                LineItem item = new LineItem
                {
                    TransactionId = order.transactionId,
                    ProductId = cartLines[i].Product.productId,
                    quantity = cartLines[i].Quantity
                };
                _dataRepo.AddLineItem(item);
            }
            _dataRepo.SaveChanges();
            return new ViewResult
            {
                ViewName = "OrderConfirmation",
                ViewData = new ViewDataDictionary<Order>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = order
                }
            };
        }

        public Order Predict(Order order)
        {
            // create variables to add to the list
            var amount = (float)0;
            var day_of_week_Mon = 0;
            var day_of_week_Sat = 0;
            var day_of_week_Sun = 0;
            var day_of_week_Thu = 0;
            var day_of_week_Tue = 0;
            var day_of_week_Wed = 0;
            var entry_mode_PIN = 0; //will always be 0
            var entry_mode_Tap = 0; //will always be 0
            var type_of_transaction_Online = 1; //will always be 1
            var type_of_transaction_POS = 0;
            var shipping_address_India = 0;
            var shipping_address_Russia = 0;
            var shipping_address_USA = 0;
            var shipping_address_UnitedKingdom = 0;
            var bank_HSBC = 0;
            var bank_Halifax = 0;
            var bank_Lloyds = 0;
            var bank_Metro = 0;
            var bank_Monzo = 0;
            var bank_RBS = 0;
            var type_of_card_Visa = 0;

            //logic to assign variables the correct number based on the input from the order object

            //amount
            if (order.transactionAmount != null)
            {
                amount = (float)order.transactionAmount;
            }
            //days of the week
            if (order.dayOfWeek == "Monday")
            {
                day_of_week_Mon = 1;
            }
            else if (order.dayOfWeek == "Saturday")
            {
                day_of_week_Sat = 1;
            }
            else if (order.dayOfWeek == "Sunday")
            {
                day_of_week_Sun = 1;
            }
            else if (order.dayOfWeek == "Thursday")
            {
                day_of_week_Thu = 1;
            }
            else if (order.dayOfWeek == "Tuesday")
            {
                day_of_week_Tue = 1;
            }
            else if (order.dayOfWeek == "Wednesday")
            {
                day_of_week_Wed = 1;
            }

            //shipping address
            if (order.shippingAddress == "India")
            {
                shipping_address_India = 1;
            }
            else if (order.shippingAddress == "Russia")
            {
                shipping_address_Russia = 1;
            }
            else if (order.shippingAddress == "United States")
            {
                shipping_address_USA = 1;
            }
            else if (order.shippingAddress == "United Kingdom")
            {
                shipping_address_UnitedKingdom = 1;
            }

            //bank
            if (order.bank == "HSBC")
            {
                bank_HSBC = 1;
            }
            else if (order.bank == "Halifax")
            {
                bank_Halifax = 1;
            }
            else if (order.bank == "Lloyds")
            {
                bank_Lloyds = 1;
            }
            else if (order.bank == "Metro")
            {
                bank_Metro = 1;
            }
            else if (order.bank == "Monzo")
            {
                bank_Monzo = 1;
            }
            else if (order.bank == "RBS")
            {
                bank_RBS = 1;
            }

            //type of card
            if (order.typeOfCard == "Visa")
            {
                type_of_card_Visa = 1;
            }

            // You will need to preprocess your input data (order) to match the feature order expected by the ONNX model.
            var input = new List<float>
        {
            amount,
            day_of_week_Mon,
            day_of_week_Sat,
            day_of_week_Sun,
            day_of_week_Thu,
            day_of_week_Tue,
            day_of_week_Wed,
            entry_mode_PIN,
            entry_mode_Tap,
            type_of_transaction_Online,
            type_of_transaction_POS,
            shipping_address_India,
            shipping_address_Russia,
            shipping_address_USA,
            shipping_address_UnitedKingdom,
            bank_HSBC,
            bank_Halifax,
            bank_Lloyds,
            bank_Metro,
            bank_Monzo,
            bank_RBS,
            type_of_card_Visa
        };

            // Now create a tensor from this list. Adjust the dimensions if necessary.
            var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

            // Wrap the tensor into an ONNX value and create the input list
            var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("float_input",
                inputTensor) // make sure "float_input" matches the actual input name in your ONNX model
        };

            // Run the prediction
            try
            {
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    if (prediction != null && prediction.Length > 0)
                    {
                        // Set order.fraud to "1" for fraud or "0" for not fraud
                        order.fraud = Convert.ToBoolean(prediction[0]) ? "1" : "0";
                    }
                    else
                    {
                        order.fraud = "Error"; // Set some error state
                    }
                }
                // _logger.LogInformation("Prediction executed successfully.");
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Error during prediction: {ex.Message}");

                order.fraud = "Error"; // Set some error state
            }

            return order; // Pass the updated order object to the Cart view
        }

        // Make sure to dispose of the ONNX session
        public void Dispose()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }
        protected  void Dispose(bool disposing)
        {
            if (disposing)
            {
                _session?.Dispose();
            }
            Dispose(disposing);
        }

        
    }
}
