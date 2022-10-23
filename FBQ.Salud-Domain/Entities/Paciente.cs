using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Entities
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string DirecionNumero { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; } = false;
        public string Foto { get; set; }
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<HistoriaClinica> HistoriaClinicas { get; set; }

    }
}
