using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Dtos
{
    public class PacienteRequest
    {
        public int PacienteId { get; set; }

        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string DirecionNumero { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Foto { get; set; }

        public bool Estado { get; set; } = false;
        public bool sort { get; set; }
    }
}
