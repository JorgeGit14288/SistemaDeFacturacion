using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using System.Data.Entity;

namespace SistemaDeFacturacion.Dao.Helpers
{
    public class HomeHelpers
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public int CotizacionesHoy(DateTime fecha)
        {
            try
            {
                //agregamos dbFuncions y el using using System.Data.Entity; par apoder utiilzar el TRUNCATETIME, que 
                // solo envia la fecha a buscar..
                int cotizado = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha)) ).Count();
                return cotizado;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public int CotizacionesMes()
        {
            try
            {
                int dia = 1;
                int year = DateTime.Now.Year;
                int mes = DateTime.Now.Month;

                DateTime fechaInicio = DateTime.Parse(dia + "/" + mes + "/"+year);
                DateTime fechaFin = DateTime.Now;

                int cotizado = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) >= DbFunctions.TruncateTime(fechaInicio) && DbFunctions.TruncateTime(c.fecha) <= DbFunctions.TruncateTime(fechaFin))).Count();

                return cotizado;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int VentasMes()
        {
            try
            {
                int dia = 1;
                int year = DateTime.Now.Year;
                int mes = DateTime.Now.Month;

                DateTime fechaInicio = DateTime.Parse(dia + "/" + mes + "/" + year);
                DateTime fechaFin = DateTime.Now;

                int Vendido = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) >= DbFunctions.TruncateTime(fechaInicio) && 
                DbFunctions.TruncateTime(c.fecha) <= DbFunctions.TruncateTime(fechaFin)
                && c.estado=="Vendido")).Count();

                int Facturado = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) >= DbFunctions.TruncateTime(fechaInicio) &&
                DbFunctions.TruncateTime(c.fecha) <= DbFunctions.TruncateTime(fechaFin)
                && c.estado == "Facturado")).Count();

               int total = Vendido + Facturado;
                return total;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int ComprasMes()
        {
            try
            {
                int dia = 1;
                int year = DateTime.Now.Year;
                int mes = DateTime.Now.Month;

                DateTime fechaInicio = DateTime.Parse(dia + "/" + mes + "/" + year);
                DateTime fechaFin = DateTime.Now;

                int compras = ctx.Compras.Where(c => (DbFunctions.TruncateTime(c.fecha) >= DbFunctions.TruncateTime(fechaInicio) && DbFunctions.TruncateTime(c.fecha) <= DbFunctions.TruncateTime(fechaFin))).Count();

                return compras;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int ComprassHoy(DateTime fecha)
        {
            try
            {
                // int comprado = Convert.ToInt32(ctx.sp_ComprasDia(fecha));
                int compras = ctx.Compras.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha))).Count();
                return compras;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int FacturasHoy(DateTime fecha)
        {
            try
            {
                //int vendido = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha) && c.estado == "Vendido")).Count();
                int facturado = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha) && c.estado == "Facturado")).Count();           
                return facturado;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public int VentasHoy(DateTime fecha)
        {
            try
            {
                int vendido = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha) && c.estado=="Vendido")).Count();
                int facturado = ctx.Cotizaciones.Where(c => (DbFunctions.TruncateTime(c.fecha) == DbFunctions.TruncateTime(fecha) && c.estado == "Facturado")).Count();
                int total = vendido + facturado;
                return total;
            }
             catch (Exception ex)
            {
                return 0;
            }
        }
        public int ProductosHoy()
        {
            try
            {
                
                return ctx.Productos.Where(p=> p.existencia>0).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int ProductosSinExistencia()
        {
            try
            {

                return ctx.Productos.Where(p => p.existencia == 0).Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}