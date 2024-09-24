using FacturaBack.Data.Interfaces;
using FacturaBack.Entities;
using FacturaBack.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FacturaApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class FacturaController : Controller
    {
        private IFacturaService _facturaRepository;

        public FacturaController(IFacturaService facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        // GET: FacturaController
        [HttpGet]
        public IActionResult GetFacturas()
        {
            return Ok(_facturaRepository.GetAll());
        }

        // GET: FacturaController/Id

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var resultado = _facturaRepository.GetById(id);

            if (resultado == null)
            {
                return NotFound("No existe la factura");
            }

            return Ok(resultado);
        }

        [HttpGet("{fecha}/{idFormaPago}")]
        public IActionResult GetByDateAndPayment(DateTime fecha, int idFormaPago)
        {
            var resultado = _facturaRepository.GetByFechaOFP(fecha, idFormaPago);
            
            if (resultado.Count == 0)
            {
                return NotFound("No existen facturas con esos parámetros");
            }
            
            foreach (var item in resultado)
            {
                if(item.NroFactura == 0)
                {
                    return NotFound("No existen facturas con esos parámetros");
                }
            }

            return Ok(resultado);
        }


        // GET: FacturaController/Create
        [HttpPost]
        public IActionResult Save(Factura f)
        {
            if (!_facturaRepository.Save(f))
            {
                return StatusCode(500, "Ha habido un error");
            }
            return Ok(f);
        }

        // GET: FacturaController/Edit/5
        [HttpPatch]
        public IActionResult Edit(Factura f)
        {
            if (!_facturaRepository.Edit(f))
            {
                return StatusCode(500, "Ha habido un error");
            }
            return Ok(f);
        }
    }
}
