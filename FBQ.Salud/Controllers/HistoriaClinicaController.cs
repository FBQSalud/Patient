using AutoMapper;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FBQ.Salud_Presentation.Controllers
{
    [Route("api/historiaClinica")]
    [ApiController]
    public class HistoriaClinicaController : ControllerBase
    {
        private readonly IHistoriaClinicaServices _service;
        private readonly IMapper _mapper;

        public HistoriaClinicaController(IHistoriaClinicaServices service,
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
                var historiaClinica = _service.GetAll();
                var historiaClinicaMapped = _mapper.Map<List<HistoriaClinicaDTO>>(historiaClinica);

                return Ok(historiaClinicaMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoriaClinicaById(int id)
        {
            try
            {
                var historiaClinica = _service.GetHistoriaClinicaById(id);
                var historiaClinicaMapped = _mapper.Map<HistoriaClinicaDTO>(historiaClinica);
                if (historiaClinica == null)
                {
                    return NotFound("Historia Clinica Inexistente");
                }
                return Ok(historiaClinicaMapped);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateHistoriaClinica([FromForm] HistoriaClinicaDTO historiaClinica)
        {
            try
            {
                var historiaClinicaEntity = _service.CreateHistoriaClinica(historiaClinica);

                if (historiaClinicaEntity != null)
                {
                    var historiaClinicaCreated = _mapper.Map<HistoriaClinicaDTO>(historiaClinicaEntity);
                    return Ok("Historia Clinica Creada");
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHistoriaClinica(int id, HistoriaClinicaDTO historiaClinica)
        {
            try
            {
                if (historiaClinica == null)
                {
                    return BadRequest("Completar todos los campos para realizar la actualizacion");
                }

                var historiaClinicaUpdate = _service.GetHistoriaClinicaById(id);

                if (historiaClinicaUpdate == null)
                {
                    return NotFound("Historia Clinica Inexistente");
                }

                _mapper.Map(historiaClinica, historiaClinicaUpdate);
                _service.Update(historiaClinicaUpdate);

                return Ok("HistoriaClinica actualizada");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHistoriaClinica(int id)
        {
            try
            {
                var historiaClinica = _service.GetHistoriaClinicaById(id);

                if (historiaClinica == null)
                {
                    return NotFound("Historia Clinica Inexistente");
                }

                _service.Delete(historiaClinica);
                return Ok("Historia Clinica eliminada");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

