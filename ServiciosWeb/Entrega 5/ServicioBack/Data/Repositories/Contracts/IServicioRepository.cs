using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Repositories.Contracts
{
    public interface IServicioRepository
    {
        // registrar, consultar (con filtros), editar y registrar la baja lógica de servicios.
        Task<List<TServicio>> GetAll();
        Task<TServicio?> GetById(int id);
        Task<TServicio?> GetByName(string name);
        Task<bool> Update(TServicio servicio);
        Task<bool> LogicDelete(TServicio servicio);
        Task<bool> Save(TServicio servicio);
    }
}
