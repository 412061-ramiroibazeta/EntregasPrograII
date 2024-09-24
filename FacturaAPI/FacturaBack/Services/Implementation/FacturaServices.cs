using FacturaBack.Data.Interfaces;
using FacturaBack.Entities;
using FacturaBack.Services.Interfaces;

namespace FacturaBack.Services.Implementation
{
    public class FacturaServices : IFacturaService
    {
        private IFacturaRepository _facturaRepository;
        public FacturaServices(IFacturaRepository factura)
        {
            _facturaRepository = factura;
        }

        public bool Save(Factura factura)
        {
            return _facturaRepository.Save(factura);
        }
        public Factura GetById(int nroFactura)
        {
            return _facturaRepository.GetById(nroFactura);
        }
        public bool Edit(Factura factura)
        {
            return _facturaRepository.Edit(factura);
        }
        public List<Factura> GetByFechaOFP(DateTime fecha, int fp)
        {
            return _facturaRepository.GetByFechaOFP(fecha,fp);
        }

        public List<Factura> GetAll()
        {
            return _facturaRepository.GetAll();
        }
    }
}
