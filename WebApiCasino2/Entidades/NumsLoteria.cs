using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiCasino2.Entidades
{
    public class NumsLoteria
    {
        public int Id { get; set; }
        [Range(1, 54)]
        public int Numero { get; set; }
        public bool Vendido { get; set; }
        public int RifaId { get; set; }
        public Rifa Rifa { get; set; }

        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
