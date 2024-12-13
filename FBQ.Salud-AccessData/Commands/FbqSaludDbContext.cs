
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
            modelBuilder.Entity<Paciente>().HasData(
                new Paciente
                {
                    PacienteId = 1,
                    Nombre = "leonardo",
                    Apellido = "esteves",
                    Edad = 25,
                    Sexo = "Masculino",
                    DNI = "41395213",
                    Direccion = "calle tremenda",
                    DirecionNumero = "5333",
                    Telefono = "112561245",
                    Estado = false,
                    Foto = "foto.jpg"
                },
                new Paciente
                {
                    PacienteId = 2,
                    Nombre = "brisa",
                    Apellido = "muñoz",
                    Edad = 25,
                    Sexo = "Femenino",
                    DNI = "41395211",
                    Direccion = "calle abismal",
                    DirecionNumero = "5323",
                    Telefono = "112561241",
                    Estado = false,
                    Foto = "foto.jpg"
                });
            modelBuilder.Entity<Turno>().HasData(
                new Turno { 
                    TurnoId = 1,
                    MedicoId = 1,
                    PacienteId = 1,
                    FechaTurno = DateTime.Now,
                    EstadoTurno = false,
                    Observacion = "gripe",
                    DiagnosticoId = "AR.1"
                },
                new Turno {
                    TurnoId = 2,
                    MedicoId = 2,
                    PacienteId = 2,
                    FechaTurno = DateTime.Now,
                    EstadoTurno = false,
                    Observacion = "gripe",
                    DiagnosticoId = "AR.1"
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
