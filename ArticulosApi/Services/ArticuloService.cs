using ArticulosApi.Data;
using ArticulosApi.Models;

namespace ArticulosApi.Services
{
    public class ArticuloService
    {
        private readonly IArticuloRepository _articulo;
        public ArticuloService()
        {
            _articulo = new ArticuloRepository();
        }
        public List<Articulo> GetAll()
        {
            return _articulo.GetAll();
        }
        public Articulo GetById(int id)
        {
            return _articulo.GetById(id);
        }
        public bool DeleteById(int id)
        {
            return _articulo.DeleteById(id);
        }
        public bool Save(Articulo articulo)
        {
            return _articulo.Save(articulo);
        }
    }
}
