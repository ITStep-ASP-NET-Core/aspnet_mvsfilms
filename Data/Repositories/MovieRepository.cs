using MVCFilms.Data;
using MVCFilms.Interfaces;
using MVCFilms.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCFilms.Repositories
{
    public class MovieRepository : IMovie
    {
        public async Task AddMovieAsync(Movie Movie)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                foreach (var actor in Movie.Actors)
                {
					context.Entry(actor).State = EntityState.Unchanged;
				}
				foreach(var author in Movie.Authors)
				{
					context.Entry(author).State = EntityState.Unchanged;
				}
				foreach(var genre in Movie.Genre)
				{
					context.Entry(genre).State = EntityState.Unchanged;
				}

				await context.Movies.AddAsync(Movie);
                await context.SaveChangesAsync();

			}
        }
        public async Task DeleteMovieAsync(Movie Movie)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Movies.Remove(Movie);
                await context.SaveChangesAsync();
            }
        }
		public async Task EditMovieAsync ( Movie movie )
		{
			using var context = Program.DbContext();

			var currentMovie = await context.Movies
				.Include(m => m.Actors)
				.Include(m => m.Authors)
				.Include(m => m.Genre)
				.FirstOrDefaultAsync(m => m.Id == movie.Id);

			if(currentMovie is null)
			{
				return;
			}

			bool hasChanges = false;

			if(currentMovie.Title != movie.Title ||
				currentMovie.Plot != movie.Plot ||
				currentMovie.Country != movie.Country ||
				currentMovie.Poster != movie.Poster ||
				currentMovie.TeaserUrl != movie.TeaserUrl ||
				currentMovie.Runtime != movie.Runtime ||
				currentMovie.Rating != movie.Rating ||
				currentMovie.ReleaseDate != movie.ReleaseDate ||
				currentMovie.ClosingDate != movie.ClosingDate)
			{
				hasChanges = true;
			}

			if(!currentMovie.Authors.Select(a => a.Id).OrderBy(id => id).ToList().SequenceEqual(movie.Authors.Select(a => a.Id).OrderBy(id => id).ToList()))
				hasChanges = true;
			if(!currentMovie.Actors.Select(a => a.Id).OrderBy(id => id).ToList().SequenceEqual(movie.Actors.Select(a => a.Id).OrderBy(id => id).ToList()))
				hasChanges = true;
			if(!currentMovie.Genre.Select(g => g.Id).OrderBy(id => id).ToList().SequenceEqual(movie.Genre.Select(g => g.Id).OrderBy(id => id).ToList()))
				hasChanges = true;

			if(!hasChanges)
			{
				return;
			}

			context.Entry(currentMovie).CurrentValues.SetValues(movie);

			var incomingAuthorIds = movie.Authors.Select(a => a.Id).ToHashSet();
			currentMovie.Authors.Clear();
			var authorsToAdd = await context.Authors
				.Where(a => incomingAuthorIds.Contains(a.Id))
				.ToListAsync();
			foreach(var author in authorsToAdd)
			{
				currentMovie.Authors.Add(author);
			}

			var incomingActorIds = movie.Actors.Select(a => a.Id).ToHashSet();
			currentMovie.Actors.Clear();
			var actorsToAdd = await context.Actors
				.Where(a => incomingActorIds.Contains(a.Id))
				.ToListAsync();
			foreach(var actor in actorsToAdd)
			{
				currentMovie.Actors.Add(actor);
			}

			var incomingGenreIds = movie.Genre.Select(g => g.Id).ToHashSet();
			currentMovie.Genre.Clear();
			var genresToAdd = await context.Genres
				.Where(g => incomingGenreIds.Contains(g.Id))
				.ToListAsync();
			foreach(var genre in genresToAdd)
			{
				currentMovie.Genre.Add(genre);
			}

			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.ToListAsync();
            }
        }
        public async Task<IEnumerable<Movie>> GetAllMoviesWithAuthorsAsync()
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.Include(e => e.Authors).ToListAsync();
            }
        }


        public async Task<IEnumerable<Movie>> GetMoviesByAuthorAsync(Author author)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.Where(e => e.Authors.Any(e => e.Id == author.Id)).ToListAsync();
            }
        }
        public async Task<IEnumerable<Movie>> GetMoviesByCountAsync(int skip, int take)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.Skip(skip).Take(take).ToListAsync();
            }
        }
        public async Task<IEnumerable<Movie>> GetMoviesByNameAsync(string name)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.Where(e => e.Title.Contains(name)).ToListAsync();
            }
        }


        public async Task<Movie> GetMovieAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.FirstOrDefaultAsync(e => e.Id == id);
            }
        }
        public async Task<Movie> GetMovieWithAllAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Movies.Include(e => e.Authors).Include(e => e.Actors).Include(e => e.Genre).FirstOrDefaultAsync(e => e.Id == id);
            }
        }

    }
}
