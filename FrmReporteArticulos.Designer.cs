using Facturacion_Jugueteria_112762.Reportes;

namespace Facturacion_Jugueteria_112762.Formularios
{
    partial class FrmReporteArticulos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DSArticulos = new Facturacion_Jugueteria_112762.Reportes.DSArticulos();
            this.tARTICULOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.t_ARTICULOSTableAdapter = new Facturacion_Jugueteria_112762.Reportes.DSArticulosTableAdapters.T_ARTICULOSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DSArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tARTICULOSBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tARTICULOSBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Facturacion_Jugueteria_112762.Reportes.RptArticulos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(776, 426);
            this.reportViewer1.TabIndex = 0;
            // 
            // dSArticulos
            // 
            this.DSArticulos.DataSetName = "DSArticulos";
            this.DSArticulos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tARTICULOSBindingSource
            // 
            this.tARTICULOSBindingSource.DataMember = "T_ARTICULOS";
            this.tARTICULOSBindingSource.DataSource = this.DSArticulos;
            // 
            // t_ARTICULOSTableAdapter
            // 
            this.t_ARTICULOSTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporteArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmReporteArticulos";
            this.Text = "FrmReporteArticulos";
            this.Load += new System.EventHandler(this.FrmReporteArticulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DSArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tARTICULOSBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Reportes.DSArticulos dSArticulos;
        private System.Windows.Forms.BindingSource tARTICULOSBindingSource;
        private Reportes.DSArticulosTableAdapters.T_ARTICULOSTableAdapter t_ARTICULOSTableAdapter;

        public DSArticulos DSArticulos { get => dSArticulos; set => dSArticulos = value; }
    }
}