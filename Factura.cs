using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion_Jugueteria_112762.Entidades.Formularios////LEGAJO 112762 PIZARRO CABALLERO, DOLORES
{
    class Factura
    {
        public int FacturaNro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public double Total { get; set; }
        public int FormaPago { get; set; }
        //prop List de detalleFactura, ya que 1 factura incluirá uno o varios detalles  
        public List <DetalleFactura> Detalles { get; set; }
        public double Descuento { get; set; }

        //constructor que incializa a la list Detalles
        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }
        //metodos: agregarDetalle, QuitarDetalle y CalculoTotal
        public void AgregarDetalle (DetalleFactura detalle)
        {
            Detalles.Add(detalle);
        }
        public void QuitarDetalle(int indice)
        {
            Detalles.RemoveAt(indice);
        }
        //ciclo que me recorre todos los detalles en la lista y por cad item
        //lo voy a ir acumulando, sumarizando los subtotales(ojo a esto aun no se le ha restado dto)
        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleFactura item in Detalles)
            {
                total += item.calcularSubTotal();
            }
            return total;
        }

        public bool Confirmar()
        {
            bool estado = true;
            SqlConnection conexion = new SqlConnection();
            SqlTransaction transaccion = null;

            try
            {
                conexion.ConnectionString = @"Data Source=LAPTOP-HM31I94B\SQLEXPRESS;Initial Catalog=Factuacion_Jugueteria_112762;Integrated Security=True";
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "SP_INSERTAR_MAESTRO";
                comando.Parameters.AddWithValue("@CLIENTE", this.Cliente);
                comando.Parameters.AddWithValue("@DESCUENTO", this.Descuento);
                comando.Parameters.AddWithValue("@TOTAL", this.Total);
                comando.Parameters.AddWithValue("@ID_FORMA_PAGO", this.FormaPago+1);
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@factura_nro";
                //param.SqlDbType= System.Data.SqlDbType.Int;
                //param.Direction = System.Data.ParameterDirection.Output;
                comando.Parameters.Add("@FACTURA_NRO", SqlDbType.Int);
                comando.Parameters["@FACTURA_NRO"].Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                this.FacturaNro = Convert.ToInt32(comando.Parameters["@FACTURA_NRO"].Value);



                foreach (DetalleFactura item in Detalles)
                {
                    int DetalleNro = 1;
                    SqlCommand comandoDetalle = new SqlCommand();
                    comandoDetalle.Connection = conexion;
                    comandoDetalle.Transaction = transaccion;
                    comandoDetalle.CommandType = CommandType.StoredProcedure;
                    comandoDetalle.CommandText = "SP_INSERTAR_DETALLE";
                    comandoDetalle.Parameters.AddWithValue("@factura_nro", this.FacturaNro);
                    comandoDetalle.Parameters.AddWithValue("@detalle", DetalleNro);
                    comandoDetalle.Parameters.AddWithValue("@id_articulo", item.Articulo.ArticuloNro);
                    comandoDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    DetalleNro++;


                }
                transaccion.Commit();
            }
            catch(Exception ex)
            {
                transaccion.Rollback();
                estado = false;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            
            
            
            
            
            return estado;
        }
    }
}
