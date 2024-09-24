using FacturaBack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturaBack.Data.Interfaces
{
    public interface IFormaPagoRepository
    {
        List<FormaPago> GetAll();
        FormaPago GetById(int id);
        bool Save(FormaPago formaPago);
        bool DeleteById(int id);
    }
}
