using Microsoft.EntityFrameworkCore;
using WebApiCasino2.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApiCasino2
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonaRifa>()
                .HasKey(al => new { al.PersonaId, al.RifaId });
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Rifa> Rifa { get; set; }
        public DbSet<NumsLoteria> NumerosL { get; set; }
        public DbSet<PersonaRifa> PersonaRifa { get; set; }
    }
}
