﻿
using FBQ.Salud_Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FBQ.Salud_Domain.Dtos
{
    public class TurnoDTO
    {
        [Required]
        public int MedicoId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public DateTime FechaTurno { get; set; }

        public string? Observacion { get; set; }

        public Diagnostico? Diagnostico { get; set; }
    }
}
