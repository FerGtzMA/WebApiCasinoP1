using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.DTOs
{
    public class NumsLoteriaCreacionDTO
    {
        [Range(1, 54)]
        public int Numero { get; set; }
        public bool Vendido { get; set; }
    }
}
