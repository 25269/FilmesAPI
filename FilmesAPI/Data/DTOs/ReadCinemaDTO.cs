using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs
{
    public class ReadCinemaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ReadAddressDTO Address { get; set; }
        public ICollection<ReadSessionDTO> Sessions { get; set; }
    }
}
