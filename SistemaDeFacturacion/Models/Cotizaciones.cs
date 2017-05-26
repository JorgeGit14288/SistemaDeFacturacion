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
    
    public partial class Cotizaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cotizaciones()
        {
            this.DetallesCotizacion = new HashSet<DetallesCotizacion>();
            this.Facturas = new HashSet<Facturas>();
        }
    
        public int idCotizacion { get; set; }
        public string nombre { get; set; }
        public System.DateTime fecha { get; set; }
        public Nullable<decimal> subTotal { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public Nullable<decimal> total { get; set; }
        public string usuario { get; set; }
        public string estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesCotizacion> DetallesCotizacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
    }
}
