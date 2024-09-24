using FacturaBack.Data.Interfaces;
using FacturaBack.Entities;

namespace FacturaBack.Services.Implementation
{
    public class FormaPagoServices
    {
        private IFormaPagoRepository _formaPagoRepository;
        public FormaPagoServices(IFormaPagoRepository fp)
        {
            _formaPagoRepository = fp;
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
