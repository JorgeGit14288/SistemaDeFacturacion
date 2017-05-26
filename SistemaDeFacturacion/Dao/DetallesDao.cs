using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    public class DetallesCotizacionDao : IDetallesCotizacionDao
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public bool Actualizar(DetallesCotizacion d)
        {
            throw new NotImplementedException();
        }

        public DetallesCotizacion Buscar(int idFactura, int idDetalle)
        {
            throw new NotImplementedException();
        }

        public bool Crear(DetallesCotizacion d)
        {
            try
            {
                ctx.DetallesCotizacion.Add(d);
                ctx.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<DetallesCotizacion> DetallesCotizacion(int idCotizacion)
        {
            List<DetallesCotizacion> lista = new List<DetallesCotizacion>();
            try
            {
                var query = (from d in ctx.DetallesCotizacion where d.idCotizacion == idCotizacion select d);
                return lista = query.ToList();
                
            }
            catch
            {
                return lista;

            }

          
        }

        public List<DetallesCotizacion> DetallesFactura(int idCotizacion)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int idCotizacion, int idDetalle)
        {
            throw new NotImplementedException();
        }

        public bool ExisteDetalle(int idCotizacion, int idDetalle)
        {
            throw new NotImplementedException();
        }
    }
}