using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion_Jugueteria_112762.Entidades////LEGAJO 112762 PIZARRO CABALLERO, DOLORES
{
    class DetalleFactura
    {
       
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

        public DetalleFactura(Articulo articulo, int cantidad)
        {
            Articulo = articulo;
            Cantidad = cantidad;
        }
        //calcula subtotal por item-por tipo de articulo-no acumula distintos tipos de articulso ojo
        public double calcularSubTotal()
        {
            return Articulo.Precio * Cantidad;
        }

    }
}
