using MVCFilms.Data;
using MVCFilms.Interfaces;
using MVCFilms.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCFilms.Repositories
{
    public class GenreRepository : IGenre
    {
        public async Task AddGenreAsync(Genre Genre)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                await context.Genres.AddAsync(Genre);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteGenreAsync(Genre Genre)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Genres.Remove(Genre);
                await context.SaveChangesAsync();
            }
        }
        public async Task EditGenreAsync(Genre Genre)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                context.Genres.Update(Genre);
                await context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Genres.ToListAsync();
            }
        }
        public async Task<IEnumerable<Genre>> GetGenresByNameAsync(string name)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Genres.Where(e => e.Name.Contains(name)).ToListAsync();
            }
        }
        public async Task<IEnumerable<Genre>> GetGenresByCountAsync(int skip, int take)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Genres.Skip(skip).Take(take).ToListAsync();
            }
        }
        
        
        public async Task<Genre> GetGenreAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Genres.FirstOrDefaultAsync(e => e.Id == id);
            }
        }
        public async Task<Genre> GetGenreWithMoviesAsync(int id)
        {
            using (ApplicationContext context = Program.DbContext())
            {
                return await context.Genres.Include(e => e.Movies).FirstOrDefaultAsync(e => e.Id == id);
            }
        }
    }
}
