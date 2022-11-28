using AutoMapper;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBQ.Salud_Presentation.Controllers
{
    [Route("api/pacientes")]
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
        /// <summary>
        ///  Endpoint dedicado a la obtencion de una lista de pacientes. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PacienteRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PacienteRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                var patients = _service.GetAll();

                if (patients.Count() == 0)
                {

                    return NotFound(patients);
                }
                else
                {
                    return Ok(patients);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a obtener un paciente Por Id.
        /// </summary>
        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyId(int id)
        {
            try
            {
                var paciente = _service.GetPacienteById(id);

                if (paciente.Success)
                {
                    return Ok(paciente);
                }
                return NotFound(paciente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a obtener un paciente Por DNI.
        /// </summary>
        [HttpGet]
        [Route("dni")]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetbyDni(int dni)
        {
            try
            {
                var paciente = _service.GetPacienteByDni(dni.ToString());

                if (paciente.Success)
                {
                    return Ok(paciente);
                }
                return NotFound(paciente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a la creación un paciente.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult CreatePaciente([FromForm] PacienteDto paciente)
        {
            try
            {
                var userNuevo = _service.Add(paciente);

                if (userNuevo.Success)
                {
                    return new JsonResult(userNuevo) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(userNuevo) { StatusCode = 409 };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a la actualización de un paciente Por Id.
        /// </summary>
        [HttpPut]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePaciente(int id, PacienteDto paciente)
        {
            try
            {
                var UserResponse = _service.Update(id, paciente);

                if (UserResponse.Success)
                {
                    return Ok(UserResponse);
                }
                else
                {
                    return NotFound(UserResponse);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a la eliminación de un paciente Por Id.
        /// </summary>
        [HttpDelete()]
        [Route("id")]
        [ProducesResponseType(typeof(Response),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePaciente(int id)
        {
            try
            {
                var paciente = _service.Delete(id);

                if (paciente.Success == false)
                {
                    return NotFound();
                }
                else
                    return Ok(paciente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}



