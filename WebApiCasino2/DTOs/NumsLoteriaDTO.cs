using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.DTOs
{
    public class NumsLoteriaDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public bool Vendido { get; set; }
    }
}
