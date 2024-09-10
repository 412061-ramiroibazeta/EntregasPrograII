using Facturas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Datos.Interfaces
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> GetAll();
        FormaPago GetById(int id);
        bool Save(FormaPago formaPago);
        bool DeleteById(int id);
    }
}
