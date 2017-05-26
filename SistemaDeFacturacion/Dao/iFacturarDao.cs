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
        bool CrearVenta(Facturar datos);
        Facturar DetallesVenta(int idFactura);
    }
}
