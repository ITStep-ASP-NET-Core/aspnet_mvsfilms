using System.Diagnostics;
using MVCFilms.Models;
using Microsoft.AspNetCore.Mvc;
using MVCFilms.Repositories;

namespace MVCFilms.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Details (int id)
        {

            var _movies = new MovieRepository();

            ViewBag.Movie = _movies.GetMovieWithAllAsync(id).Result;

            return View();
        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
