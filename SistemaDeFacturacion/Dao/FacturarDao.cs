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
        public bool CrearVenta(Facturar datos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
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

                       foreach(var e in datos.detalles)
                        {
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
                            // Descontamos de la entidad producto cada venta.. para que el inventario baje
                            Productos producto = new Productos();
                            producto = ctx2.Productos.Find(d.idProducto);
                            decimal existencia = Convert.ToDecimal(producto.existencia);
                            decimal cantidadActual = existencia - d.cantidad;
                            producto.existencia = cantidadActual;
                            //ctx2.Entry(producto).State = EntityState.Modified;
                        }
                        ctx2.SaveChanges();
                        scope.Complete();
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
               
            }
        }
        public Facturar DetallesVenta(int idFactura)
        {
            throw new NotImplementedException();
        }
    }
}