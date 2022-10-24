using AutoMapper;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Domain.Dtos;

namespace FBQ.Salud_Application.Services
{
    public interface IDiagnosticoServices
    {
        Response GetByCodigo(string codigo);
        List<DiagnosticoDTO> GetAll();
    }
    public class DiagnosticoServices : IDiagnosticoServices
    {
        private readonly IDiagnosticoRepository _diagnosticoRepository;
        private readonly IMapper _mapper;
        public DiagnosticoServices(IDiagnosticoRepository diagnosticoRepository,IMapper mapper)
        {
            _diagnosticoRepository = diagnosticoRepository;
            _mapper = mapper;
        }

        public List<DiagnosticoDTO> GetAll()
        {
            var diagnostico = _diagnosticoRepository.GetAll();

            var diagnosticosMapeados = _mapper.Map<List<DiagnosticoDTO>>(diagnostico);

            return diagnosticosMapeados;
        }

        public Response GetByCodigo(string codigo)
        {
            var diagnostico = _diagnosticoRepository.GetDiagnosticoByCodigo(codigo);

            if (diagnostico == null)
            {
                return new Response
                {
                    Success = false,
                    Message = "Diagnostico con codigo " + codigo + " inexistente",
                    Result = ""
                };
            }
            var diagnosticoMappeado = _mapper.Map<DiagnosticoDTO>(diagnostico);

            return new Response
            {
                Success = true,
                Message = "Exito",
                Result = diagnosticoMappeado
            };
        }
    }
}
