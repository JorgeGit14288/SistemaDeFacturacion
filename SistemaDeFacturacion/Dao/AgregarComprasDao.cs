using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using System.Transactions;
using System.Data.Entity;
using SistemaDeFacturacion.Dao;

namespace SSistemaDeFacturacion.Dao
{
   
    public class AgregarComprasDao : IAgregarComprasDao
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        public mCompras BuscarCompra(int idCompra)
        {
            throw new NotImplementedException();
        }

        public bool Crear(mCompras mc)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (FacturacionDbEntities ctx2 = new FacturacionDbEntities())
                    {
                        // verificamos si existe el proveedor, si no lo agregamos
                        Proveedores p = new Proveedores();
                        p = ctx.Proveedores.Find(mc.proveedor.idProveedor);
                        if (p == null)
                        {
                            // si retorna null, es porque el proveedor no existe en la bd, asi que se crea
                            ctx2.Entry(mc.proveedor).State = EntityState.Added;
                        }
                        else
                        {
                            // si retorna un valor, el proveedor existe entonces  no agregamos nada

                        }
                        //Creamos la compra
                        ctx2.Entry(mc.compra).State = EntityState.Added;

                        //Iniciamos el proceso para agregar al stock el detalle de los productos
                        foreach (var d in mc.detalles)
                        {
                            // Creamos un objeto detalles de compa pero sin la entidad productos enlazada 
                            DetallesCompra temp = new DetallesCompra();
                            temp.idCompra = d.idCompra;
                            temp.idDetalle = d.idDetalle;
                            temp.idProducto = d.idProducto;
                            temp.cantidad = d.cantidad;
                            temp.precio = d.precio;
                            temp.precioVenta = d.precioVenta;
                            temp.subTotal = d.subTotal;
                            temp.observaciones = d.observaciones;

                            //agregamos el objeto a la transaccion
                            ctx2.Entry(temp).State = EntityState.Added;
                            //agregamos la cantidad de compra actual, al stock y le metemos el precio de compra
                            Productos producto = new Productos();

                            producto = ctx2.Productos.Find(temp.idProducto);
                            decimal existencia = Convert.ToDecimal(producto.existencia);
                            decimal cantTotal = existencia + Convert.ToDecimal(temp.cantidad);
                            producto.existencia = cantTotal;
                            producto.precioCompra = temp.precio;
                            producto.precio = Convert.ToDecimal(temp.precioVenta);
                            producto.modificado = DateTime.Now;
                            // se supone que no ponemos la transaccion Entry, a productos porque 
                            //al hacer save changes, todo se guarda
                        }
                        ctx2.SaveChanges();
                        scope.Complete();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}