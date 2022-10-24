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
        public int PacienteId { get; set; }
        public DateTime FechaApertura { get; set; }
        public bool Estado { get; set; }
        public string Diagnostico { get; set; }
        public string Recomendacion { get; set; }
        public string Medicacion { get; set; }
        public Paciente Paciente { get; set; }
    }
}
