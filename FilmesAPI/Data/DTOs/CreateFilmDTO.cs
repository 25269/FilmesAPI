using FilmesAPI.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class CreateFilmDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O gênero deve ser preenchido.")]
        [Range(1, 6, ErrorMessage = "O  gênero deve respeitar a lista previamente disponibilizada.")]
        public GenreFilm Genre { get; set; }
    }
}
