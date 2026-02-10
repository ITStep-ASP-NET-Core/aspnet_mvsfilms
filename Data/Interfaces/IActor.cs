using MVCFilms.Models;

namespace MVCFilms.Interfaces
{
    public interface IActor
    {
        Task AddActorAsync(Actor Actor);
        Task DeleteActorAsync(Actor Actor);
        Task EditActorAsync(Actor Actor);

        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<IEnumerable<Actor>> GetActorsByNameAsync(string name);
        Task<IEnumerable<Actor>> GetActorsByCountAsync(int skip, int take);

        Task<Actor> GetActorAsync(int id);
        Task<Actor> GetActorWithMoviesAsync(int id);
    }
}
