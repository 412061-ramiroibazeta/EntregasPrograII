using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServicioBack.Data.Models;
using ServicioBack.Data.Repositories;
using ServicioBack.Data.Services;

namespace ServicioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepository _turnoService;

        public TurnoController(ITurnoRepository turnoService)
        {
            _turnoService = turnoService;
        }

        //bool Save(TTurno turno);

        [HttpPost]
        public IActionResult SaveTurno([FromQuery] TTurno turno)
        {
            try
            {
                if (Validar(turno))
                {
                    if (_turnoService.Save(turno))
                    {
                        return Ok("Se guardó turno exitosamente");
                    }
                    else
                    {
                        return StatusCode(500, "Error intentar nuevamente");
                    }
                }
                else
                {
                    return StatusCode(500, "El turno no ha pasado todas las validaciones");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        private bool Validar(TTurno turno)
        {
            bool aux = true;
            DateTime fecha = Convert.ToDateTime(turno.Fecha);
            var existe = _turnoService.GetByFechaHora(turno.Fecha, turno.Hora);

            if (fecha > DateTime.Now.AddDays(45) || fecha < DateTime.Now.Date)
            {
                aux = false;
                return aux;
            }

            if(existe != null)
            {
                aux = false;
                return aux;
            }

            if(string.IsNullOrEmpty(turno.Cliente) || turno.Cliente.Length > 100 || string.IsNullOrEmpty(turno.Fecha) || turno.Fecha.Length != 10 || string.IsNullOrEmpty(turno.Hora) || turno.Hora.Length != 5)
            {
                aux = false;
                return aux;
            }

            return aux;
        }

        [HttpGet("clientes")]
        public IActionResult GetByCliente([FromQuery] string cliente)
        {
            try
            {
                if (!string.IsNullOrEmpty(cliente))
                {
                    var turnos = _turnoService.GetByCliente(cliente);
                    if (turnos.Count > 0)
                    {
                        return Ok(turnos);
                    }
                    else
                    {
                        return StatusCode(500, "No hay resultados coincidentes");
                    }
                }
                else
                {
                    return StatusCode(500, "Debe introducir un cliente");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        [HttpGet("FechaHora")]
        public IActionResult GetByFechaHora([FromQuery] string fecha, [FromQuery] string hora)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha) && fecha.Length == 10 && !string.IsNullOrEmpty(hora) && hora.Length == 5)
                {
                    var turno = _turnoService.GetByFechaHora(fecha, hora);

                    if (turno != null)
                    {
                        return Ok(turno);
                    }
                    else
                    {
                        return StatusCode(500, "No hay resultados coincidentes");
                    }
                }
                else
                {
                    return StatusCode(500, "Los parámetros introducidos no cumplen los requisitos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        [HttpPut]
        public IActionResult Update([FromQuery] TTurno turno)
        {
            try
            {
                if(turno != null)
                {
                    if (_turnoService.Update(turno))
                    {
                        return Ok("Se actualizó");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo actualizar");
                    }
                }
                else
                {
                    return StatusCode(500, "El turno no puede ser nullo");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

        [HttpDelete]
        public IActionResult LogicDelete([FromQuery] int id, [FromQuery] string motivo)
        {
            try
            {
                if (id > 0 && !string.IsNullOrEmpty(motivo))
                {
                    if (_turnoService.Delete(id,motivo))
                    {
                        return Ok("Se realizó el borrado correctamente");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo actualizar");
                    }
                }
                else
                {
                    return StatusCode(500, "Los parametros son incorrectos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error");
            }
        }

    }
}
