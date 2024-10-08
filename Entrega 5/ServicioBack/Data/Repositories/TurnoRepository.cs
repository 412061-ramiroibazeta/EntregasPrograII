using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly TurnosDbContext _context;

        public TurnoRepository(TurnosDbContext context)
        {
            _context = context;
        }
        public bool Delete(int id, string motivo)
        {
            var turno = _context.TTurnos.Find(id);
            if (turno != null)
            {
                turno.FechaCancelacion = DateTime.Now.Date;
                turno.MotivoCancelacion = motivo;   
            }
            return _context.SaveChanges() > 0;
        }

        public List<TTurno> GetByCliente(string cliente)
        {
            var turnos = _context.TTurnos.Where(t => t.Cliente.ToLower() == cliente.ToLower()).ToList();
            return turnos;
        }

        public TTurno? GetByFechaHora(string fecha, string hora)
        {
            var turno = _context.TTurnos.FirstOrDefault(t => t.Fecha == fecha && t.Hora == hora);
            return turno;
        }

        public bool Save(TTurno turno)
        {
            var existe = _context.TTurnos.Find(turno.Id);
            if (existe == null)
            {
                _context.TTurnos.Add(turno);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Update(TTurno turno)
        {
            var existe = _context.TTurnos.Find(turno.Id);
            if (existe != null)
            {
                existe.Fecha = turno.Fecha;
                existe.Hora = turno.Hora;
                existe.Cliente = turno.Cliente;
                
                return _context.SaveChanges() == 1;                
            }
            else return false;
        }
    }
}
