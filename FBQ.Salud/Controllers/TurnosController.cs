﻿using AutoMapper;
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
        /// <summary>
        ///  Endpoint dedicado a la obtencion de una lista de atenciones. 
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(TurnoRequest), StatusCodes.Status200OK)]
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
        /// <summary>
        ///  Endpoint dedicado a la obtencion de una atencion.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TurnoDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTurnosById(int id)
        {
            try
            {
                var historia = _service.GetTurnoById(id);

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
        ///  Endpoint dedicado a la creación de una atencion.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTurno([FromBody] TurnoDTOForCreated turno)
        {
            try
            {
                // Llama al servicio para crear el turno
                var turnoNuevo = _service.CreateTurno(turno);

                if (turnoNuevo.Success)
                {
                    // Retorna el objeto turnoNuevo con un código 201 (Created)
                    return CreatedAtAction(nameof(CreateTurno), null, turnoNuevo); // Sin incluir ID
                }
                else
                {
                    // Retorna un código 404 si algo falla en la lógica del servicio
                    return NotFound(new { message = turnoNuevo.Message });
                }
            }
            catch (Exception e)
            {
                // Manejar cualquier excepción inesperada
                return BadRequest(new { message = e.Message });
            }
        }


        /// <summary>
        ///  Endpoint dedicado a la actualización de un turno.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTurno(int id, TurnoDTOForUpdate turno)
        {
            try
            {
                var TurnoResponse = _service.Update(id, turno);

                if (TurnoResponse.Success)
                {
                    return Ok(TurnoResponse);
                }
                else
                {
                    return NotFound(TurnoResponse);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///  Endpoint dedicado a la eliminacion de un turno. 
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTurno(int id)
        {
            try
            {
                var turno = _service.Delete(id);

                if (turno.Success == false)
                {
                    return NotFound();
                }
                else
                    return Ok(turno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}




