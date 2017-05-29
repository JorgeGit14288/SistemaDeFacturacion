using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using System.Transactions;
using System.Data.Entity;

namespace SistemaDeFacturacion.Dao
{
    public class CotizarDao : ICotizarDao
    {
       private FacturacionDbEntities ctx = new FacturacionDbEntities();
        public string ActualizarCotizacion(CotizarModel c)
        {
            throw new NotImplementedException();
           
        }

        public string Cotizar(CotizarModel c)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {
                        c.cotizacion.estado = "Cotizacion";
                        ctx2.Entry(c.cotizacion).State = EntityState.Added;
                        foreach (var e in c.Detalles)
                        {
                            // Creamos un detalle sin el objeto producto para que no de error
                            DetallesCotizacion d = new DetallesCotizacion()
                            {
                                idCotizacion = e.idCotizacion,
                                idDetalle = e.idDetalle,                               
                                idProducto = e.idProducto,
                                cantidad = e.cantidad,
                                precio = e.precio,
                                descuento = e.descuento,
                                subTotal = e.subTotal
                            };
                            ctx2.Entry(d).State = EntityState.Added;

                        }
                        ctx2.SaveChanges();
                        scope.Complete();
                        return "ok";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return "Error, " + ex.ToString();
                }

            }
        }

        public string DetallesCotizacion(int idCotizacion)
        {
            throw new NotImplementedException();
        }

        public string EliminarCotizacion(int id)
        {
            throw new NotImplementedException();
        }

        public int noCotizacion()
        {
            throw new NotImplementedException();
        }
    }
}