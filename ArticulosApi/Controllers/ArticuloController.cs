using ArticulosApi.Data;
using ArticulosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace ArticulosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : Controller
    {
        private IArticuloRepository _articulo;
        public ArticuloController()
        {
            _articulo = new ArticuloRepository();
        }


        [HttpGet("articulos")]
        public IActionResult GetAll()
        {
            var lst = _articulo.GetAll();
            if (lst == null)
            {
                return NotFound();
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var articulo = _articulo.GetById(id);
            if (articulo == null)
            {
                return NotFound("No se encontro el articulo");
            }
            return Ok(articulo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            if (_articulo.DeleteById(id))
            {
                return Ok("Articulo eliminado con éxito");
            }
            else
            {
                return BadRequest("No se pudo eliminar el artículo");
            }
        }

        [HttpPost("articulos")] //Entiendo que se puede usar HttpPatch, pero el post funciona tambien como UPDATE ya que si el articulo ya existe, lo edita. Si leen esto favor de comentarme que sería más conveniente o qué se usa normalmente.
        public IActionResult Save([FromBody] Articulo a)
        {
            if (a != null)
            {
                _articulo.Save(a);
                return Ok("Guardado con exito");
            }
            else
            {
                return BadRequest("Se debe insertar un articulo valido");
            }
        }
    }
}
