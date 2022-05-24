using WebApiCasino2.Entidades;

namespace WebApiCasino2.DTOs
{
    public class GetRifaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public bool existencias { get; set; }
        public List<Premio> Premios { get; set; }
        public List<PersonaRifa> PersonasRifas { get; set; }
    }
}
