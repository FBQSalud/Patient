
using FBQ.Salud_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FBQ.Salud_AccessData.Commands
{
    public class FbqSaludDbContext : DbContext
    {
        public FbqSaludDbContext() { }
        public FbqSaludDbContext(DbContextOptions<FbqSaludDbContext> options) : base(options) { }

        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<HistoriaClinica> HistoriasClinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoriaClinica>(entity =>
            {
                entity.HasOne(x => x.Paciente).WithMany(a => a.HistoriaClinicas).HasForeignKey(x => x.PacienteId);

            });
            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasOne(x => x.Paciente).WithMany(a => a.Turnos).HasForeignKey(x => x.PacienteId);

            });
        }
    }
}
