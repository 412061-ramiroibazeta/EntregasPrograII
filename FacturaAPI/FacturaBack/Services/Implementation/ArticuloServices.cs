using FacturaBack.Data.Interfaces;
using FacturaBack.Entities;

namespace FacturaBack.Services.Implementation
{
    public class ArticuloServices
    {
        private IArticuloRepository _articuloRepository;
        public ArticuloServices(IArticuloRepository articulo)
        {
            _articuloRepository = articulo;
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
