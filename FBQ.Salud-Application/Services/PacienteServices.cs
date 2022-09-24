using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Application.Services
{
    public interface IPacienteService
    {
        List<Paciente> GetAll();
        Paciente GetPacienteById(int id);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
        void Add(Paciente paciente);
        Paciente CreatePaciente(PacienteDto paciente);
    }
    public class PacienteServices : IPacienteService
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteServices(IMapper mapper, IPacienteRepository pacienteRepository)
        {
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }

        public void Add(Paciente paciente)
        {
            var pacienteMapped = _mapper.Map<Paciente>(paciente);
            _pacienteRepository.Add(pacienteMapped);
        }

        public Paciente CreatePaciente(PacienteDto paciente)
        {
            var pacienteMapped = _mapper.Map<Paciente>(paciente);
            _pacienteRepository.Add(pacienteMapped);
            return pacienteMapped;
        }

        public void Delete(Paciente paciente)
        {
            _pacienteRepository.Delete(paciente);
        }

        public List<Paciente> GetAll()
        {
            return _pacienteRepository.GetAll();
        }

        public Paciente GetPacienteById(int id)
        {
            return _pacienteRepository.GetPacienteById(id);
        }

        public void Update(Paciente paciente)
        {
            _pacienteRepository.Update(paciente);
        }
    }
}
