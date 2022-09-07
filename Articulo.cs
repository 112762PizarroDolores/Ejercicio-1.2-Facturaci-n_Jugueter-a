using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion_Jugueteria_112762.Entidades////LEGAJO 112762 PIZARRO CABALLERO, DOLORES
{
    class Articulo
    {
        public  int ArticuloNro { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Articulo(int articuloNro, string nombre, double precio)
        {
            ArticuloNro = articuloNro;
            Nombre = nombre;
            Precio = precio;
        }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
