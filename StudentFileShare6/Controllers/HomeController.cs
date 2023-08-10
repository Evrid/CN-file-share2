using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using StudentFileShare6.data;
using StudentFileShare6.Hubs;
using StudentFileShare6.Models;
using System.Diagnostics;

namespace StudentFileShare6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
      //  private readonly DocumentContext _context;
      
     //   private readonly UniversityContext _universityContext;
     //   private readonly CourseContext _courseContext;

        public HomeController(ILogger<HomeController> logger, CourseContext courseContext, UniversityContext universityContext)
        {
            _logger = logger;
     //       _courseContext = courseContext;
      //      _universityContext = universityContext;
        }

        public IActionResult Index()
        {
            return View();
        }








        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
