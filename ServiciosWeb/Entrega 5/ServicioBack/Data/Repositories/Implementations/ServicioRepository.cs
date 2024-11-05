using Microsoft.EntityFrameworkCore;
using ServicioBack.Data.Models;
using ServicioBack.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Repositories.Implementations
{
    public class ServicioRepository : IServicioRepository
    {
        private TurnosDbContext _context;

        public ServicioRepository(TurnosDbContext context)
        {
            _context = context;
        }

        public async Task<List<TServicio>> GetAll()
        {
            return await _context.TServicios.ToListAsync();
        }

        public async Task<TServicio?> GetById(int id)
        {
            return await _context.TServicios.FindAsync(id);
        }

        public async Task<TServicio?> GetByName(string name)
        {
            return await _context.TServicios.FirstOrDefaultAsync(e => e.Nombre == name);
        }

        public async Task<bool> LogicDelete(TServicio servicio)
        {
            var s = await _context.TServicios.FirstOrDefaultAsync(e => e.Id == servicio.Id);

            if (s != null)
            {
                try
                {
                    s.EnPromocion = string.Empty;
                    s.Costo = 0;

                    _context.TServicios.Update(s);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> Save(TServicio servicio)
        {
            if (servicio != null)
            {
                try
                {                    
                    servicio.Id = await _context.TServicios.MaxAsync(s => s.Id) + 1;

                    await _context.TServicios.AddAsync(servicio);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(TServicio servicio)
        {
            var actualizar = await _context.TServicios.FindAsync(servicio.Id);
            if (actualizar != null)
            {
                _context.Entry(actualizar).CurrentValues.SetValues(servicio);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

    }
}
