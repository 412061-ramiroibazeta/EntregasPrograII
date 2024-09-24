using System.Data.SqlClient;
using System.Data;
using FacturaBack.Entities;
using FacturaBack.Data.Interfaces;
using FacturaBack.Data.Utils;

namespace FacturaBack.Data.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {

        public List<Factura> GetAll()
        {
            var lst = new List<Factura>();
            var dt = new DataTable();
            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_all_facturas", null);

            foreach (DataRow dr in dt.Rows)
            {
                Factura factura = new Factura();
                factura.NroFactura = Convert.ToInt32(dr["nro_factura"]);
                factura.Fecha = Convert.ToDateTime(dr["fecha"]);

                FormaPago pago = new FormaPago();
                pago.IdFormaPago = Convert.ToInt32(dr["id_forma_pago"]);
                pago.FormaDePago = Convert.ToString(dr["forma_pago"]);

                factura.FormaPago = pago;
                factura.Cliente = Convert.ToString(dr["cliente"]);

                lst.Add(factura);
            }
            return lst;
        }
        public bool Save(Factura factura)
        {
            bool aux = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();
                var cmd = new SqlCommand("sp_insertar_factura", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago.IdFormaPago);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);

                SqlParameter parametro = new SqlParameter("@nro_factura", SqlDbType.Int);
                parametro.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametro);

                cmd.ExecuteNonQuery();

                int nro_factura = (int)parametro.Value;
                int detalleId = 1;
                foreach (var detalle in factura.Detalles)
                {
                    var cmdDetail = new SqlCommand("sp_insert_detalle_factura", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;

                    cmdDetail.Parameters.AddWithValue("@id_detalle_factura", detalleId);
                    cmdDetail.Parameters.AddWithValue("@id_factura", nro_factura);
                    cmdDetail.Parameters.AddWithValue("@id_articulo", detalle.Articulo.IdArticulo);
                    cmdDetail.Parameters.AddWithValue("@precio", detalle.Precio);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                    cmdDetail.ExecuteNonQuery();

                    detalleId++;
                }
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    aux = false;
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return aux;
        }

        public Factura GetById(int nroFactura)
        {
            Factura factura = new Factura();
            var dt = new DataTable();
            List<Parametro> list = new List<Parametro>();
            list.Add(new Parametro("@nro_factura", nroFactura));

            dt = DataHelper.GetInstance().ExecuteSPGet("sp_get_factura", list);
            foreach (DataRow dr in dt.Rows)
            {
                factura.NroFactura = nroFactura;
                factura.Fecha = Convert.ToDateTime(dr["fecha"]);

                FormaPago pago = new FormaPago();
                pago.IdFormaPago = Convert.ToInt32(dr["id_forma_pago"]);
                pago.FormaDePago = Convert.ToString(dr["forma_pago"]);

                factura.FormaPago = pago;
                factura.Cliente = Convert.ToString(dr["cliente"]);
            }

            var dtArt = new DataTable();
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@nro_factura", nroFactura));
            dtArt = DataHelper.GetInstance().ExecuteSPGet("sp_get_detalle_nro_f", lst);
            foreach (DataRow dr in dtArt.Rows)
            {
                DetalleFactura detalle = new DetalleFactura();

                Articulo articulo = new Articulo();
                articulo.IdArticulo = Convert.ToInt32(dr["id_articulo"]);
                articulo.Nombre = Convert.ToString(dr["nombre"]);

                detalle.IdFactura = nroFactura;
                detalle.Articulo = articulo;
                detalle.Cantidad = Convert.ToInt32(dr["cantidad"]);
                detalle.Precio = Convert.ToInt32(dr["precio"]);

                factura.Detalles.Add(detalle);
            }

            return factura;
        }

        public bool Edit(Factura factura)
        {
            bool aux = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmdHeader = new SqlCommand("sp_edit_header_factu", cnn, t);
                cmdHeader.CommandType = CommandType.StoredProcedure;

                cmdHeader.Parameters.AddWithValue("@nro_factura", factura.NroFactura);
                cmdHeader.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago.IdFormaPago);
                cmdHeader.Parameters.AddWithValue("@cliente", factura.Cliente);

                cmdHeader.ExecuteNonQuery();
                ////////////////////////////////////////////
                var cmd = new SqlCommand("sp_delete_detalle", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nro_factura", factura.NroFactura);

                cmd.ExecuteNonQuery();

                int detalleId = 1;
                foreach (var detalle in factura.Detalles)
                {
                    var cmdDetail = new SqlCommand("sp_insert_detalle_factura", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;

                    cmdDetail.Parameters.AddWithValue("@id_detalle_factura", detalleId);
                    cmdDetail.Parameters.AddWithValue("@id_factura", factura.NroFactura);
                    cmdDetail.Parameters.AddWithValue("@id_articulo", detalle.Articulo.IdArticulo);
                    cmdDetail.Parameters.AddWithValue("@precio", detalle.Precio);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detalle.Cantidad);

                    cmdDetail.ExecuteNonQuery();

                    detalleId++;
                }
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                    aux = false;
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return aux;
        }

        public List<Factura> GetByFechaOFP(DateTime fecha, int fp)
        {
            List<Factura> facturas = new List<Factura>();

            var dt = new DataTable();
            List<Parametro> list = new List<Parametro>();
            list.Add(new Parametro("@fecha", fecha));

            if (fp == 0)
            {
                list.Add(new Parametro("@id_forma_pago", DBNull.Value));
            }

            list.Add(new Parametro("@id_forma_pago", fp));

            dt = DataHelper.GetInstance().ExecuteSPGet("sp_consultar_facturas", list);
            foreach (DataRow dr in dt.Rows)
            {
                Factura factura = new Factura();
                factura.NroFactura = Convert.ToInt32(dr["nro_factura"]);
                factura.Fecha = Convert.ToDateTime(dr["fecha"]);

                FormaPago pago = new FormaPago();
                pago.IdFormaPago = Convert.ToInt32(dr["id_forma_pago"]);
                pago.FormaDePago = Convert.ToString(dr["forma_pago"]);

                factura.FormaPago = pago;
                factura.Cliente = Convert.ToString(dr["cliente"]);


                var dtArt = new DataTable();

                List<Parametro> lst = new List<Parametro>();
                lst.Add(new Parametro("@nro_factura", factura.NroFactura));

                dtArt = DataHelper.GetInstance().ExecuteSPGet("sp_get_detalle_nro_f", lst);

                foreach (DataRow dd in dtArt.Rows)
                {
                    DetalleFactura detalle = new DetalleFactura();

                    Articulo articulo = new Articulo();
                    articulo.IdArticulo = Convert.ToInt32(dd["id_articulo"]);
                    articulo.Nombre = Convert.ToString(dd["nombre"]);

                    detalle.IdFactura = factura.NroFactura;
                    detalle.Articulo = articulo;
                    detalle.Cantidad = Convert.ToInt32(dd["cantidad"]);
                    detalle.Precio = Convert.ToInt32(dd["precio"]);

                    factura.Detalles.Add(detalle);
                }
            facturas.Add(factura);
            }

            return facturas;
        }
    }
}
