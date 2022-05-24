using System.ComponentModel.DataAnnotations;
using WebApiCasino2.Validaciones;

namespace WebApiCasino2.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public bool existencias { get; set; }
        //Una relación de uno a muchos
        public List<Premio> Premios { get; set; }
        //Una relación de muchos a muchos
        public List<PersonaRifa> PersonasRifas { get; set; }
    }
}
