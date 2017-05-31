using SistemaDeFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeFacturacion.Dao
{
    interface IFacturarDao
    {
        string FacturarVenta(Facturar datos);
        string RealizarVenta(int idCotizacion);
        Facturar DetallesVenta(int idFactura);
    }
}
