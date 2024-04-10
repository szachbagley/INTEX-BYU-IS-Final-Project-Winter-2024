using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Intex_Group3_6.Controllers
{
    public class MarkThuetController : Controller
    {
        private IDataRepo _repo;

        public MarkThuetController(IDataRepo repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            // Retrieve top 10 reviewed LEGO sets
            var top10Sets = _repo.GetTop10ReviewedSets();

            return View(top10Sets);
        }
    }
}
