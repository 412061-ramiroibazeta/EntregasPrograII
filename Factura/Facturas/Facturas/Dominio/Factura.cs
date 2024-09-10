using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void AniadirDetalle(DetalleFactura detalle)
        {            
            bool repetido = false;
            
            foreach (DetalleFactura d in Detalles)
            {
                if (detalle.Articulo.IdArticulo == d.Articulo.IdArticulo)
                {
                    d.Cantidad += detalle.Cantidad;
                    repetido = true; 
                    break; 
                }
            }
            if (!repetido)
            {
                Detalles.Add(detalle);
            }
        }
        public void QuitarDetalle(int indice)
        {
            Detalles.RemoveAt(indice);
        }
        public double Total()
        {
            double total = 0;
            foreach (DetalleFactura df in Detalles)
            {
                total += df.Subtotal();
            }
            return total;
        }
        public override string ToString()
        {
            return NroFactura.ToString();                ;
        }
    }
}
