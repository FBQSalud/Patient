using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Dtos
{
    public class TurnoDTOForUpdate
    {
        [Required]
        public int MedicoId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [Required]
        public string DiagnosticoId { get; set; }
    }
}
