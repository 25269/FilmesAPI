using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class UpdateCinemaDTO
    {
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Name { get; set; }
    }
}
