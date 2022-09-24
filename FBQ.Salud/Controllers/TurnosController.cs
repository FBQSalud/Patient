using AutoMapper;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FBQ.Salud_Presentation.Controllers
{
    [Route("api/turnos")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoServices _service;
        private readonly IMapper _mapper;
        public TurnosController(ITurnoServices service, 
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var turno = _service.GetAll();
                var turnoMapped = _mapper.Map<List<TurnoDTO>>(turno);

                return Ok(turnoMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurnosById(int id)
        {
            try
            {
                var turno = _service.GetTurnoById(id);
                var turnoMapped = _mapper.Map<TurnoDTO>(turno);
                if (turno == null)
                {
                    return NotFound("Turno Inexistente");
                }
                return Ok(turnoMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTurno([FromForm] TurnoDTO turno)
        {
            try
            {
                var turnoEntity = _service.CreateTurno(turno);

                if (turnoEntity != null)
                {
                    var turnoCreated = _mapper.Map<TurnoDTO>(turnoEntity);
                    return Ok("Turno Creado");
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTurno(int id, TurnoDTO turno)
        {
            try
            {
                if (turno == null)
                {
                    return BadRequest("Completar todos los campos para realizar la actualizacion");
                }

                var turnoUpdate = _service.GetTurnoById(id);

                if (turnoUpdate == null)
                {
                    return NotFound("Turno Inexistente");
                }

                _mapper.Map(turno, turnoUpdate);
                _service.Update(turnoUpdate);

                return Ok("Turno actualizado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurno(int id)
        {
            try
            {
                var turno = _service.GetTurnoById(id);

                if (turno == null)
                {
                    return NotFound("Turno Inexistente");
                }

                _service.Delete(turno);
                return Ok("Turno eliminado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}




