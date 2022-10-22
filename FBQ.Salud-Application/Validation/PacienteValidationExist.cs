using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQ.Salud_Application.Validation
{ 
    public interface IPacienteValidationExist
    {
        bool ExistePaciente(Paciente paciente);
    }
    public class PacienteValidationExist : IPacienteValidationExist
    {
        private readonly IPacienteRepository _repository;
        public PacienteValidationExist(IPacienteRepository repository)
        {
            _repository = repository;
        }
        public bool ExistePaciente(Paciente paciente)
        {
            var busquedaCliente = _repository.GetPacienteByDNI(paciente.DNI);

            return busquedaCliente == null;
        }
    }
}
