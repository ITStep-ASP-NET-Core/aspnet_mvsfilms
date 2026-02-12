using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFilms.Interfaces;
using MVCFilms.Models;
using MVCFilms.Repositories;
using System.Diagnostics;

namespace MVCFilms.Controllers
{
    public class MovieController : Controller
    {
		private readonly IMovie _movie;
		private readonly IActor _actor;
		private readonly IAuthor _author;
		private readonly IGenre _genre;

		public MovieController()
        {
			_movie = new MovieRepository();
			_actor = new ActorRepository();
			_author = new AuthorRepository();
			_genre = new GenreRepository();
		}

		public async Task<IActionResult> Index( )
		{
			return View(await _movie.GetAllMoviesAsync());
		}

		public async Task<IActionResult> Details (int? id)
        {
			if(id == null)
			{
				return NotFound();
			}

			var movie = await _movie.GetMovieWithAllAsync((int)id);

			if(movie == null)
			{
				return NotFound();
			}

			return View(movie);
        }


		public class CreateMovieViewModel
		{
			public string Title { get; set; }

			public DateTime ReleaseDate { get; set; }

			public DateTime? ClosingDate { get; set; }

			public int Runtime { get; set; }

			public string Plot { get; set; }

			public string Country { get; set; }

			public IFormFile ImageFile { get; set; }

			public string? TeaserUrl { get; set; }

			public decimal Rating { get; set; }

			public List<int> SelectedActorsIds { get; set; } = new List<int>();

			public List<int> SelectedAuthorsIds { get; set; } = new List<int>();			

			public List<int> SelectedGenresIds { get; set; } = new List<int>();
		}


		public async Task<IActionResult> Create( )
		{
			var authors = await _author.GetAllAuthorsAsync();
			ViewBag.Authors = authors.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});

			var actors = await _actor.GetAllActorsAsync();
			ViewBag.Actors = actors.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});

			var genres = await _genre.GetAllGenresAsync();
			ViewBag.Genres = genres.Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});
			
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create ( CreateMovieViewModel model )
		{
			if(ModelState.IsValid)
			{
				var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

				if(!allowedExtensions.Contains(Path.GetExtension(model.ImageFile.FileName).ToLower()))
						ModelState.AddModelError("ImageFile", "Разрешены только .jpg, .jpeg, .png");

				if(model.ImageFile.Length > 2_000_000)
						ModelState.AddModelError("ImageFile", "Файл слишком большой (макс 2 МБ)");

				var fileName = $"movie_image_{model.Title.Replace(" ", "_").ToLower().Trim()}{Path.GetExtension(model.ImageFile.FileName)}";
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

				using(var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(stream);
				}

				Console.WriteLine("\tActors");
				var actors = new List<Actor>();
				Console.WriteLine(model.SelectedActorsIds);
				Console.WriteLine(String.Join(", ", model.SelectedActorsIds));
				foreach (var actorId in model.SelectedActorsIds)
				{

					Console.WriteLine("before:");
					Console.WriteLine(actorId);
					var actor = await _actor.GetActorAsync(actorId);
					actors.Add(actor);
					Console.WriteLine("\nafter:");
					Console.WriteLine(actor);
				}
				
				var authors = new List<Author>();
				foreach(var authorId in model.SelectedAuthorsIds)
				{
					authors.Add(await _author.GetAuthorAsync(authorId));
				}
				
				var genres = new List<Genre>();
				foreach(var genreId in model.SelectedGenresIds)
				{
					genres.Add(await _genre.GetGenreAsync(genreId));
				}

				var movie = new Movie
				{
					Title = model.Title.Trim(),
					ReleaseDate = model.ReleaseDate,
					ClosingDate = model.ClosingDate,
					Runtime = model.Runtime,
					Plot = model.Plot,
					Country = model.Country,
					Poster = "/images/" + fileName,
					TeaserUrl = model.TeaserUrl,
					Rating = model.Rating,
					Actors = actors,
					Authors = authors,
					Genre = genres
				};


				await _movie.AddMovieAsync(movie);
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

	}
}
