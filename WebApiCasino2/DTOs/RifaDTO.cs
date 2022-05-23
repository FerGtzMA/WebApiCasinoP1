namespace WebApiCasino2.DTOs
{
    public class RifaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }

        public List<NumsLoteriaDTO> Numeros { get; set; }
    }
}
