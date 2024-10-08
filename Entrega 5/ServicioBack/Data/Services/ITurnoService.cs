using ServicioBack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioBack.Data.Services
{
    public interface ITurnoService
    {
        bool Save(TTurno turno);
        List<TTurno> GetByCliente(string cliente);
        TTurno? GetByFechaHora(string fecha, string hora);
        bool Update(TTurno turno);
        bool Delete(int id, string motivo);
    }
}
