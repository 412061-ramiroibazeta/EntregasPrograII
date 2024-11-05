using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Services.Contracts
{
    public interface IServicioService
    {
        Task<List<TServicio>>  GetAll();
        Task<TServicio?> GetById(int id);
        Task<TServicio?> GetByName(string name);
        Task<bool> Update(TServicio servicio);
        Task<bool> LogicDelete(TServicio servicio);
        Task<bool> Save(TServicio servicio);
    }
}
