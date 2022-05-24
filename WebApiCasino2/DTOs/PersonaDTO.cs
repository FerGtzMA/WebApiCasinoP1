using System.ComponentModel.DataAnnotations;
using WebApiCasino2.Entidades;
using WebApiCasino2.Validaciones;

namespace WebApiCasino2.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} solo puede tener hasta 100 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public List<PersonaRifa> PersonasRifas { get; set; }
    }
}
