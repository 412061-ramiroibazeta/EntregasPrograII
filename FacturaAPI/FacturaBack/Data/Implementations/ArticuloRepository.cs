using FacturaBack.Data.Interfaces;
using FacturaBack.Data.Utils;
using FacturaBack.Entities;
using System.Data;
using System.Data.SqlClient;

namespace FacturaBack.Data.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool DeleteById(int id)
        {
            var parameters = new List<Parametro>();
            parameters.Add(new Parametro("@id_articulo", id));
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPUpd("sp_delete_articulo", parameters);
            return filasAfectadas == 1;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            DataTable dt = new DataTable();
            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_all_articulos", null);
            foreach (DataRow dr in dt.Rows)
            {
                Articulo articulo = new Articulo();
                articulo.IdArticulo = Convert.ToInt32(dr["id_articulo"]);
                articulo.Nombre = Convert.ToString(dr["nombre"]);
                articulo.PrecioUnitario = Convert.ToDouble(dr["precio_unitario"]);
                lst.Add(articulo);
            }
            return lst;
        }

        public Articulo GetById(int id)
        {
            Articulo articulo = new Articulo();
            DataTable dt = new DataTable();
            List<Parametro> list = new List<Parametro>();
            list.Add(new Parametro("@id_articulo", id));

            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_articulo_id", list);
            foreach (DataRow dr in dt.Rows)
            {
                articulo.IdArticulo = Convert.ToInt32(dr["id_articulo"]);
                articulo.Nombre = Convert.ToString(dr["nombre"]);
                articulo.PrecioUnitario = Convert.ToDouble(dr["precio_unitario"]);
            }
            return articulo;
        }

        public bool Save(Articulo articulo)
        {
            bool aux = false;
            try
            {
                if (articulo != null)
                {
                    List<Parametro> list = new List<Parametro>();
                    list.Add(new Parametro("@id_articulo", articulo.IdArticulo));
                    list.Add(new Parametro("@nombre", articulo.Nombre));
                    list.Add(new Parametro("@precio_unitario", articulo.PrecioUnitario));
                    if (DataHelper.GetInstance().ExecuteSPUpd("sp_insert_articulos", list) == 1)
                    {
                        aux = true;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                aux = false;
            }

            return aux;
        }
    }
}
