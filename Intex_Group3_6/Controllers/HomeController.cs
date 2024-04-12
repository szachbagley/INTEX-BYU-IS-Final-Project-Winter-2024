using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.AccessControl;

using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using Microsoft.AspNetCore.Identity;


namespace Intex_Group3_6.Controllers;

public class HomeController : Controller
{
    private IDataRepo _repo;

    private UserManager<IdentityUser> _userManager;
    private readonly InferenceSession _session;

    public HomeController(IDataRepo repo, UserManager<IdentityUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
        _session = new InferenceSession("fraud_model3.onnx");
    }



    public IActionResult Index()
    {
        // Retrieve top 10 reviewed LEGO sets
        var top10Sets = _repo.GetRatingsWithPictures();

        return View(top10Sets);
    } 
    
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Predict(Order order)
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
        if (order.typeOfCard == "VISA")
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
                    ViewBag.Prediction = Convert.ToBoolean(prediction[0]) ? "1" : "0";
                }
                else
                {
                    ViewBag.Prediction = "Error: Unable to make a prediction.";
                    order.fraud = "Error"; // Set some error state
                }
            }
            // _logger.LogInformation("Prediction executed successfully.");
        }
        catch (Exception ex)
        {
            // _logger.LogError($"Error during prediction: {ex.Message}");
            ViewBag.Prediction = "Error during prediction.";
            order.fraud = "Error"; // Set some error state
        }

        return View("Index"); // Pass the updated order object to the Cart view
    }

    // Make sure to dispose of the ONNX session
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _session?.Dispose();
        }
        base.Dispose(disposing);
    }
    
    public IActionResult OrderConfirmation()
    {
        return View();
    }
}