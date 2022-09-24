﻿
using System.ComponentModel.DataAnnotations;

namespace FBQ.Salud_Domain.Dtos
{
    public class TurnoDTO
    {
        public int TurnoId { get; set; }
        [Required]
        public int MedicoId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public DateTime FechaTurno { get; set; }
        [Required]
        public string Observacion { get; set; }
        [Required]
        public bool EstadoTurno { get; set; }
    }
}