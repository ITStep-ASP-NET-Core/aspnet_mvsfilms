using System.Diagnostics;
using MVCFilms.Models;
using Microsoft.AspNetCore.Mvc;
using MVCFilms.Repositories;

namespace MVCFilms.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var _movies = new MovieRepository();

            ViewBag.Movies = _movies.GetAllMoviesAsync().Result;

            return View();
        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
