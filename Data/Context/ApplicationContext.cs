using Microsoft.EntityFrameworkCore;
using MVCFilms.Models;

namespace MVCFilms.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Movie>(builder =>
			{
				builder.HasKey(a => a.Id);
			});
			modelBuilder.Entity<Movie>()
                        .HasMany<Author>(s => s.Authors)
                        .WithMany(c => c.Movies)
                        .UsingEntity(e => e.ToTable("MovieAuthor"));
            modelBuilder.Entity<Movie>()
                        .HasMany<Actor>(s => s.Actors)
                        .WithMany(c => c.Movies)
                        .UsingEntity(e => e.ToTable("MovieActor"));
            modelBuilder.Entity<Movie>()
                        .HasMany<Genre>(s => s.Genre)
                        .WithMany(c => c.Movies)
                        .UsingEntity(e => e.ToTable("MovieGenre"));

            modelBuilder.Entity<Author>(builder =>
            {
                builder.HasKey(a => a.Id);
            });

            modelBuilder.Entity<Actor>(builder =>
            {
                builder.HasKey(a => a.Id);
            });
        }

    }
}
