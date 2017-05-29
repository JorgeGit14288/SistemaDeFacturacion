using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using System.Data.Entity;

namespace SistemaDeFacturacion.Dao
{
    public class FacturasDao : IFacturasDao
    { 
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public bool Actualizar(Facturas a)
        {
            throw new NotImplementedException();
        }

        public Facturas BuscarId()
        {
            throw new NotImplementedException();
        }

        public bool Crear(Facturas f)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdFactura()
        {
            try
            {
                //int n = ctx.Facturas.Max(r=> r.idFactura);
                System.Nullable<Int32> maxIdFactura =
                (from f in ctx.Facturas
                select f.idFactura)
                .Max();

                return  (Convert.ToInt32(maxIdFactura)+1);

            }
            catch
            {
                return 0;
            }
        }

        public List<Facturas> Listar()
        {
            throw new NotImplementedException();
        }

        public List<DetallesCotizacion> ListarDetalles(int id)
        {
            throw new NotImplementedException();
        }

        public List<Facturas> ListarFacturasFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        public List<Facturas> ListarFacturasHoy(DateTime fecha)
        {
            try
            {

                //  List<Facturas> lista = ctx.Facturas.Where(f => DbFunctions.TruncateTime(f.fecha) == DbFunctions.TruncateTime(fecha)).ToList();
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}