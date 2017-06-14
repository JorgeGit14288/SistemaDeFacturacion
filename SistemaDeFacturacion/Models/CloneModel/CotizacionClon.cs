using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models.CloneModel
{
    public class CotizacionClon
    {
        public int idCotizacion { get; set; }
        public string nitCliente { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public System.DateTime fecha { get; set; }
        public decimal subTotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
        public string usuario { get; set; }
        public string estado { get; set; }
    }
}