using Facturas.Datos.Interfaces;
using Facturas.Datos.Utils;
using Facturas.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Datos.Implementations
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public bool DeleteById(int id)
        {
            var parameters = new List<Parametro>();
            parameters.Add(new Parametro("@id_forma_pago", id));
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPUpd("sp_delete_tipo_pago", parameters);
            return filasAfectadas == 1;
        }

        public List<FormaPago> GetAll()
        {
            var Formas = new List<FormaPago>();
            var dt = new DataTable();
            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_all_forma_pago", null);
            foreach (DataRow dr in dt.Rows)
            {
                FormaPago formaPago = new FormaPago();
                formaPago.IdFormaPago = (int)dr["id_forma_pago"];
                formaPago.FormaDePago = Convert.ToString(dr["forma_pago"]);
                Formas.Add(formaPago);
            }
            return Formas;
        }

        public FormaPago GetById(int id)
        {
            var dt = new DataTable();

            var parameters = new List<Parametro>();
            parameters.Add(new Parametro("@id_forma_pago", id));

            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_forma_pago", null);

            FormaPago formaPago = new FormaPago();

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                formaPago.IdFormaPago = (int)dr["id_forma_pago"];
                formaPago.FormaDePago = Convert.ToString(dr["forma_pago"]);
            }
            if (dt.Rows.Count == 0)
            {
                formaPago = null;
            }
            return formaPago;
        }

        public bool Save(FormaPago formaPago)
        {
            bool aux = false;
            try
            {
                if (formaPago != null)
                {
                    List<Parametro> list = new List<Parametro>();
                    list.Add(new Parametro("@id_forma_pago", formaPago.IdFormaPago));
                    list.Add(new Parametro("@forma_pago", formaPago.FormaDePago));                    
                    if (DataHelper.GetInstance().ExecuteSPUpd("sp_insert_forma_pago", list) == 1)
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
