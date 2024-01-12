#nullable disable

using DataAccess.Entities.Bases;

namespace DataAccess.Entities
{
    public class MovieGenre : Record
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
