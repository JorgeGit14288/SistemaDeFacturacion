using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models
{
    public class Facturar
    {
        public Facturas factura { get; set; }
        public int idCotizacion {get;set;}
        public Clientes cliente { get; set; }

    }
}