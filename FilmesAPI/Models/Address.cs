using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string PublicSpace { get; set; }
        public int Number {  get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
