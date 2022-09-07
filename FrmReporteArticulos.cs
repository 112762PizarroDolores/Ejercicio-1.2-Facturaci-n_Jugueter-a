using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturacion_Jugueteria_112762.Formularios
{
    public partial class FrmReporteArticulos : Form
    {
        public FrmReporteArticulos()
        {
            InitializeComponent();
        }

        private void FrmReporteArticulos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dSArticulos.T_ARTICULOS' Puede moverla o quitarla según sea necesario.
            this.t_ARTICULOSTableAdapter.Fill(this.DSArticulos.T_ARTICULOS);

            this.reportViewer1.RefreshReport();
        }
    }
}
