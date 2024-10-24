using GestorActividades.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestorActividades.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.Organizador)
                .WithMany(o => o.Actividades)
                .HasForeignKey(a => a.OrganizadorID);

            modelBuilder.Entity<Participante>()
                .HasMany(p => p.Actividades)
                .WithMany(a => a.Participantes);

            base.OnModelCreating(modelBuilder);
        }

    }
}
