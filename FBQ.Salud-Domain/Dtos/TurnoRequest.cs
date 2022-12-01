
using System.ComponentModel.DataAnnotations;

namespace FBQ.Salud_Domain.Dtos
{
    public class TurnoRequest
    {
        public int TurnoId { get; set; }

        public int MedicoId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public DateTime FechaTurno { get; set; }

        public bool EstadoTurno { get; set; } 

        public string? Observacion { get; set; }

        [Required]
        public string DiagnosticoId { get; set; }
    }
}
