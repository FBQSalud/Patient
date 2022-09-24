using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Entities
{
    public class HistoriaClinica
    {
        public int HistoriaClinicaId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public DateTime FechaApertura { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public string Diagnostico { get; set; }
    }
}
