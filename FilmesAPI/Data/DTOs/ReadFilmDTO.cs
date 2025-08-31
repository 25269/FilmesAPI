using FilmsAPI.Helpers.Enums;
using FilmsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class ReadFilmDTO
    {
        public int IdFilm { get; set; }
        public string Title { get; set; }
        public GenreFilm Genre { get; set; }
        public DateTime ConsultHour { get; set; } = DateTime.Now;
        public ICollection<ReadSessionDTO> Sessions { get; set; }
    }
}
