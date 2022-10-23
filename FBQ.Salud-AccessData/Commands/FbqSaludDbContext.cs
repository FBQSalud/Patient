
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
        public virtual DbSet<Diagnostico> Diagnostico { get; set; }
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
                entity.HasOne(x => x.Diagnostico).WithMany(a => a.Turnos).HasForeignKey(x => x.DiagnosticoId);

            });
            modelBuilder.Entity<Diagnostico>(entity =>
            {
                entity.HasKey(c => c.Codigo);

            });
            modelBuilder.Entity<Diagnostico>().HasData(
            new Diagnostico
            {
                Codigo = "AR.1",
                Emfermedad = "ARTRITIS GRADO 1"
            },
            new Diagnostico
            {
                Codigo = "AR.2",
                Emfermedad = "ARTRITIS GRADO 2"
            },
            new Diagnostico
            {
                Codigo = "QE.1",
                Emfermedad = "QUEBRADURA EXPUESTA GRADO 1"
            },
            new Diagnostico
            {
                Codigo = "QE.2",
                Emfermedad = "QUEBRADURA EXPUESTA GRADO 2"
            },
            new Diagnostico
            {
                Codigo = "FI.1",
                Emfermedad = "FIEBRE"
            },
            new Diagnostico
            {
                Codigo = "CO.1",
                Emfermedad = "COVID19"
            },
            new Diagnostico
            {
                Codigo = "GR.1",
                Emfermedad = "GRIPE A"
            });

        }
    }
}
