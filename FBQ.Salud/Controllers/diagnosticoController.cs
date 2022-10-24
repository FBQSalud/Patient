using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBQ.Salud_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class diagnosticoController : ControllerBase
    {
        private readonly IDiagnosticoServices _service;
        public diagnosticoController(IDiagnosticoServices service)
        {
            _service = service;
        }

        /// <summary>
        ///  Endpoint dedicado a la obtencion de una lista de diagnosticos. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DiagnosticoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                var turnos = _service.GetAll();

                if (turnos.Count() == 0)
                {

                    return NotFound(turnos);
                }
                else
                {
                    return Ok(turnos);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
