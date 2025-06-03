using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FilmesAPI.Helpers.Enums;

namespace FilmesAPI.Models
{
    public class Film
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero deve ser preenchido.")]
        [Range(1, 6, ErrorMessage = "O gênero deve respeitar a lista previamente disponibilizada.")]
        public GenreFilm Genero { get; set; }
        public int IdFilm { get; set; }
    }
}
