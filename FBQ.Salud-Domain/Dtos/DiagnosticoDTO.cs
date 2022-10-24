using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Dtos
{
    public class DiagnosticoDTO
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Emfermedad { get; set; }
    }
}
