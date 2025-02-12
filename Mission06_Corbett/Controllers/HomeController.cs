using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Corbett.Models;

namespace Mission06_Corbett.Controllers
{
    public class HomeController : Controller
    {
        private MoviesContext _context;

        public HomeController(MoviesContext temp) //Constructor
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        public IActionResult Movies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Movies(Movie response)
        {

            _context.Movies.Add(response); //Add record to db
            _context.SaveChanges(); //Save changes to db

            return View("Confirmation", response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
