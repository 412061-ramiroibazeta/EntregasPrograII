using Facturas.Datos.Implementations;
using Facturas.Datos.Interfaces;
using Facturas.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios
{
    public class ArticuloServices
    {
        private IArticuloRepository _articuloRepository;
        public ArticuloServices()
        {
            _articuloRepository = new ArticuloRepository();
        }        
        public List<Articulo> GetAll()
        {
            return _articuloRepository.GetAll();
        }
        public Articulo GetById(int id)
        {
            return _articuloRepository.GetById(id);
        }
        public bool Save(Articulo articulo)
        {
            return _articuloRepository.Save(articulo);
        }
        public bool DeleteById(int id)
        {
            return _articuloRepository.DeleteById(id);
        }
    }
}
