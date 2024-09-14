using ArticulosApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace ArticulosApi.Data
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool DeleteById(int id)
        {
            bool aux = false;

            var lst = new List<SqlParameter>();
            lst.Add(new SqlParameter("id_articulo", id));

            int filasAfectadas = DataHelper.GetInstance().ExecuteSPUpd("sp_delete_articulo", lst);

            if (filasAfectadas == 1) aux = true;

            return aux;
        }

        public List<Articulo> GetAll()
        {
            var dt = new DataTable();
            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_all_articulos", null);
            var lst = new List<Articulo>();
            foreach (DataRow r in dt.Rows)
            {
                var a = new Articulo();

                a.IdArticulo = Convert.ToInt32(r["id_articulo"]);
                a.Nombre = Convert.ToString(r["nombre"]);
                a.PrecioUnitario = Convert.ToDouble(r["precio_unitario"]);

                lst.Add(a);
            }

            return lst;
        }

        public Articulo GetById(int id)
        {
            var a = new Articulo();
            if (id != 0)
            {
                var dt = new DataTable();
                var parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("id_articulo", id));

                dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_articulo_id", parametros);

                foreach (DataRow r in dt.Rows)
                {
                    a.IdArticulo = Convert.ToInt32(r["id_articulo"]);
                    a.Nombre = Convert.ToString(r["nombre"]);
                    a.PrecioUnitario = Convert.ToDouble(r["precio_unitario"]);
                }
            }
            else
            {
                a = null;
            }

            return a;
        }

        public bool Save(Articulo a)
        {
            //VERIFICO SI EXISTE O ES NUEVO
            var existe = GetById(a.IdArticulo);            

            //bool aux = false;
            if (a == null) return false;

            var lst = new List<SqlParameter>();
            lst.Add(new SqlParameter("id_articulo", a.IdArticulo));
            lst.Add(new SqlParameter("Nombre", a.Nombre));
            lst.Add(new SqlParameter("precio_unitario", a.PrecioUnitario));

            if (existe.IdArticulo == 0)
            {
                return DataHelper.GetInstance().ExecuteSPUpd("sp_insert_articulos", lst) == 1;
            }
            else
            {
                return DataHelper.GetInstance().ExecuteSPUpd("sp_update_articulos", lst) == 1;
            }
        }        
    }
}
