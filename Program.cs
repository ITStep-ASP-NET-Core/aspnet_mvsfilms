
using MVCFilms.Data;
using MVCFilms.Repositories;

public class Program
{
	public static ApplicationContext DbContext ( ) => new ApplicationContextFactory().CreateDbContext();

	static void Initialize()
    {
        new DbInit().Init(DbContext());
    }

    public static void Main(string[] args)
    {
        using(ApplicationContext db = DbContext()) {
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();
		}

        Initialize();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
			
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}")
                .WithStaticAssets();
        app.MapControllerRoute(
				name: "movie",
				pattern: "Movie/{id:int}",
				defaults: new { controller = "Movie", action = "Details" }
        );

        app.Run();
    }
}