using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Entities;


namespace FBQ.Salud_Application.Services
{
    public interface IHistoriaClinicaServices
    {
        List<HistoriaClinicaRequest> GetAll(bool fecha, int? pacienteId);
        Response GetHistoriaClinicaById(int id);
        Response Update(int id, HistoriaClinicaDTOForUpdate historiaClinica);
        Response Delete(int historiaClinicaId);
        Response CreateHistoriaClinica(HistoriaClinicaDTO historiaClinica);
    }
    public class HistoriaClinicaServices : IHistoriaClinicaServices
    {
        IHistoriaClinicaRepository _historiaClinicaRepository;
        private readonly IMapper _mapper;
        private readonly IPacienteRepository _pacienteRepository;

        public HistoriaClinicaServices(IHistoriaClinicaRepository historiaClinicaRepository,
            IMapper mapper,
            IPacienteRepository pacienteRepository)
        {
            _historiaClinicaRepository = historiaClinicaRepository;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }
        public List<HistoriaClinicaRequest> GetAll(bool fecha, int? pacienteId)
        {
            if (pacienteId == null)
            {
                var historias = _historiaClinicaRepository.GetAll(fecha, pacienteId);

                List<HistoriaClinicaRequest> historiasMapeados = new List<HistoriaClinicaRequest>();

                foreach (var p in historias)
                {
                    var historyMaper = new HistoriaClinicaRequest
                    {
                        PacienteId = p.PacienteId,
                        FechaApertura = p.FechaApertura,
                        Estado = p.Estado,
                        Diagnostico = p.Diagnostico,
                        Recomendacion = p.Recomendacion,
                        Medicacion = p.Medicacion,
                        sort = fecha
                    };
                    historiasMapeados.Add(historyMaper);
                }
                return historiasMapeados;
            }
            else
            {
                var historias = _historiaClinicaRepository.GetListHistoriaClinicaByPacienteId(pacienteId);

                List<HistoriaClinicaRequest> historiasMapeados = new List<HistoriaClinicaRequest>();

                foreach (var p in historias)
                {
                    var historyMaper = new HistoriaClinicaRequest
                    {
                        PacienteId = p.PacienteId,
                        FechaApertura = p.FechaApertura,
                        Estado = p.Estado,
                        Diagnostico = p.Diagnostico,
                        Recomendacion = p.Recomendacion,
                        Medicacion = p.Medicacion,
                        sort = fecha
                    };
                    historiasMapeados.Add(historyMaper);
                }
                return historiasMapeados;
            }
        }
        public Response GetHistoriaClinicaById(int id)
        {
            var historia = _historiaClinicaRepository.GetHistoriaClinicaById(id);

            if (historia == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Historia clinica con id " + id + " inexistente",
                    Result = ""
                };
            }
            var historiaMappeada = _mapper.Map<HistoriaClinicaDTO>(historia);

            return new Response
            {
                Success = true,
                Message = "Exito",
                Result = historiaMappeada
            };
        }

        public Response CreateHistoriaClinica(HistoriaClinicaDTO historiaClinica)
        {
            var paciente = _pacienteRepository.GetPacienteById(historiaClinica.PacienteId);

            var fechaValida = historiaClinica.FechaApertura >= DateTime.Today.Date;
            if (!fechaValida)
            {
                return new Response
                {
                    Success = false,
                    Message = "La fecha debe ser a partir de " + DateTime.Today.Date,
                    Result = ""
                };
            }
            if (paciente == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No existe un paciente con id " + historiaClinica.PacienteId,
                    Result = ""
                };
            }
            else
            {
                var historiaMapped = _mapper.Map<HistoriaClinica>(historiaClinica);
                _historiaClinicaRepository.Add(historiaMapped);

                return new Response
                {
                    Success = true,
                    Message = "Exito",
                    Result = historiaClinica
                };
            }
        }
        public Response Update(int id, HistoriaClinicaDTOForUpdate historiaClinica)
        {
            var paciente = _pacienteRepository.GetPacienteById(historiaClinica.PacienteId);

            if (paciente == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "No existe un paciente con id " + historiaClinica.PacienteId,
                    Result = ""
                };
            }
            var historyUpdate = _historiaClinicaRepository.GetHistoriaClinicaById(id);

            if (historyUpdate != null)
            {
                _mapper.Map(historiaClinica, historyUpdate);

                _historiaClinicaRepository.Update(historyUpdate);

                return new Response
                {
                    Success = true,
                    Message = "Historia clinica modificada",
                    Result = historiaClinica
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Historia clinica con id " + id + " inexistente",
                    Result = ""
                };
            }
        }
        public Response Delete(int historiaClinicaId)
        {
            var historia = _historiaClinicaRepository.GetHistoriaClinicaById(historiaClinicaId);

            if (historia != null)
            {
                if (historia.Estado == true)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Historia clinica inexistente",
                        Result = ""
                    };
                }
                else
                {
                    historia.Estado = true;

                    _historiaClinicaRepository.Update(historia);

                    var historiaMappeo = _mapper.Map<HistoriaClinicaDTO>(historia);

                    return new Response
                    {
                        Success = true,
                        Message = "Historia clinica eliminada",
                        Result = historiaMappeo
                    };
                }
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Historia con id " + historiaClinicaId + " inexistente",
                    Result = " "
                };
            }
        }
    }
}
