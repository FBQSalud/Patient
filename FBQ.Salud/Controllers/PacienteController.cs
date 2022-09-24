using AutoMapper;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBQ.Salud_Presentation.Controllers
{
    [Route("api/[pacientes]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _service;
        private readonly IMapper _mapper;

        public PacienteController(IPacienteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var paciente = _service.GetAll();
                var pacienteMapped = _mapper.Map<List<PacienteDto>>(paciente);

                return Ok(pacienteMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var paciente = _service.GetPacienteById(id);
                var pacienteMapped = _mapper.Map<PacienteDto>(paciente);
                if (paciente == null)
                {
                    return NotFound("Paciente Inexistente");
                }
                return Ok(pacienteMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePaciente([FromForm] PacienteDto paciente)
        {
            try
            {
                var pacienteEntity = _service.CreatePaciente(paciente);

                if (pacienteEntity != null)
                {
                    var pacienteCreated = _mapper.Map<PacienteDto>(pacienteEntity);
                    return Ok("Paciente Creado");
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, PacienteDto paciente)
        {
            try
            {
                if (paciente == null)
                {
                    return BadRequest("Completar todos los campos para realizar la actualizacion");
                }

                var pacienteUpdate = _service.GetPacienteById(id);

                if (pacienteUpdate == null)
                {
                    return NotFound("Paciente Inexistente");
                }

                _mapper.Map(paciente, pacienteUpdate);
                _service.Update(pacienteUpdate);

                return Ok("Paciente actualizado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(int id)
        {
            try
            {
                var paciente = _service.GetPacienteById(id);

                if (paciente == null)
                {
                    return NotFound("Paciente Inexistente");
                }

                _service.Delete(paciente);
                return Ok("Paciente eliminado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}



