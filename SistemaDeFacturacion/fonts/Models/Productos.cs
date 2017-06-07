//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaDeFacturacion.fonts.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            this.DetallesCompra = new HashSet<DetallesCompra>();
            this.DetallesCotizacion = new HashSet<DetallesCotizacion>();
        }
    
        public string idProducto { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precioCompra { get; set; }
        public decimal precio { get; set; }
        public Nullable<decimal> descuentoVenta { get; set; }
        public Nullable<decimal> existencia { get; set; }
        public string observacion { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public byte[] imagen { get; set; }
        public Nullable<System.DateTime> creado { get; set; }
        public Nullable<System.DateTime> modificado { get; set; }
    
        public virtual Categorias Categorias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesCompra> DetallesCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesCotizacion> DetallesCotizacion { get; set; }
    }
}
