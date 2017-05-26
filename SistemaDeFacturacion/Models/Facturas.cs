//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaDeFacturacion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Facturas
    {
        public int idFactura { get; set; }
        public string nitCliente { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string fecha { get; set; }
        public Nullable<decimal> subTotal { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> idCotizacion { get; set; }
        public byte[] usuario { get; set; }
        public Nullable<int> tipoPago { get; set; }
        public string idPago { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Cotizaciones Cotizaciones { get; set; }
        public virtual TipoPago TipoPago1 { get; set; }
    }
}
