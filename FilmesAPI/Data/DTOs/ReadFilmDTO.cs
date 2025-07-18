using FilmesAPI.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class ReadFilmDTO
    {
        public string Title { get; set; }
        public GenreFilm Genre { get; set; }
        public DateTime ConsultHour { get; set; } = DateTime.Now;
    }
}
