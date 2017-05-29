using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    public class CotizacionesDao : ICotizacionesDao
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        public int getIdCotizacion()
        {
            try
            {
                //int n = ctx.Facturas.Max(r=> r.idFactura);
                System.Nullable<Int32> maxIdFactura =
                (from c in ctx.Cotizaciones
                 select c.idCotizacion)
                .Max();
                return (Convert.ToInt32(maxIdFactura) + 1);

            }
            catch
            {
                return 0;
            }
        }
    }
}