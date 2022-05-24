using WebApiCasino2.Entidades;

namespace WebApiCasino4.DTOs
{
    public class RifaDTOConPersona
    {
        public int Id { get; set; }
        public int NumLoteria { get; set; }
        public bool Gana { get; set; }
        public int PersonaId { get; set; }
        public int RifaId { get; set; }
        public int PremioId { get; set; }
        public Persona Personas { get; set; }
    }
}
