using MVCFilms.Data;
using MVCFilms.Interfaces;
using MVCFilms.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCFilms.Repositories
{
    public class ActorRepository : IActor
    {
        public async Task AddActorAsync(Actor Actor)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                await context.Actors.AddAsync(Actor);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteActorAsync(Actor Actor)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Actors.Remove(Actor);
                await context.SaveChangesAsync();
            }
        }
        public async Task EditActorAsync(Actor Actor)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Actors.Update(Actor);
                await context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Actors.ToListAsync();
            }
        }
        public async Task<IEnumerable<Actor>> GetActorsByNameAsync(string name)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Actors.Where(e => e.Name.Contains(name)).ToListAsync();
            }
        }
        public async Task<IEnumerable<Actor>> GetActorsByCountAsync(int skip, int take)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Actors.Skip(skip).Take(take).ToListAsync();
            }
        }
        
        
        public async Task<Actor> GetActorAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Actors.FirstOrDefaultAsync(e => e.Id == id);
            }
        }
        public async Task<Actor> GetActorWithMoviesAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Actors.Include(e => e.Movies).FirstOrDefaultAsync(e => e.Id == id);
            }
        }
    }
}
