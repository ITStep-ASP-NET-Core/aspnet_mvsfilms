using MVCFilms.Models;

namespace MVCFilms.Interfaces
{
    public interface IAuthor
    {
        Task AddAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
        Task EditAuthorAsync(Author author);

        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name);
        Task<IEnumerable<Author>> GetAuthorsByCountAsync(int skip, int take);

        Task<Author> GetAuthorAsync(int id);
        Task<Author> GetAuthorWithMoviesAsync(int id);
    }
}
