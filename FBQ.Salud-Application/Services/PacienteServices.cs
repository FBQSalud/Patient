using AutoMapper;
using FBQ.Salud_Application.Validation;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Application.Services
{
    public interface IPacienteService
    {
        List<PacienteDto> GetAll();
        Paciente GetPacienteById(int id);
        void Update(Paciente paciente);
        Response Delete(int pacienteId);
        Response Add(PacienteDto paciente);
    }
    public class PacienteServices : IPacienteService
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPacienteValidationExist _pacienteValidation;
        public PacienteServices(IMapper mapper, IPacienteRepository pacienteRepository, IPacienteValidationExist pacienteValidation)
        {
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
            _pacienteValidation = pacienteValidation;
        }

        public Response Add(PacienteDto paciente)
        {
            var userMapped = _mapper.Map<Paciente>(paciente);

            if (_pacienteValidation.ExistePaciente(userMapped))
            {
                _pacienteRepository.Add(userMapped);

                return new Response
                {
                    Success = true,
                    Message = "Exito",
                    Result = paciente
                };
            }
            else
            {
                var userExistente = _pacienteRepository.GetPacienteByDNI(userMapped.DNI);
                if (userExistente.Estado == true)
                {
                    userExistente.Estado = false;
                    _pacienteRepository.Update(userExistente);

                    var userMap = _mapper.Map<PacienteDto>(userExistente);
                    return new Response
                    {
                        Success = true,
                        Message = "Paciente con dni " + paciente.DNI + " activado",
                        Result = userMap
                    };
                }
                else
                    return new Response
                    {
                        Success = false,
                        Message = "Existe un paciente con dni " + paciente.DNI,
                        Result = ""
                    };

            }

        }
        public Response Delete(int pacienteId)
        {
            var pacient = _pacienteRepository.GetPacienteById(pacienteId);

            if (pacient != null)
            {
                if (pacient.Estado == true)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Paciente inexistente",
                        Result = ""
                    };
                }
                else
                {
                    pacient.Estado = true;

                    _pacienteRepository.Update(pacient);

                    var userMappeo = _mapper.Map<PacienteDto>(pacient);

                    return new Response
                    {
                        Success = true,
                        Message = "Paciente eliminado",
                        Result = userMappeo
                    };
                }
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Empleado con id " + pacienteId + " inexistente",
                    Result = " "
                };
            }
        }

        public List<PacienteDto> GetAll()
        {
            var pacientes = _pacienteRepository.GetAll();

            var pacientesMapeados = _mapper.Map<List<PacienteDto>>(pacientes);

            return pacientesMapeados;
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
