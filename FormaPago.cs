using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion_Jugueteria_112762.Entidades////LEGAJO 112762 PIZARRO CABALLERO, DOLORES
{
    class FormaPago
    {
        public int Id_forma_pago { get; set; }
        public string Nombre { get; set; }
        public FormaPago(int id_forma_pago, string nombre)
        {
            this.Id_forma_pago = id_forma_pago;
            this.Nombre = nombre;
        }
    }
}
