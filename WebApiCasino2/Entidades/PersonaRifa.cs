namespace WebApiCasino2.Entidades
{
    public class PersonaRifa
    {
        public int PersonaId { get; set; }
        public int RifaId { get; set; }
        public int Oredn { get; set; }
        public Persona Persona { get; set; }
        public Rifa Rifa { get; set; }
    }
}
