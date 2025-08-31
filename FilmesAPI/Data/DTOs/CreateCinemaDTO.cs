using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class CreateCinemaDTO
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}
