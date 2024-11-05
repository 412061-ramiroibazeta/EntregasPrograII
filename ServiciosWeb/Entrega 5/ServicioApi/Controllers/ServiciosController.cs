using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ServicioBack.Data.Models;
using ServicioBack.Data.Services.Contracts;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var servicios = await _servicioService.GetAll();
                if(servicios.Count > 0)
                {
                return Ok(servicios);
                }
                else
                {
                    return StatusCode(404, "No hay servicios");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var servicio = await _servicioService.GetById(id);
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
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var servicio = await _servicioService.GetByName(name);

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
        public async Task<IActionResult> Update(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (await _servicioService.Update(servicio))
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
        public async Task<IActionResult> LogicalDelete(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (await _servicioService.LogicDelete(servicio))
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
        public async Task<IActionResult> Save(TServicio servicio)
        {
            try
            {
                if (servicio != null)
                {
                    if (await _servicioService.Save(servicio))
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
