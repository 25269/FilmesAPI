using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class CreateSessionDTO
    {
        public int IdFilm { get; set; }
        public int IdCinema { get; set; }
    }
}
