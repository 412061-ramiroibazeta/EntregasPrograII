using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Repositories
{
    public interface IServicioRepository
    {
        // registrar, consultar (con filtros), editar y registrar la baja lógica de servicios.
        List<TServicio> GetAll();
        TServicio? GetById(int id);
        TServicio? GetByName(string name);
        bool Update(TServicio servicio);
        bool LogicDelete(TServicio servicio);
        bool Save(TServicio servicio);
    }
}
