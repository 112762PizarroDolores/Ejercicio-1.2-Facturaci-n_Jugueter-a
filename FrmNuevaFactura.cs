using Facturacion_Jugueteria_112762.Entidades;
using Facturacion_Jugueteria_112762.Entidades.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturacion_Jugueteria_112762.Formularios //LEGAJO 112762 PIZARRO CABALLERO, DOLORES
{
    public partial class FrmNuevaFactura : Form

    {
        Factura nueva = new Factura();
        
        public FrmNuevaFactura()
        {
            InitializeComponent();
        }

        private void FrmNuevaFactura_Load(object sender, EventArgs e)
        {
            ProximaFactura();
            CargarArticulos();
            CargarFormasPago();
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtCliente.Text = "Consumidor Final";
            txtDescuento.Text = "0";
            txtCantidad.Text = "1";
            dgvDetalles.ScrollBars = ScrollBars.None;
        }

        private void CargarFormasPago()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = @"Data Source=LAPTOP-HM31I94B\SQLEXPRESS;Initial Catalog=Factuacion_Jugueteria_112762;Integrated Security=True";
            conexion.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_FORMAS_PAGO";

            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();

            cboFormaPago.DataSource = tabla;
            cboFormaPago.DisplayMember = "nombre";
            cboFormaPago.ValueMember = "id_forma_pago";
            cboFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarArticulos()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = @"Data Source=LAPTOP-HM31I94B\SQLEXPRESS;Initial Catalog=Factuacion_Jugueteria_112762;Integrated Security=True";
            conexion.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_ARTICULOS";

            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();

            cboArticulo.DataSource = tabla;
            cboArticulo.DisplayMember = "n_articulo";
            cboArticulo.ValueMember = "id_articulo";
            cboArticulo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ProximaFactura()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = @"Data Source=LAPTOP-HM31I94B\SQLEXPRESS;Initial Catalog=Factuacion_Jugueteria_112762;Integrated Security=True";
            conexion.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_PROXIMO_ID";

            SqlParameter param = new SqlParameter("@NEXT", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            comando.Parameters.Add(param);
            comando.ExecuteNonQuery();
            lblFacturaNro.Text += param.Value;
            conexion.Close();


            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboArticulo.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un artículo", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(cboFormaPago.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar una forma de pago", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))

            {
                MessageBox.Show("Debes ingresar una cantidad válida", "control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["colArt"].Value.ToString().Equals(cboArticulo.Text))
                {
                    MessageBox.Show("Este producto ya está cargado", "control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //cada fila de mi grilla=1 item de mi comboArticulo
            DataRowView item = (DataRowView)cboArticulo.SelectedItem;
            //Rescato los valores que necesito para mi grilla
            int Art = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();
            double pre = Convert.ToDouble(item.Row.ItemArray[2]);
            int cant = Convert.ToInt32(txtCantidad.Text);

            //Creo Articulo
            Articulo a = new Articulo(Art, nom, pre);
            //creo detalle a partir del cosntructor de clase detalle que pedia: un objeto articulo y una cantidad

            DetalleFactura detalle = new DetalleFactura(a, cant);

            nueva.AgregarDetalle(detalle);
            //la siguiente linea me permite ver en la grilla los valores que provienen del elemento seleccionado en el cboArtic, txt cantidad al presionar AGREGAR
            dgvDetalles.Rows.Add(new object[] { Art, nom, pre, cant });
            calcularTotales();
            
        }

        private void calcularTotales()
        {
            txtSubTotal.Text = nueva.CalcularTotal().ToString();
            double desc = nueva.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
            txtTotal.Text = (nueva.CalcularTotal() - desc).ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //VALIDAR TODO LO QUE VA A IR A LA BD
            if(txtCliente.Text==String.Empty)
            {
                MessageBox.Show("Debe ingresar un cliente", "control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCliente.Focus();
                return;

            }
            if (dgvDetalles.Rows.Count==0)
            {
                MessageBox.Show("Debe ingresar un detalle al menos", "control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboArticulo.Focus();
                return;
            }
            GuardarFactura();

        }
        //este neuvo metodo dira a la factura todos los datos que debe llevar
        private void GuardarFactura()
        {
            
            nueva.Fecha = Convert.ToDateTime(txtFecha.Text);
            nueva.Cliente = txtCliente.Text;
            nueva.Descuento = Convert.ToDouble(txtDescuento.Text);
            nueva.Total = Convert.ToDouble(txtTotal.Text);
            nueva.FormaPago = Convert.ToInt32(cboFormaPago.SelectedIndex);

            if (nueva.Confirmar())
            {
                MessageBox.Show("La factura se grabó correctamente!!!", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else 
            {
                MessageBox.Show("La factura NO se grabó correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvDetalles_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex==4)
            {
                nueva.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                calcularTotales();
            }
        }
    }
    
}
