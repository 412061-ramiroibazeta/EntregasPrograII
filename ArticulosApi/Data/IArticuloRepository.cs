using ArticulosApi.Models;

namespace ArticulosApi.Data
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool DeleteById(int id);
        bool Save(Articulo articulo);
    }
}
