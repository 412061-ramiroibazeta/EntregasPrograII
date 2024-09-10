using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Dominio
{
    public class DetalleFactura
    {
        public int IdFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal()
        {
            return Cantidad * Precio;
        }

    }
}
