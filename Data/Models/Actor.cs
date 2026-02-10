
namespace MVCFilms.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Связи с другими классами
        public virtual ICollection<Movie> Movies { get; set; }

    }
}