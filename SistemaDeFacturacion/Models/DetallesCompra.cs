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
    
    public partial class DetallesCompra
    {
        public int idCompra { get; set; }
        public int idDetalle { get; set; }
        public string idProducto { get; set; }
        public decimal cantidad { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> precioVenta { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public Nullable<decimal> subTotal { get; set; }
        public string observaciones { get; set; }
    
        public virtual Compras Compras { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
