using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Entities
{
    public class Diagnostico
    {
        public string Codigo { get; set; }
        public string Emfermedad { get; set; }
        public ICollection<Turno> Turnos { get; set; }
    }
}
