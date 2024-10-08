using Microsoft.EntityFrameworkCore;
using ServicioBack.Data.Models;
using ServicioBack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Services
{
    public class TurnoService
    {
        private readonly ITurnoRepository _turnoService;

        public TurnoService(ITurnoRepository turnoService)
        {
            _turnoService = turnoService;
        }
        public bool Save(TTurno turno)
        {
            return _turnoService.Save(turno);
        }
        public List<TTurno> GetByCliente(string cliente)
        {
            return _turnoService.GetByCliente(cliente);
        }
        public TTurno? GetByFechaHora(string fecha, string hora)
        {
            return _turnoService.GetByFechaHora(fecha, hora);
        }
        public bool Update(TTurno turno)
        {
            return _turnoService.Update(turno);
        }
        public bool Delete(int id, string motivo)
        {
            return _turnoService.Delete(id, motivo);
        }
    }
}
