using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsAPI.Models
{
    public class Session
    {     
        [ForeignKey("Film")]
        public int? IdFilm { get; set; }
        public virtual Film Film { get; set; }
        [ForeignKey("Cinema")]
        public int? IdCinema { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
