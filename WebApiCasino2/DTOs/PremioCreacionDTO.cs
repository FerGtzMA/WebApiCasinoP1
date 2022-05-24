using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.DTOs
{
    public class PremioCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public bool Entregado { get; set; }
        [Required]
        public int RifaId { get; set; }
    }
}
