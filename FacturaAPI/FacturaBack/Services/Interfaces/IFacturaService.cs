using FacturaBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturaBack.Services.Interfaces
{
    public interface IFacturaService
    {
        bool Save(Factura factura);
        Factura GetById(int nroFactura);
        List<Factura> GetByFechaOFP(DateTime fecha, int fp);
        bool Edit(Factura factura);
        List<Factura> GetAll();
    }
}
