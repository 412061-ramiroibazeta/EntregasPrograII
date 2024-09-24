using FacturaBack.Entities;

namespace FacturaBack.Data.Interfaces
{
    public interface IFacturaRepository
    {
        bool Save(Factura factura);
        Factura GetById(int nroFactura);
        List<Factura> GetByFechaOFP(DateTime fecha, int fp);
        bool Edit (Factura factura);
        List<Factura> GetAll();
    }
}
