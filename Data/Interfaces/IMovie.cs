using MVCFilms.Models;

namespace MVCFilms.Interfaces
{
    public interface IMovie
    {
        Task AddMovieAsync(Movie Movie);
        Task DeleteMovieAsync(Movie Movie);
        Task EditMovieAsync(Movie Movie);

        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> GetAllMoviesWithAuthorsAsync();

        Task<IEnumerable<Movie>> GetMoviesByAuthorAsync(Author author);
        Task<IEnumerable<Movie>> GetMoviesByCountAsync(int skip, int take);
        Task<IEnumerable<Movie>> GetMoviesByNameAsync(string name);

        Task<Movie> GetMovieAsync(int id);
        Task<Movie> GetMovieWithAllAsync(int id);

    }
}
