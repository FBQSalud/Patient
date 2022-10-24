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
        public IActionResult GetAll(bool fecha, int? pacienteId)
        {
            try
            {
                var historias = _service.GetAll(fecha, pacienteId);

                if (historias.Count() == 0)
                {

                    return NotFound(historias);
                }
                else
                {
                    return Ok(historias);
                }
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
                var historia = _service.GetHistoriaClinicaById(id);

                if (historia.Success)
                {
                    return Ok(historia);
                }
                return NotFound(historia);
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
                var HistoriaNueva = _service.CreateHistoriaClinica(historiaClinica);

                if (HistoriaNueva.Success)
                {
                    return new JsonResult(HistoriaNueva) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(HistoriaNueva) { StatusCode = 404 };
                }
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
        public IActionResult UpdateHistoriaClinica(int id, HistoriaClinicaDTOForUpdate historiaClinica)
        {
            try
            {
                var HistoryResponse = _service.Update(id, historiaClinica);

                if (HistoryResponse.Success)
                {
                    return Ok(HistoryResponse);
                }
                else
                {
                    return NotFound(HistoryResponse);
                }
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
                var historias = _service.Delete(id);

                if (historias.Success == false)
                {
                    return NotFound();
                }
                else
                    return Ok(historias);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

