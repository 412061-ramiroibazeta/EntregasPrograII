using ServicioBack.Data.Models;
using ServicioBack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _service;
        public ServicioService(IServicioRepository service)
        {
            _service = service;
        }
        public List<TServicio> GetAll()
        {
            return _service.GetAll();
        }

        public TServicio? GetById(int id)
        {
            return _service.GetById(id);
        }

        public TServicio? GetByName(string name)
        {
            return _service.GetByName(name);
        }

        public bool LogicDelete(TServicio servicio)
        {
            return _service.LogicDelete(servicio);
        }

        public bool Save(TServicio servicio)
        {
            return _service.Save(servicio);
        }

        public bool Update(TServicio servicio)
        {
            return _service.Update(servicio);
        }
    }
}
