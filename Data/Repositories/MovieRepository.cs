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
        public async Task EditMovieAsync(Movie Movie)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                var currentMovie = context.Movies
                    .Include(b => b.Authors)
                    .FirstOrDefault(b => b.Id == Movie.Id);

                if (currentMovie is null) return;

                context.Entry(currentMovie).CurrentValues.SetValues(Movie);

                var incomingAuthorIds = Movie.Authors.Select(a => a.Id).ToList();
                var updatedAuthors = context.Authors
                    .Where(a => incomingAuthorIds.Contains(a.Id))
                    .ToList();

                currentMovie.Authors = currentMovie.Authors.Where(a => !incomingAuthorIds.Contains(a.Id)).ToList();

                foreach (var author in updatedAuthors)
                {
                    if (!currentMovie.Authors.Any(a => a.Id == author.Id))
                    {
                        currentMovie.Authors.Add(author);
                    }
                }

                var incomingActorsIds = Movie.Actors.Select(a => a.Id).ToList();
                var updatedActors = context.Actors
                    .Where(a => incomingActorsIds.Contains(a.Id))
                    .ToList();

                currentMovie.Actors = currentMovie.Actors.Where(a => !incomingActorsIds.Contains(a.Id)).ToList();

                foreach (var actor in updatedActors)
                {
                    if (!currentMovie.Actors.Any(a => a.Id == actor.Id))
                    {
                        currentMovie.Actors.Add(actor);
                    }
                }

                var incomingGenreIds = Movie.Genre.Select(a => a.Id).ToList();
                var updatedGenres = context.Genres
                    .Where(a => incomingGenreIds.Contains(a.Id))
                    .ToList();

                currentMovie.Genre = currentMovie.Genre.Where(a => !incomingGenreIds.Contains(a.Id)).ToList();

                foreach (var genre in updatedGenres)
                {
                    if (!currentMovie.Genre.Any(a => a.Id == genre.Id))
                    {
                        currentMovie.Genre.Add(genre);
                    }
                }

                context.SaveChanges();
            }
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
