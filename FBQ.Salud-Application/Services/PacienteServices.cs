using AutoMapper;
using FBQ.Salud_Application.Validation;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Application.Services
{
    public interface IPacienteService
    {
        List<PacienteRequest> GetAll(bool edad, string? nombre);
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

        public List<PacienteRequest> GetAll(bool edad, string? nombre)
        {
            //var pacientes = _pacienteRepository.GetAll(edad,nombre);

            //var pacientesMapeados = _mapper.Map<List<PacienteDto>>(pacientes);

            //return pacientesMapeados;
            if (nombre == null)
            {
                var pacientes = _pacienteRepository.GetAll(edad, nombre);

                List<PacienteRequest> pacientesMapeados = new List<PacienteRequest>();

                foreach (var p in pacientes)
                {
                    var pacientemaper = new PacienteRequest
                    {
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Edad = p.Edad,
                        Sexo = p.Sexo,
                        DNI = p.DNI,
                        Direccion = p.Direccion,
                        DirecionNumero = p.DirecionNumero,
                        Telefono = p.Telefono,
                        Foto = p.Foto,
                        sort = edad
                    };
                    pacientesMapeados.Add(pacientemaper);
                }
                return pacientesMapeados;
            }
            else
            {
                var productos = _pacienteRepository.GetListPacientesByNombre(nombre);

                List<PacienteRequest> pacientesMapeados = new List<PacienteRequest>();

                foreach (var p in productos)
                {
                    var pacientemaper = new PacienteRequest
                    {
                        Nombre = p.Nombre,
                        Apellido = p.Apellido,
                        Edad = p.Edad,
                        Sexo = p.Sexo,
                        DNI = p.DNI,
                        Direccion = p.Direccion,
                        DirecionNumero = p.DirecionNumero,
                        Telefono = p.Telefono,
                        Foto = p.Foto,
                        sort = edad
                    };
                    pacientesMapeados.Add(pacientemaper);
                }
                return pacientesMapeados;
            }
        }
        public Response GetPacienteByDni(string dni)
        {
            var paciente = _pacienteRepository.GetPacienteByDNI(dni);

            if (paciente == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Paciente con dni " + dni + " inexistente",
                    Result = ""
                };
            }
            var pacienteMappeado = _mapper.Map<PacienteDto>(paciente);

            return new Response
            {
                Success = true,
                Message = "Exito",
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

        public Response Update(int id,PacienteDto paciente)
        {
            //_pacienteRepository.Update(paciente);
            var pacienteUpdate = _pacienteRepository.GetPacienteById(id);

            var pacienteMapped = _mapper.Map<Paciente>(paciente);

            if (pacienteUpdate != null && _pacienteValidation.ExistePaciente(pacienteMapped))
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
            else
            {
                if (_pacienteValidation.ExistePaciente(pacienteMapped)==false)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Paciente con dni existente",
                        Result = ""
                    };
                }
                return new Response
                {
                    Success = false,
                    Message = "Paciente con id " + id + " inexistente",
                    Result = ""
                };
            }
        }
    }
}
