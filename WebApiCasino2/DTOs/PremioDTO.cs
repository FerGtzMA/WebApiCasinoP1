using WebApiCasino2.Entidades;

namespace WebApiCasino2.DTOs
{
    public class PremioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public bool Entregado { get; set; }
        public int RifaId { get; set; }
        public Rifa Rifa { get; set; }
    }
}
