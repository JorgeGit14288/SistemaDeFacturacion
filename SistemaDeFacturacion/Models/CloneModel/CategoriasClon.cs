using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeFacturacion.Models.CloneModel
{
    public class CategoriasClon
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }

    }
}