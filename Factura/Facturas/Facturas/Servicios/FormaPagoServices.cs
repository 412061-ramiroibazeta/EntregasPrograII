using Facturas.Datos.Implementations;
using Facturas.Datos.Interfaces;
using Facturas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios
{
    public class FormaPagoServices
    {
        private IFormaPagoRepository _formaPagoRepository;
        public FormaPagoServices()
        {
            _formaPagoRepository = new FormaPagoRepository();
        }
        public List<FormaPago> GetAll()
        {
            return _formaPagoRepository.GetAll();
        }
        public FormaPago GetById(int id)
        {
            return _formaPagoRepository.GetById(id);
        }
        public bool Save(FormaPago formaPago)
        {
            return _formaPagoRepository.Save(formaPago);
        }
        public bool DeleteById(int id)
        {
            return _formaPagoRepository.DeleteById(id);
        }
    }
}
