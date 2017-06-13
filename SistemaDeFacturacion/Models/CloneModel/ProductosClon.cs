using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models.CloneModel
{
    public class ProductosClon
    {
        public string idProducto { get; set; }
        public string nombre { get; set; }
        public decimal precioCompra { get; set; }
        public decimal precio { get; set; }
        public decimal descuentoVenta { get; set; }
        public decimal existencia { get; set; }
        public string observacion { get; set; }
        public int idCategoria { get; set; }
        public byte[] imagen { get; set; }
        public DateTime creado { get; set; }
        public DateTime modificado { get; set; }
    }
}