using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiCasino2.Entidades;

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
        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Premio> Premios { get; set; }

        public DbSet<PersonaRifa> PersonasRifas { get; set; }
    }
}
