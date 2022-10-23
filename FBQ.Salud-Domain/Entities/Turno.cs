
using System.ComponentModel.DataAnnotations;

namespace FBQ.Salud_Domain.Entities
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaTurno { get; set; }
        public bool EstadoTurno { get; set; } = false;
        public string DiagnosticoId { get; set; }

        public Paciente Paciente { get; set; }
        public Diagnostico Diagnostico { get; set; }
    }
}
