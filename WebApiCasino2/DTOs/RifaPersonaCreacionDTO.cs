using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.DTOs
{
    public class RifaPersonaCreacionDTO
    {
        [Required]
        [Range(1, 54)]
        public int NumLoteria { get; set; }

        public int PersonaId { get; set; }
        public int RifaId { get; set; }
        public int? PremioId { get; set; }

        public bool? Gana { get; set; }
    }
}
