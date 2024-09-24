using System.Data;
using System.Data.SqlClient;

namespace FacturaBack.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _cnn;
        public DataHelper()
        {
            _cnn = new SqlConnection("Data Source=DESKTOP-GVURFS8\\SQLEXPRESS;Initial Catalog=Facturas;Integrated Security=True;");
        }
        public static DataHelper GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DataHelper();
            }
            return _instancia;
        }
        public SqlConnection GetConnection()
        {
            return _cnn;
        }
        public DataTable ExecuteSPGet(string sp, List<Parametro>? lst) // sirve para ingresar datos
        {
            DataTable dt = new DataTable();
            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp, _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    foreach (var param in lst)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }
                dt.Load(cmd.ExecuteReader());
                _cnn.Close();
            }
            catch (SqlException)
            {
                dt = null;
            }
            return dt;
        }
        public int ExecuteSPUpd(string sp, List<Parametro>? lst) //sirve para actualizar(update)
        {
            int filasAfectadas;
            if (_cnn.State == ConnectionState.Open)
            {
                _cnn.Close();
            }
            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp, _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    foreach (var param in lst)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }
                filasAfectadas = cmd.ExecuteNonQuery();
                _cnn.Close();
            }
            catch (SqlException)
            {
                filasAfectadas = 0;
            }
            finally
            {                
                if (_cnn.State == ConnectionState.Open)
                {
                    _cnn.Close();
                }
            }
            return filasAfectadas;
        }
        
    }
}
