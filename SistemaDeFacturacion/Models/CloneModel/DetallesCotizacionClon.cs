using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models.CloneModel
{
    public class DetallesCotizacionClon
    {
        public int idDetalle { get; set; }
        public int idCotizacion { get; set; }
        public string idProducto { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
        public decimal subTotal { get; set; }
        public string descripcion { get; set; }
    }
}