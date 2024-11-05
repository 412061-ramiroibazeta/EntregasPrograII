using ServicioBack.Data.Models;
using ServicioBack.Data.Repositories.Contracts;
using ServicioBack.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Services.Implementations
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _service;
        public ServicioService(IServicioRepository service)
        {
            _service = service;
        }
        public async Task<List<TServicio>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<TServicio?> GetById(int id)
        {
            return await _service.GetById(id);
        }

        public async Task<TServicio?> GetByName(string name)
        {
            return await _service.GetByName(name);
        }

        public async Task<bool> LogicDelete(TServicio servicio)
        {
            return await _service.LogicDelete(servicio);
        }

        public async Task<bool> Save(TServicio servicio)
        {
            return await _service.Save(servicio);
        }

        public async Task<bool> Update(TServicio servicio)
        {
            return await _service.Update(servicio);
        }
    }
}
