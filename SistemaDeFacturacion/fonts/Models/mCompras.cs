using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Models
{
    public class mCompras
    {
        public List<DetallesCompra> detalles { get; set; }
        public Compras compra { get; set; }
        public Proveedores proveedor { get; set; }

    }
}