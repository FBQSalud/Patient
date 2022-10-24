using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Dtos
{
    public class HistoriaClinicaRequest
    {
        [Required]
        public int PacienteId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaApertura { get; set; }
        [Required]
        public bool Estado { get; set; }
        [Required]
        public string Diagnostico { get; set; }
        [Required]
        public string Recomendacion { get; set; }
        [Required]
        public string Medicacion { get; set; }
        public bool sort { get; set; }
    }
}
