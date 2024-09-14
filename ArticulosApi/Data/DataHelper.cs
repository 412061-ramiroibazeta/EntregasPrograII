using System.Data;
using System.Data.SqlClient;

namespace ArticulosApi.Data
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _cnn;
        private DataHelper()
        {
            _cnn = new SqlConnection(Properties.Resources.cnnStringPC);
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

        public DataTable ExecuteSPGet(string sp, List<SqlParameter> lst)
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
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
                _cnn.Close();
            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                if (_cnn.State == ConnectionState.Open)
                {
                    _cnn.Close();
                }
            }
            return dt;
        }

        public int ExecuteSPUpd(string sp, List<SqlParameter> lst)
        {
            int filasAfectadas = 0;

            try
            {
                _cnn.Open();
                var cmd = new SqlCommand(sp, _cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lst != null)
                {
                    foreach (var param in lst)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
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
