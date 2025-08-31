using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FilmsAPI.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Models
{
    public class Film
    {
        [Key]
        public int IdFilm { get; set; }
        public string Title { get; set; }
        public GenreFilm Genre { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
