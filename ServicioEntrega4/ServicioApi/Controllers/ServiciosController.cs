using Microsoft.AspNetCore.Mvc;
using ServicioBack.Data.Models;
using ServicioBack.Data.Services;

namespace ServicioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : Controller
    {
        private IServicioService _servicioService;
        public ServiciosController(IServicioService servicio)
        {
            _servicioService = servicio;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_servicioService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var servicio = _servicioService.GetById(id);
                if (servicio != null)
                {
                    return Ok(servicio);
                }
                else
                {
                    return StatusCode(404, "No hay resultados coincidentes");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var servicio = _servicioService.GetByName(name);

                if (servicio != null)
                {
                    return Ok(servicio);
                }
                else
                {
                    return StatusCode(404, "No hay resultados coincidentes");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpPatch]
        public IActionResult Update(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (_servicioService.Update(servicio))
                    {
                        return Ok("Se actualizó el servicio");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo actualizar el servicio");
                    }
                }
                else
                {
                    return StatusCode(400, "El servicio proporcionado es inválido");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpDelete]
        public IActionResult LogicalDelete(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (_servicioService.LogicDelete(servicio))
                    {
                        return Ok("Se dio de baja el servicio brindado");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo eliminar el servicio");
                    }
                }
                else
                {
                    return StatusCode(400, "El servicio proporcionado es inválido");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpPost]
        public IActionResult Save(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (_servicioService.Save(servicio))
                    {
                        return Ok("Se guardo el servicio brindado");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo guardar el servicio");
                    }
                }
                else
                {
                    return StatusCode(400, "El servicio proporcionado es inválido");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }
    }
}
