using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models
{
    public class CotizarModel
    {
        public Cotizaciones cotizacion { get; set; }
        public List<DetallesCotizacion> Detalles {get;set;}

    }
}