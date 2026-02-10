using MVCFilms.Models;

namespace MVCFilms.Interfaces
{
    public interface IGenre
    {
        Task AddGenreAsync(Genre Genre);
        Task DeleteGenreAsync(Genre Genre);
        Task EditGenreAsync(Genre Genre);

        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<IEnumerable<Genre>> GetGenresByNameAsync(string name);
        Task<IEnumerable<Genre>> GetGenresByCountAsync(int skip, int take);

        Task<Genre> GetGenreAsync(int id);
        Task<Genre> GetGenreWithMoviesAsync(int id);
    }
}
