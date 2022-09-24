using FBQ.Salud_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Domain.Commands
{
    public interface IPacienteRepository
    {
        List<Paciente> GetAll();
        Paciente GetPacienteById(int id);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
        void Add(Paciente paciente);
    }
}
