using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Only_test.Models;
using System.Diagnostics;

namespace Only_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Only_tContext db;
        public HomeController(Only_tContext context)
        {
            db = context;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task <IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}