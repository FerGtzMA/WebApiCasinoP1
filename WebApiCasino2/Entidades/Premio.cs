namespace WebApiCasino2.Entidades
{
    public class Premio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public bool Entregado { get; set; }
        public int RifaId { get; set; }
        public Rifa Rifa { get; set; }
    }
}
