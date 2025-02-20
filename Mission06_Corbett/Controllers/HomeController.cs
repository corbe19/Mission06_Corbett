using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Categories = _context.Categories
                .OrderBy(c => c.CategoryId)
                .ToList();

            return View("Movies", new Movie());
        }

        [HttpPost]
        public IActionResult Movies(Movie response)
        {
            if (ModelState.IsValid)
            {

                _context.Movies.Add(response); //Add record to db
                _context.SaveChanges(); //Save changes to db

                return View("Confirmation", response);
            }
            else 
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(c => c.CategoryId)
                    .ToList();

                return View(response);
            
            }
        }
        [HttpGet]
        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(m => m.Category)
                .OrderBy(m => m.Title).ToList();

            return View(movies);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Movies
                .Single(m => m.MovieId == id);

            ViewBag.Categories = _context.Categories.OrderBy(c => c.CategoryId).ToList();

            return View("Movies", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie UpdatedInfo)
        {
            _context.Movies.Update(UpdatedInfo);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var recordToDelete = _context.Movies
                .Single(m => m.MovieId == id);


            return View("ConfirmDelete", recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Movie recordToDelete)
        {
            _context.Movies.Remove(recordToDelete);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
