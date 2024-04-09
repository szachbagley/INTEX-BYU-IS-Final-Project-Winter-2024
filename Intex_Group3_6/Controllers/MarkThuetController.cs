using Microsoft.AspNetCore.Mvc;

namespace Intex_Group3_6.Controllers
{
    public class MarkThuetController : Controller
    {
        public IActionResult About()
        {
            // Set the title in ViewData
            ViewData["Title"] = "About";

            // Return the About view
            return View();
        }
    }
}
