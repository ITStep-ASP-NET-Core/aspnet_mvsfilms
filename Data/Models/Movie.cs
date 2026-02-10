
namespace MVCFilms.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public DateTime? ClosingDate { get; set; }

        public int Runtime { get; set; }

        public string Plot { get; set; }

        public string Country { get; set; }

        public string Poster { get; set; }

        public string? TeaserUrl { get; set; }

        public decimal Rating { get; set; }

        //Связи с другими классами
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Genre> Genre { get; set; }

    }
}
