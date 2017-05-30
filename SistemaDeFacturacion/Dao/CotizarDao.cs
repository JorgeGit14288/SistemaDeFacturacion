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
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {
                        // si el cliente se ingresa con un nit lo crearmos en la bd, si no existe,
                        // en caso de que exista no se crea, y se pasa a las siguientes transacciones
                        if (ctx.Clientes.SingleOrDefault(p => p.nit == c.cliente.nit) == null)
                        {
                            if (String.IsNullOrEmpty(c.cliente.nit) || String.IsNullOrEmpty(c.cliente.nombre))
                            {
                                // no se crea el cliente
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(c.cliente.direccion))
                                {
                                    c.cliente.direccion = "Ciudad";
                                }
                                ctx.Clientes.Add(c.cliente);
                                ctx.SaveChanges();
                            }
                        }
                        Cotizaciones cotizacion = new Cotizaciones();
                        cotizacion.idCotizacion = c.cotizacion.idCotizacion;
                        cotizacion.nombre = c.cotizacion.nombre;
                        cotizacion.nitCliente = c.cotizacion.nitCliente;
                        cotizacion.usuario = c.cotizacion.usuario;
                        cotizacion.fecha = c.cotizacion.fecha;
                        cotizacion.estado = "Cotizacion";
                        cotizacion.direccion = c.cotizacion.direccion;
                        cotizacion.descuento = c.cotizacion.descuento;
                        cotizacion.subTotal = c.cotizacion.subTotal;
                        cotizacion.total = c.cotizacion.total;
                        ctx2.Entry(cotizacion).State = EntityState.Added;
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

        public string ActualizarTotal(decimal total, int idCotizacion)
        {
            try
            {
                Cotizaciones coti = new Cotizaciones();
                coti = ctx.Cotizaciones.Find(idCotizacion);
                coti.total = total;
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "mensaje " + ex.Message;
            }
        }

        public string Cotizar(CotizarModel c)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {
                        // si el cliente se ingresa con un nit lo crearmos en la bd, si no existe,
                        // en caso de que exista no se crea, y se pasa a las siguientes transacciones
                        if (ctx.Clientes.SingleOrDefault(p => p.nit == c.cliente.nit) == null)
                        {
                            if (String.IsNullOrEmpty(c.cliente.nit) || String.IsNullOrEmpty(c.cliente.nombre))
                            {
                                // no se crea el cliente
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(c.cliente.direccion))
                                {
                                    c.cliente.direccion = "Ciudad";
                                }
                                ctx.Clientes.Add(c.cliente);
                                ctx.SaveChanges();
                            }
                        }
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

        public string CrearDetalle(DetallesCotizacion detalle)
        {
            try
            {
                ctx.DetallesCotizacion.Add(detalle);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "mensaje " + ex.Message;
            }

        }

        public string DetallesCotizacion(int idCotizacion)
        {
            throw new NotImplementedException();
        }

        public string EliminarCotizacion(int id)
        {
            try
            {
                Cotizaciones c = new Cotizaciones();
                c= ctx.Cotizaciones.Find(id);
                ctx.Cotizaciones.Remove(c);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "mensaje " + ex.Message;
            }
        }

        public string EliminarDetalle(DetallesCotizacion detalle)
        {
            try
            {
                DetallesCotizacion d = new Models.DetallesCotizacion();
                d = ctx.DetallesCotizacion.SingleOrDefault(r => (r.idCotizacion == detalle.idCotizacion && r.idDetalle == detalle.idDetalle));               
                ctx.DetallesCotizacion.Remove(d);
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "mensaje " + ex.Message;
            }
        }
        public string ModificarDetalle(DetallesCotizacion detalle)
        {
            try
            {
                ctx.Entry(detalle).State = EntityState.Modified;
                ctx.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return "mensaje " + ex.Message;
            }
        }
        public int noCotizacion()
        {
            throw new NotImplementedException();
        }
    }
}