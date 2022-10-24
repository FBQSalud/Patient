using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;

namespace FBQ.Salud_Application.Services
{
    public interface ITurnoServices
    {
        List<TurnoDTO> GetAll();
        Response GetTurnoById(int id);
        Response Update(int id, TurnoDTOForUpdate Turno);
        Response Delete(int turnoId);
        Response CreateTurno(TurnoDTOForCreated turno);
    }
    public class TurnoServices : ITurnoServices
    {
        private ITurnosRepository _turnosRepository;
        private IPacienteRepository _pacienteRepository;
        private IDiagnosticoRepository _diagnosticoRepository;
        private readonly IMapper _mapper;

        public TurnoServices(ITurnosRepository turnosRepository,
            IMapper mapper,
            IPacienteRepository pacienteRepository,IDiagnosticoRepository diagnosticoRepository)
        {
            _turnosRepository = turnosRepository;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
            _diagnosticoRepository = diagnosticoRepository;
        }

        public List<TurnoDTO> GetAll()
        {
            var turnos= _turnosRepository.GetAll();

            var turnosMapeados=_mapper.Map<List<TurnoDTO>>(turnos);

            return turnosMapeados;
        }
        public Response GetTurnoById(int id)
        {
            var turno = _turnosRepository.GetTurnoById(id);

            if (turno == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Atencion con id " + id + " inexistente",
                    Result = ""
                };
            }
            var turnoMappeado = _mapper.Map<TurnoDTO>(turno);

            return new Response
            {
                Success = true,
                Message = "Exito",
                Result = turnoMappeado
            };
        }

        public Response CreateTurno(TurnoDTOForCreated turno)
        {
            var paciente = _pacienteRepository.GetPacienteById(turno.PacienteId);

            

            var fechaValida = turno.FechaTurno >= DateTime.Today.Date;

            if (!fechaValida)
            {
                return new Response
                {
                    Success = false,
                    Message = "La fecha debe ser a partir de " + DateTime.Today.Date,
                    Result = ""
                };
            }
            if (paciente == null || paciente.Estado==true)
            {
                return new Response
                {
                    Success = false,
                    Message = "No existe un paciente con id " + turno.PacienteId,
                    Result = ""
                };
            }
            else
            {
                var turnoMapped = _mapper.Map<Turno>(turno);
                _turnosRepository.Add(turnoMapped);

                return new Response
                {
                    Success = true,
                    Message = "Exito",
                    Result = turno
                };
            }
        }

        public Response Update(int id, TurnoDTOForUpdate turno)
        {
            var turn = _pacienteRepository.GetPacienteById(turno.PacienteId);

            var diagnostico = _diagnosticoRepository.GetDiagnosticoByCodigo(turno.DiagnosticoId);

            if (turn == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No existe un paciente con id " + turno.PacienteId,
                    Result = ""
                };
            }
            if (diagnostico == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No existe diagnostico con codigo " + turno.DiagnosticoId,
                    Result = ""
                };
            }
            var turnoUpdate = _turnosRepository.GetTurnoById(id);

            if (turnoUpdate != null)
            {
                if (turnoUpdate.EstadoTurno == true)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Turno atendido",
                        Result = ""
                    };
                }
                turnoUpdate.EstadoTurno = true;

                _mapper.Map(turno, turnoUpdate);

                _turnosRepository.Update(turnoUpdate);

                return new Response
                {
                    Success = true,
                    Message = "Turno modificado",
                    Result = turno
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Turno con id " + id + " inexistente",
                    Result = ""
                };
            }
        }

        public Response Delete(int turnoId)
        {
            var turno = _turnosRepository.GetTurnoById(turnoId);

            if (turno != null)
            {
                if (turno.EstadoTurno == true)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Turno inexistente",
                        Result = ""
                    };
                }
                else
                {
                    turno.EstadoTurno = true;

                    _turnosRepository.Update(turno);

                    var TurnoMappeo = _mapper.Map<TurnoDTO>(turno);

                    return new Response
                    {
                        Success = true,
                        Message = "Turno eliminado",
                        Result = TurnoMappeo
                    };
                }
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Turno con id " + turnoId + " inexistente",
                    Result = " "
                };
            }
        }
    }
}
