using AutoMapper;
using FBQ.Salud_Application.Validation;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Application.Services
{
    public interface IPacienteService
    {
        List<PacienteRequest> GetAll();
        Response GetPacienteById(int id);
        Response GetPacienteByDni(string dni);
        Response Update(int id, PacienteDto paciente);
        Response Delete(int pacienteId);
        Response Add(PacienteDto paciente);
    }
    public class PacienteServices : IPacienteService
    {
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPacienteValidationExist _pacienteValidation;
        private IPacienteRepository @object;

        public PacienteServices(IMapper mapper, IPacienteRepository pacienteRepository, IPacienteValidationExist pacienteValidation)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pacienteRepository = pacienteRepository ?? throw new ArgumentNullException(nameof(pacienteRepository));
            _pacienteValidation = pacienteValidation ?? throw new ArgumentNullException(nameof(pacienteValidation));
        }

        public Response Add(PacienteDto paciente)
        {
            if (paciente == null)
                throw new ArgumentNullException(nameof(paciente));

            if (string.IsNullOrEmpty(paciente.DNI))
                throw new ArgumentException("El DNI del paciente no puede ser nulo o vacío.");

            var userMapped = _mapper.Map<Paciente>(paciente);

            if (_pacienteValidation.ExistePaciente(userMapped))
            {
                _pacienteRepository.Add(userMapped);

                return new Response
                {
                    Success = true,
                    Message = "Éxito",
                    Result = paciente
                };
            }
            else
            {
                var userExistente = _pacienteRepository.GetPacienteByDNI(userMapped.DNI);
                if (userExistente != null && userExistente.Estado == false)
                {
                    userExistente.Estado = true;
                    _pacienteRepository.Update(userExistente);

                    var userMap = _mapper.Map<PacienteDto>(userExistente);
                    return new Response
                    {
                        Success = true,
                        Message = $"Paciente con DNI {paciente.DNI} activado",
                        Result = userMap
                    };
                }
                else
                {
                    return new Response
                    {
                        Success = false,
                        Message = $"Existe un paciente con DNI {paciente.DNI}",
                        Result = ""
                    };
                }
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

        public List<PacienteRequest> GetAll()
        {
        
                var pacientes = _pacienteRepository.GetListPacientesByNombre();

                List<PacienteRequest> pacientesMapeados = new List<PacienteRequest>();

                foreach (var p in pacientes)
                {
                    var pacientemaper = new PacienteRequest
                    {
                        PacienteId = p.PacienteId,
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Edad = p.Edad,
                        Sexo = p.Sexo,
                        DNI = p.DNI,
                        Direccion = p.Direccion,
                        DirecionNumero = p.DirecionNumero,
                        Telefono = p.Telefono,
                        Foto = p.Foto,
                        Estado = p.Estado,
                    };
                    pacientesMapeados.Add(pacientemaper);
                }
                return pacientesMapeados;
            
        }
        public Response GetPacienteByDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                return new Response
                {
                    Success = false,
                    Message = "El DNI no puede estar vacío.",
                    Result = null
                };
            }

            var paciente = _pacienteRepository.GetPacienteByDNI(dni);

            if (paciente == null)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Paciente con DNI {dni} inexistente",
                    Result = ""
                };
            }
            var pacienteMappeado = _mapper.Map<PacienteDto>(paciente);

            return new Response
            {
                Success = true,
                Message = "Éxito",
                Result = pacienteMappeado
            };
        }

        public Response GetPacienteById(int id)
        {
            var paciente = _pacienteRepository.GetPacienteById(id);

            if (paciente == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Paciente con id " + id + " inexistente",
                    Result = ""
                }; 
            }
            var pacienteMappeado= _mapper.Map<PacienteDto>(paciente);

            return new Response
            {
                Success = true,
                Message = "Exito",
                Result = pacienteMappeado
            };
        }

        public Response Update(int id, PacienteDto paciente)
        {
            var pacienteUpdate = _pacienteRepository.GetPacienteById(id);

            if (pacienteUpdate == null)
            {
                return new Response
                {
                    Success = false,
                    Message = $"Paciente con id {id} inexistente",
                    Result = ""
                };
            }

            var pacienteMapped = _mapper.Map<Paciente>(paciente);

            if (_pacienteValidation.ExistePaciente(pacienteMapped))
            {
                _mapper.Map(paciente, pacienteUpdate);
                _pacienteRepository.Update(pacienteUpdate);

                return new Response
                {
                    Success = true,
                    Message = "Paciente modificado",
                    Result = paciente
                };
            }

            return new Response
            {
                Success = false,
                Message = "Paciente con DNI existente",
                Result = ""
            };
        }
    }
}
