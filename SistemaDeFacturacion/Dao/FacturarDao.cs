using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using System.Transactions;
using System.Messaging;
using System.Data.Entity;

namespace SistemaDeFacturacion.Dao
{
    public class FacturarDao : IFacturarDao
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public string FacturarVenta(Facturar datos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    int id = datos.idCotizacion;
                    Cotizaciones coti = new Cotizaciones();
                    coti = ctx.Cotizaciones.Find(id);
                    if(coti==null)
                    {
                        return "No se ha encontrado la cotizacion";
                    }
                    
                    List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                    detalles = ctx.DetallesCotizacion.Where(s => s.idCotizacion == coti.idCotizacion).ToList();
                    if(detalles==null || detalles.Count==0)
                    {
                        return "No hay productos en la cotizacion";
                    }
                    if (coti.estado == "Facturado" || coti.estado == "facturado")
                    {
                        return "La cotizacion ya ha sido facturada, si desea vuelva a reimprimir la factura";
                    }
                    if(coti.estado=="Vendido")
                    {
                        using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                        {
                            Clientes c = new Clientes();
                            c = datos.cliente;
                            if (c.nit == "c/f" || c.nit == "C/F" || c.nit == "c/F" || c.nit == "C/f")
                            {
                                // no se inserta el cliente en la tabla de la base de datos
                            }
                            else
                            {
                                //buscamos si el cliente existe para insertarlo en la base de datos.
                                c = new Clientes();
                                c = ctx2.Clientes.Find(datos.cliente.nit);
                                //verificamos si existe 
                                if (c == null)
                                {
                                    //lo insertamos en la base de datos
                                    ctx2.Entry(datos.cliente).State = EntityState.Added;
                                    // ctx.Clientes.Add(datos.cliente);
                                    //ctx.SaveChanges();
                                }
                                //de lo contrario no hacemos nada si ya existe
                            }
                            //creamos la factura
                            datos.factura.idCotizacion = id;
                            Cotizaciones cotiz = new Cotizaciones();
                            cotiz = ctx2.Cotizaciones.Find(id);
                            cotiz.estado = "Facturado";
                            datos.factura.total = cotiz.total;
                            datos.factura.fecha = DateTime.Now.Date;
                            ctx2.Entry(datos.factura).State = EntityState.Added;                            
                            ctx2.SaveChanges();
                            scope.Complete();
                            return "ok";
                        }
                    }
                    else
                    {

                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {
                        Clientes c = new Clientes();
                        c = datos.cliente;
                        if (c.nit == "c/f" || c.nit =="C/F" || c.nit == "c/F" || c.nit == "C/f")
                        {
                            // no se inserta el cliente en la tabla de la base de datos
                        }
                        else
                        {
                            //buscamos si el cliente existe para insertarlo en la base de datos.
                            c = new Clientes();
                            c = ctx2.Clientes.Find(datos.cliente.nit);
                            //verificamos si existe 
                            if(c==null)
                            {
                                //lo insertamos en la base de datos
                               ctx2.Entry(datos.cliente).State = EntityState.Added;
                               // ctx.Clientes.Add(datos.cliente);
                                //ctx.SaveChanges();
                            }
                            //de lo contrario no hacemos nada si ya existe
                        }
                        //creamos la factura
                        ctx2.Entry(datos.factura).State = EntityState.Added;

                       foreach(var e in detalles)
                        {
                            /**
                            // Creamos un detalle sin el objeto producto para que no de error
                            DetallesCotizacion d = new DetallesCotizacion()
                            {
                                idDetalle = e.idDetalle,
                              //  idFactura= e.idFactura,
                                idProducto= e.idProducto,
                                cantidad= e.cantidad,
                                precio = e.precio,
                                subTotal= e.subTotal
                            };
                             ctx2.Entry(d).State = EntityState.Added;
                            */
                            // Descontamos de la entidad producto cada venta.. para que el inventario baje
                            Productos producto = new Productos();
                            producto = ctx2.Productos.Find(e.idProducto);
                            decimal existencia = Convert.ToDecimal(producto.existencia);
                            decimal cantidadActual = existencia - e.cantidad;
                            producto.existencia = cantidadActual;
                            //ctx2.Entry(producto).State = EntityState.Modified;
                        }

                            datos.factura.idCotizacion = id;
                            Cotizaciones cotiz = new Cotizaciones();
                            cotiz = ctx2.Cotizaciones.Find(id);
                            cotiz.estado = "Facturado";
                            datos.factura.total = cotiz.total;
                            datos.factura.fecha = DateTime.Now.Date;
                            ctx2.Entry(datos.factura).State = EntityState.Added;
                            ctx2.SaveChanges();
                            scope.Complete();
                            return "ok";
                        }
                    }

                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return "Mensaje de error " + ex.ToString();
                }
               
            }
        }
        public string RealizarVenta(int idCotizacion)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Cotizaciones c = new Cotizaciones();
                    c = ctx.Cotizaciones.Find(idCotizacion);
                    if(c.estado=="Vendido"||c.estado=="Facturado")
                    {
                        return "la transaccion ya fue realizada, el estado es " + c.estado;
                    }                      
                    List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                    detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == idCotizacion).ToList();

                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {                      
                        foreach (var e in detalles)
                        {
                            // Descontamos de la entidad producto cada venta.. para que el inventario baje
                            Productos producto = new Productos();
                            producto = ctx2.Productos.Find(e.idProducto);
                            decimal existencia = Convert.ToDecimal(producto.existencia);
                            decimal cantidadActual = existencia - e.cantidad;
                            producto.existencia = cantidadActual;
                            //ctx2.Entry(producto).State = EntityState.Modified;
                            Cotizaciones coti = new Cotizaciones();
                            coti = ctx2.Cotizaciones.Find(idCotizacion);
                            coti.estado = "Vendido";
                        }
                        ctx2.SaveChanges();
                        scope.Complete();
                        return "ok";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return "mensaje " + ex.ToString();
                }
            }
        }
        public Facturar DetallesVenta(int idFactura)
        {
            throw new NotImplementedException();
        }
    }
}