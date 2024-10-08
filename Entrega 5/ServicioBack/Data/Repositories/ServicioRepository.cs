using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private TurnosDbContext _context;

        public ServicioRepository(TurnosDbContext context)
        {
            _context = context;
        }

        public List<TServicio> GetAll()
        {
            return _context.TServicios.ToList();
        }

        public TServicio? GetById(int id)
        {
            return _context.TServicios.Find(id);
        }

        public TServicio? GetByName(string name)
        {
            return _context.TServicios.FirstOrDefault(e => e.Nombre == name);
        }

        public bool LogicDelete(TServicio servicio)
        {
            var s = _context.TServicios.FirstOrDefault(e => e.Id == servicio.Id);

            if (s != null)
            {
                try
                {
                    s.EnPromocion = string.Empty;
                    s.Costo = 0;

                    _context.TServicios.Update(s);
                    return _context.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else return false;
        }

        public bool Save(TServicio servicio)
        {
            if (servicio != null)
            {
                try
                {
                    _context.TServicios.Add(servicio);
                    return _context.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            else return false;
        }

        public bool Update(TServicio servicio)
        {
            var actualizar = _context.TServicios.Find(servicio.Id);
            if (actualizar != null)
            {
                _context.Entry(actualizar).CurrentValues.SetValues(servicio);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
