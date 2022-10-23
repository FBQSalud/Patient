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
        List<Paciente> GetAll(bool edad, string? nombre);
        List<Paciente> GetListPacientesByNombre(string? nombre);
        Paciente GetPacienteById(int id);
        Paciente GetPacienteByDNI(string dni);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
        void Add(Paciente paciente);
    }
}
