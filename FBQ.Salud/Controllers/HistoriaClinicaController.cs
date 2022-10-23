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
        /// <summary>
        ///  Endpoint dedicado a la obtencion de una lista de historias clinicas. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(HistoriaClinicaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        ///  Endpoint dedicado a la obtener una historia clinica Por Id.
        /// </summary>
        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(HistoriaClinicaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        ///  Endpoint dedicado a la creación una historia clinica.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        ///  Endpoint dedicado a la actualización de una historia clinica Por Id.
        /// </summary>
        [HttpPut]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        ///  Endpoint dedicado a la eliminación de un historia clinica Por Id.
        /// </summary>
        [HttpDelete()]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

