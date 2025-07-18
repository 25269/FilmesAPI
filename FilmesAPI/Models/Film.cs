using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FilmesAPI.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Models
{
    public class Film
    {
        [Key]
        public int IdFilm { get; set; }
        public string Title { get; set; }
        public GenreFilm Genre { get; set; }
    }
}
