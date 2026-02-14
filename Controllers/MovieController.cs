using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFilms.Interfaces;
using MVCFilms.Models;
using MVCFilms.Repositories;

namespace MVCFilms.Controllers
{
	public class MovieController : Controller
	{
		private readonly IMovie _movie;
		private readonly IActor _actor;
		private readonly IAuthor _author;
		private readonly IGenre _genre;

		public MovieController ( )
		{
			_movie = new MovieRepository();
			_actor = new ActorRepository();
			_author = new AuthorRepository();
			_genre = new GenreRepository();
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
		public class EditMovieViewModel
		{
			public int? Id { get; set; }
			public string Title { get; set; }

			public DateTime ReleaseDate { get; set; }

			public DateTime? ClosingDate { get; set; }

			public int Runtime { get; set; }

			public string Plot { get; set; }

			public string Country { get; set; }

			public IFormFile? ImageFile { get; set; }

			public string? TeaserUrl { get; set; }

			public decimal Rating { get; set; }

			public List<int> SelectedActorsIds { get; set; } = new List<int>();

			public List<int> SelectedAuthorsIds { get; set; } = new List<int>();

			public List<int> SelectedGenresIds { get; set; } = new List<int>();
		}

		public async Task<IActionResult> Index ( )
		{
			return View(await _movie.GetAllMoviesAsync());
		}

		public async Task<IActionResult> Details ( int? id )
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

		public async Task<IActionResult> Create ( )
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
		public async Task<IActionResult> Create ( CreateMovieViewModel movieModel )
		{
			if(ModelState.IsValid)
			{
				var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

				if(!allowedExtensions.Contains(Path.GetExtension(movieModel.ImageFile.FileName).ToLower()))
					ModelState.AddModelError("ImageFile", "Allowed only  .jpg, .jpeg, .png");

				if(movieModel.ImageFile.Length > 2_000_000)
					ModelState.AddModelError("ImageFile", "File is too big");

				var fileName = $"movie_image_{movieModel.Title.Replace(" ", "_").ToLower().Trim()}{Path.GetExtension(movieModel.ImageFile.FileName)}";
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

				using(var stream = new FileStream(filePath, FileMode.Create))
				{
					await movieModel.ImageFile.CopyToAsync(stream);
				}

				Console.WriteLine("\tActors");
				var actors = new List<Actor>();
				Console.WriteLine(movieModel.SelectedActorsIds);
				Console.WriteLine(String.Join(", ", movieModel.SelectedActorsIds));
				foreach(var actorId in movieModel.SelectedActorsIds)
				{

					Console.WriteLine("before:");
					Console.WriteLine(actorId);
					var actor = await _actor.GetActorAsync(actorId);
					actors.Add(actor);
					Console.WriteLine("\nafter:");
					Console.WriteLine(actor);
				}

				var authors = new List<Author>();
				foreach(var authorId in movieModel.SelectedAuthorsIds)
				{
					authors.Add(await _author.GetAuthorAsync(authorId));
				}

				var genres = new List<Genre>();
				foreach(var genreId in movieModel.SelectedGenresIds)
				{
					genres.Add(await _genre.GetGenreAsync(genreId));
				}

				var movie = new Movie
				{
					Title = movieModel.Title.Trim(),
					ReleaseDate = movieModel.ReleaseDate,
					ClosingDate = movieModel.ClosingDate,
					Runtime = movieModel.Runtime,
					Plot = movieModel.Plot,
					Country = movieModel.Country,
					Poster = "/images/" + fileName,
					TeaserUrl = movieModel.TeaserUrl,
					Rating = movieModel.Rating,
					Actors = actors,
					Authors = authors,
					Genre = genres
				};


				await _movie.AddMovieAsync(movie);
				return RedirectToAction(nameof(Index));
			}
			return View(movieModel);
		}

		public async Task<IActionResult> Edit ( int? id )
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

			var movieModel = new EditMovieViewModel {
				Id = movie.Id,
				Title = movie.Title,
				ReleaseDate = movie.ReleaseDate,
				ClosingDate = movie.ClosingDate,
				Runtime = movie.Runtime,
				Plot = movie.Plot,
				Country = movie.Country,
				TeaserUrl = movie.TeaserUrl,
				Rating = movie.Rating,
				SelectedActorsIds = [.. movie.Actors.Select(e => e.Id)],
				SelectedAuthorsIds = [.. movie.Authors.Select(e => e.Id)],
				SelectedGenresIds = [.. movie.Genre.Select(e => e.Id)]
			};

			return View(movieModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit ( int id, EditMovieViewModel movieModel )
		{
			if(id != movieModel.Id)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					var originalMovie = await _movie.GetMovieWithAllAsync(id);

					var editedMovie = new Movie
					{
						Id = id,
						Title = movieModel.Title.Trim(),
						ReleaseDate = movieModel.ReleaseDate,
						ClosingDate = movieModel.ClosingDate,
						Runtime = movieModel.Runtime,
						Plot = movieModel.Plot,
						Country = movieModel.Country,
						Poster = originalMovie.Poster,
						TeaserUrl = movieModel.TeaserUrl,
						Rating = movieModel.Rating,
						Actors = originalMovie.Actors,
						Authors = originalMovie.Authors,
						Genre = originalMovie.Genre
					};

					if(movieModel.ImageFile != null)
					{
						var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

						if(!allowedExtensions.Contains(Path.GetExtension(movieModel.ImageFile.FileName).ToLower()))
							ModelState.AddModelError("ImageFile", "Allowed only .jpg, .jpeg, .png");

						if(movieModel.ImageFile.Length > 2_000_000)
							ModelState.AddModelError("ImageFile", "File is too big");

						var fileName = $"movie_image_{movieModel.Title.Replace(" ", "_").ToLower().Trim()}{Path.GetExtension(movieModel.ImageFile.FileName)}";
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

						using(var stream = new FileStream(filePath, FileMode.Create))
						{
							await movieModel.ImageFile.CopyToAsync(stream);
						}

						editedMovie.Poster = "/images/" + fileName;
					}

					if (!originalMovie.Actors.Select(e => e.Id).OrderBy(x => x)
						   .SequenceEqual(movieModel.SelectedActorsIds.OrderBy(x => x)))
					{
						var actors = new List<Actor>();
						foreach(var actorInfo in movieModel.SelectedActorsIds)
						{
							actors.Add(await _actor.GetActorAsync(actorInfo));
						}
						editedMovie.Actors = actors;
					}


					if(!originalMovie.Authors.Select(e => e.Id).OrderBy(x => x)
						   .SequenceEqual(movieModel.SelectedAuthorsIds.OrderBy(x => x)))
					{
						var authors = new List<Author>();
						foreach(var authorInfo in movieModel.SelectedAuthorsIds)
						{
							authors.Add(await _author.GetAuthorAsync(authorInfo));
						}
						editedMovie.Authors = authors;
					}

					if(!originalMovie.Genre.Select(e => e.Id).OrderBy(x => x)
						   .SequenceEqual(movieModel.SelectedGenresIds.OrderBy(x => x)))
					{
						var genres = new List<Genre>();
						foreach(var genreInfo in movieModel.SelectedGenresIds)
						{
							genres.Add(await _genre.GetGenreAsync(genreInfo));
						}
						editedMovie.Genre = genres;
					}

					await _movie.EditMovieAsync(editedMovie);
				}
				catch(DbUpdateConcurrencyException)
				{
					return NotFound();
				}
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Authors = (await _author.GetAllAuthorsAsync()).Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});

			ViewBag.Actors = (await _actor.GetAllActorsAsync()).Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});

			ViewBag.Genres = (await _genre.GetAllGenresAsync()).Select(a => new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name
			});
			return View(movieModel);
		}

		public async Task<IActionResult> Delete ( int? id )
		{
			if(id == null)
			{
				return NotFound();
			}

			var movie = await _movie.GetMovieAsync((int)id);

			if(movie == null)
			{
				return NotFound();
			}

			return View(movie);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed ( int id )
		{
			var movie = await _movie.GetMovieAsync((int)id);

			if(movie != null)
			{
				await _movie.DeleteMovieAsync(movie);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
