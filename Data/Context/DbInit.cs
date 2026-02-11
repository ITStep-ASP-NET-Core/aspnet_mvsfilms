using MVCFilms.Models;

namespace MVCFilms.Data
{
	public class DbInit
	{
		public void Init ( ApplicationContext context )
		{
			// Сначала Authors (писатели, режиссёры как авторы/сценаристы)
			if(!context.Authors.Any())
			{
				context.Authors.AddRange(
					new Author { Name = "Stephen King" },
					new Author { Name = "Frank Darabont" },
					new Author { Name = "Mario Puzo" },
					new Author { Name = "Francis Ford Coppola" },
					new Author { Name = "Christopher Nolan" },
					new Author { Name = "Jonathan Nolan" },
					new Author { Name = "David S. Goyer" },
					new Author { Name = "Thomas Keneally" },
					new Author { Name = "Steven Spielberg" },
					new Author { Name = "J.R.R. Tolkien" },
					new Author { Name = "Peter Jackson" },
					new Author { Name = "Quentin Tarantino" },
					new Author { Name = "Winston Groom" },
					new Author { Name = "Robert Zemeckis" },
					new Author { Name = "Chuck Palahniuk" },
					new Author { Name = "David Fincher" },
					new Author { Name = "Reginald Rose" }
				);
				context.SaveChanges();
			}

			// Actors (основные + несколько дополнительных для полноты)
			if(!context.Actors.Any())
			{
				context.Actors.AddRange(
					new Actor { Name = "Tom Hanks" },
					new Actor { Name = "Matthew McConaughey" },
					new Actor { Name = "Michael Clarke Duncan" },
					new Actor { Name = "Michael Caine" },
					new Actor { Name = "David Morse" },
					new Actor { Name = "Tim Robbins" },
					new Actor { Name = "Morgan Freeman" },
					new Actor { Name = "Anne Hathaway" },
					new Actor { Name = "Marlon Brando" },
					new Actor { Name = "Al Pacino" },
					new Actor { Name = "Christian Bale" },
					new Actor { Name = "Heath Ledger" },
					new Actor { Name = "Liam Neeson" },
					new Actor { Name = "Ralph Fiennes" },
					new Actor { Name = "Elijah Wood" },
					new Actor { Name = "Viggo Mortensen" },
					new Actor { Name = "John Travolta" },
					new Actor { Name = "Samuel L. Jackson" },
					new Actor { Name = "Matt Damon" },
					new Actor { Name = "Robin Wright" },
					new Actor { Name = "Brad Pitt" },
					new Actor { Name = "Edward Norton" },
					new Actor { Name = "Leonardo DiCaprio" },
					new Actor { Name = "Joseph Gordon-Levitt" },
					new Actor { Name = "Henry Fonda" },
					new Actor { Name = "Lee J. Cobb" }
				);
				context.SaveChanges();
			}

			// Genres (расширенный список)
			if(!context.Genres.Any())
			{
				context.Genres.AddRange(
					new Genre { Name = "Crime", Description = "..." }, // можно оставить или обновить описания
					new Genre { Name = "Drama", Description = "..." },
					new Genre { Name = "Fantasy", Description = "..." },
					new Genre { Name = "Action", Description = "High-energy films with physical feats and stunts." },
					new Genre { Name = "Adventure", Description = "Journeys, exploration, and quests." },
					new Genre { Name = "Sci-Fi", Description = "Science fiction involving futuristic concepts." },
					new Genre { Name = "Thriller", Description = "Suspenseful films that thrill and excite." },
					new Genre { Name = "History", Description = "Based on historical events or periods." },
					new Genre { Name = "Biography", Description = "Life stories of real people." },
					new Genre { Name = "Romance", Description = "Focus on romantic relationships." }
				);
				context.SaveChanges();
			}

			// Теперь фильмы (10 штук, как раньше + The Green Mile из примера)
			if(!context.Movies.Any())
			{
				var movies = new List<Movie>
				{
					// 1. The Green Mile (из твоего примера)
					new Movie {
						Title = "The Green Mile",
						ReleaseDate = new DateTime(1999, 12, 10),
						ClosingDate = null,
						Runtime = 189,
						Plot = "A death row guard learns that a gentle giant in his charge possesses a mysterious gift.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BMTUxMzQyNjA5MF5BMl5BanBnXkFtZTYwOTU2NTY3._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/Ki4haFrqSrw?si=l-Hraetfytl5H8P5",
						Rating = 8.6m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Stephen King") ?? new Author { Name = "Stephen King" },
							context.Authors.FirstOrDefault(e => e.Name == "Frank Darabont") ?? new Author { Name = "Frank Darabont" }
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Tom Hanks"),
							context.Actors.FirstOrDefault(e => e.Name == "Michael Clarke Duncan"),
							context.Actors.FirstOrDefault(e => e.Name == "David Morse")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Crime"),
							context.Genres.FirstOrDefault(e => e.Name == "Drama")
						}
					},

					// 2. The Shawshank Redemption
					new Movie {
						Title = "The Shawshank Redemption",
						ReleaseDate = new DateTime(1994, 10, 14),
						ClosingDate = null,
						Runtime = 142,
						Plot = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/PLl99DlL6b4?si=hcekB4nSWLoo_RXU",
						Rating = 9.3m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Stephen King"),
							context.Authors.FirstOrDefault(e => e.Name == "Frank Darabont")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Tim Robbins"),
							context.Actors.FirstOrDefault(e => e.Name == "Morgan Freeman")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Drama"),
							context.Genres.FirstOrDefault(e => e.Name == "Crime")
						}
					},

					// 3. The Godfather
					new Movie {
						Title = "The Godfather",
						ReleaseDate = new DateTime(1972, 3, 24),
						ClosingDate = null,
						Runtime = 175,
						Plot = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/UaVTIH8mujA?si=cMkJmQcaFoe73Pya",
						Rating = 9.2m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Mario Puzo"),
							context.Authors.FirstOrDefault(e => e.Name == "Francis Ford Coppola")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Marlon Brando"),
							context.Actors.FirstOrDefault(e => e.Name == "Al Pacino")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Crime"),
							context.Genres.FirstOrDefault(e => e.Name == "Drama")
						}
					},

					// 4. The Dark Knight
					new Movie {
						Title = "The Dark Knight",
						ReleaseDate = new DateTime(2008, 7, 18),
						ClosingDate = null,
						Runtime = 152,
						Plot = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/EXeTwQWrcwY?si=48e43XAgcYb1firA",
						Rating = 9.0m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Christopher Nolan"),
							context.Authors.FirstOrDefault(e => e.Name == "Jonathan Nolan"),
							context.Authors.FirstOrDefault(e => e.Name == "David S. Goyer")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Christian Bale"),
							context.Actors.FirstOrDefault(e => e.Name == "Heath Ledger"),
							context.Actors.FirstOrDefault(e => e.Name == "Michael Caine")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Action"),
							context.Genres.FirstOrDefault(e => e.Name == "Crime"),
							context.Genres.FirstOrDefault(e => e.Name == "Drama")
						}
					},

					// 5. Schindler's List
					new Movie {
						Title = "Schindler's List",
						ReleaseDate = new DateTime(1993, 12, 15),
						ClosingDate = null,
						Runtime = 195,
						Plot = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
						Country = "United States",
						Poster = "https://upload.wikimedia.org/wikipedia/en/3/38/Schindler%27s_List_movie.jpg",
						Rating = 9.0m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Thomas Keneally"),
							context.Authors.FirstOrDefault(e => e.Name == "Steven Spielberg")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Liam Neeson"),
							context.Actors.FirstOrDefault(e => e.Name == "Ralph Fiennes")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Drama"),
							context.Genres.FirstOrDefault(e => e.Name == "History"),
							context.Genres.FirstOrDefault(e => e.Name == "Biography")
						}
					},

					// 6. Interstellar
					new Movie {
						Title = "Interstellar",
						ReleaseDate = new DateTime(2014, 10, 26),
						ClosingDate = null,
						Runtime = 169,
						Plot = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
						Country = "USA , UK , Canada",
						Poster = "https://statichdrezka.ac/i/2022/12/8/la363f2cf94f5gp16v37x.jpeg",
						TeaserUrl = "https://www.youtube.com/embed/zSWdZVtXT7E?si=kOTPFwWEq7m2aQrT",
						Rating = 8.7m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Christopher Nolan"),
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Matthew McConaughey"),
							context.Actors.FirstOrDefault(e => e.Name == "Anne Hathaway"),
							context.Actors.FirstOrDefault(e => e.Name == "Michael Caine"),
							context.Actors.FirstOrDefault(e => e.Name == "Matt Damon"),
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Fantasy"),
							context.Genres.FirstOrDefault(e => e.Name == "Adventure"),
							context.Genres.FirstOrDefault(e => e.Name == "Drama")
						}
					},

					// 7. Pulp Fiction
					new Movie {
						Title = "Pulp Fiction",
						ReleaseDate = new DateTime(1994, 10, 14),
						ClosingDate = null,
						Runtime = 154,
						Plot = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_.jpg",
						TeaserUrl = "https://www.youtube.com/embed/s7EdQ4FqbhY?si=hNyVB3X95aN9uXny",
						Rating = 8.9m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Quentin Tarantino")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "John Travolta"),
							context.Actors.FirstOrDefault(e => e.Name == "Samuel L. Jackson")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Crime"),
							context.Genres.FirstOrDefault(e => e.Name == "Drama")
						}
					},

					// 8. Forrest Gump
					new Movie {
						Title = "Forrest Gump",
						ReleaseDate = new DateTime(1994, 7, 6),
						ClosingDate = null,
						Runtime = 142,
						Plot = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BNWIwODRlZTUtY2U3ZS00Yzg1LWJhNzYtMmZiYmEyNmU1NjMzXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg",
						Rating = 8.8m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Winston Groom"),
							context.Authors.FirstOrDefault(e => e.Name == "Robert Zemeckis")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Tom Hanks"),
							context.Actors.FirstOrDefault(e => e.Name == "Robin Wright")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Drama"),
							context.Genres.FirstOrDefault(e => e.Name == "Romance")
						}
					},

					// 9. Fight Club
					new Movie {
						Title = "Fight Club",
						ReleaseDate = new DateTime(1999, 10, 15),
						ClosingDate = null,
						Runtime = 139,
						Plot = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/qtRKdVHc-cE?si=YS2BDBM1vVVEG8Ho",
						Rating = 8.8m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Chuck Palahniuk"),
							context.Authors.FirstOrDefault(e => e.Name == "David Fincher")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Brad Pitt"),
							context.Actors.FirstOrDefault(e => e.Name == "Edward Norton")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Drama"),
							context.Genres.FirstOrDefault(e => e.Name == "Thriller")
						}
					},

					// 10. Inception
					new Movie {
						Title = "Inception",
						ReleaseDate = new DateTime(2010, 7, 16),
						ClosingDate = null,
						Runtime = 148,
						Plot = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
						Country = "United States",
						Poster = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SX300.jpg",
						TeaserUrl = "https://www.youtube.com/embed/66TuSJo4dZM?si=ThzRoBYWssi-wjH5&amp",
						
						Rating = 8.8m,
						Authors = new List<Author>()
						{
							context.Authors.FirstOrDefault(e => e.Name == "Christopher Nolan")
						},
						Actors = new List<Actor>()
						{
							context.Actors.FirstOrDefault(e => e.Name == "Leonardo DiCaprio"),
							context.Actors.FirstOrDefault(e => e.Name == "Joseph Gordon-Levitt")
						},
						Genre = new List<Genre>()
						{
							context.Genres.FirstOrDefault(e => e.Name == "Action"),
							context.Genres.FirstOrDefault(e => e.Name == "Sci-Fi"),
							context.Genres.FirstOrDefault(e => e.Name == "Adventure")
						}
					}
				};

				context.Movies.AddRange(movies);
				context.SaveChanges();
			}


		}
	}
}
