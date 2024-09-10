using Facturas.Datos.Utils;
using Facturas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Datos.Interfaces
{
    public interface IFacturaRepository
    {
        bool Save(Factura factura);
        Factura GetById(int nroFactura);
        public bool Edit (Factura factura);
    }
}
