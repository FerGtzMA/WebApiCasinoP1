using System.ComponentModel.DataAnnotations;
using WebApiCasino2.Validaciones;

namespace WebApiCasino2.DTOs
{
    public class RifaCreacionDTO
    {
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }

        public List<int> PersonasIds { get; set; }
    }
}
