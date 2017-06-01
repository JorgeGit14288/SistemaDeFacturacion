using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using SistemaDeFacturacion.Dao;

namespace SistemaDeFacturacion.Controllers
{
    public class CotizarController : Controller
    {
        // objetos de acceso a datoa
        IFacturarDao daoFacturar = new FacturarDao();
        IFacturasDao daoFacturas = new FacturasDao();
        ICotizacionesDao daoCoti = new CotizacionesDao();
        IClientesDao daoClientes = new ClientesDao();
        IProductosDao daoProductos = new ProductosDao();
        ICotizarDao daoCotizar = new CotizarDao();
        FacturacionDbEntities ctx = new FacturacionDbEntities();

        // GET: Facturar
        public ActionResult Cotizar()
        {
            //ViewBag.Mensaje = "Todos los campos son requeridos";
            //obtenermos el id de factura 
            int idCotizacion = daoCoti.getIdCotizacion();
            if (idCotizacion == 0)
            {
                idCotizacion = 1;
            }
            Session["idCotizacion"] = idCotizacion;

            //ViewBag.idProducto = new SelectList(daoProductos.Listar(), "nombre", "precio","existencia");
            // le devolvemos la lista de clientes
            ViewBag.Clientes = daoClientes.Listar();
            // para devolver la lista de productos
            ViewBag.Productos = daoProductos.Listar();
            return View(new Facturar());
        }
        [HttpPost]
        public ActionResult Cotizar(FormCollection form, CotizarModel model)
        {
            try
            {
                // para devolver la lista de productos
                ViewBag.Clientes = daoClientes.Listar();
                ViewBag.Productos = daoProductos.Listar();
                //obtenemos los objetos de sesiones
                List<DetallesCotizacion> DetallesCotizacion = new List<DetallesCotizacion>();
                Clientes c = new Clientes();
                if((Session["ClienteC"] == null) || (Session["ClienteC"].ToString()=="") )
                {
                    // si no se ha ingesado ningun cliente retorna la misma vista sugiriendo que no pudede cotizar.
                    ViewBag.Error = "Debe ingresar al menos el nombre de el cliente y la direccion para crear la cotizacion";         
                    return View();
                }
                else
                {
                    // si el cliente existe se parsea
                    c = (Clientes)Session["ClienteC"];
                }
                if (String.IsNullOrEmpty(c.nombre))
                {
                    //si al menos no esta el nombre del clietne retorna error.
                    ViewBag.Error = "Debe de ingresar al menos el nombre de la persona que cotiza";
                    // para devolver la lista de productos         
                    return View();
                }
                if ((Session["DetallesC"].ToString() == "") || (Session["DetallesC"] == null))
                {
                    //si no existe producto no podra crearse la cotizacion
                    ViewBag.Error = "Debe de ingresar al menos un producto a la cotizacion";                  
                    return View();
                }
               
                else
                {
                    //si hay al menos un producto se creara la cotizacion;
                    DetallesCotizacion = (List<DetallesCotizacion>)Session["DetallesC"];
                }
                if (DetallesCotizacion==null)
                {
                    ViewBag.Error = "Debe de ingresar al menos un producto para crear una cotizacion";
                    return View();
                }
                
                decimal totalC = Convert.ToDecimal(Session["totalC"]);
                Cotizaciones coti = new Cotizaciones();
                if (Session["Cotizacion"] == null || Session["Cotizacion"].ToString() == "")
                {
                    ViewBag.Error = "No se ha podido cargar los datos de la cotizacion, contacte con el tecnico";
                    return View();

                }
                else
                {
                    /// se cargan los datos de la cotizacion
                    coti = (Cotizaciones)Session["Cotizacion"];
                }
                coti.nitCliente = c.nit;
                coti.estado = "Cotizado";
                coti.nombre = c.nombre;
                coti.direccion = c.direccion;
                coti.fecha = DateTime.Now;
                coti.total = totalC;
                coti.usuario = Session["Usuario"].ToString();
                int tempIdCoti = daoCoti.getIdCotizacion();
                if(tempIdCoti==0)
                {
                    tempIdCoti = 1;
                }
                coti.idCotizacion = tempIdCoti;
                foreach (var d in DetallesCotizacion)
                {
                    d.idCotizacion = tempIdCoti;
                }
                CotizarModel datosCoti = new CotizarModel()
                {
                    cotizacion = coti,
                    Detalles = DetallesCotizacion,
                    cliente = c
                };
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                string resultado = daoCotizar.Cotizar(datosCoti);
                if (resultado =="ok")
                {
                    ViewBag.Mensaje = "Se ha creado la cotizacion Numero: " + datosCoti.cotizacion.idCotizacion;
                    Session["Cotizacion"] = "";
                    Session["ClienteC"] = "";
                    int idCotizacion = daoCoti.getIdCotizacion();
                    Session["idCotizacion"] = idCotizacion;
                    Session["DetallesC"] = "";
                    Session["idDetalleC"] = "0";
                    Session["totalC"] = "0";
                    return RedirectToAction("RealizarVenta","Facturar",new { id =coti.idCotizacion});
                }
                else
                {
                    ViewBag.Error = "Error, No se realizo la transaccion" + resultado;
                    return View("Cotizar");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = "ha ocurrido un error "+ ex.Message;
                // para devolver la lista de productos
                ViewBag.Clientes = daoClientes.Listar();
                ViewBag.Productos = daoProductos.Listar();
                return View();
            }
        }
        [HttpPost]
        public ActionResult BorrarCotizacion(FormCollection form)
        {
            try
            {
                Session["Cotizacion"] = "";
                Session["ClienteC"] = "";
                int idCotizacion = daoCoti.getIdCotizacion();
                Session["idCotizacion"] = idCotizacion;
                Session["DetallesC"] = "";
                Session["idDetalleC"] = "0";
                Session["totalC"] = "0";
                

                // para devolver la lista de productos
                ViewBag.Clientes = daoClientes.Listar();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Mensaje = "Ahora puede crear una nueva Cotizacion";
                return View("Cotizar");
            }
            catch
            {
                return RedirectToAction("Cotizar");
            }
        }
        [HttpPost]
        public ActionResult BuscarCliente(FormCollection form)
        {
            try
            {
                //Session["Cliente"] = "";
                string nit = form["nitCliente"];
                Clientes cliente = new Clientes();
                cliente = daoClientes.BuscarNit(nit);

                if (cliente == null)
                {
                    Session["ClienteC"] = "";
                    cliente = new Clientes()
                    {
                        nit = nit
                    };
                    Session["ClienteC"] = cliente;
                    // para devolver la lista de productos
                    ViewBag.Clientes = daoClientes.Listar();
                    ViewBag.Productos = daoProductos.Listar();
                    ViewBag.ErrorCliente = "No existe un cliente registrado con este nit";
                    return View("Cotizar");
                }
                else
                {
                    ViewBag.ErrorCliente = "Se ha encontrado el cliente";
                    // para devolver la lista de productos
                    ViewBag.Clientes = daoClientes.Listar();
                    ViewBag.Productos = daoProductos.Listar();
                    Session["ClienteC"] = cliente;

                    Cotizaciones f = new Cotizaciones()
                    {
                        nitCliente = cliente.nit,
                        nombre = cliente.nombre,
                        direccion = cliente.direccion,

                        fecha = DateTime.Now.Date
                    };
                    f.idCotizacion = Convert.ToInt32(Session["idCotizacion"]);
                    if (f.idCotizacion == 0)
                    {
                        f.idCotizacion= daoFacturas.GetIdFactura();
                        f.fecha = DateTime.Now.Date;
                        Session["idCotizacion"] = f.idCotizacion;
                    }
                    f.usuario = Session["Usuario"].ToString();
                    Session["Cotizacion"] = f;
                    return View("Cotizar");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Mensaje de erro: " + ex.Message ;
                // para devolver la lista de productos
                ViewBag.Clientes = daoClientes.Listar();
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }
        }
        [HttpPost]
        public ActionResult CargarCliente(FormCollection form)
        {
            try
            {
                Clientes c = new Clientes()
                {
                    nit = form["nitCliente"],
                    nombre = form["nombre"],
                    direccion = form["direccion"]
                };
                if (c.nit == "c/f" || c.nit == "C/F"|| c.nit==null)
                {
                    //significa que el cliente no se debe de crear
                }
                else
                {
                    if (daoClientes.BuscarNit(c.nit) == null)
                    {
                        // no cambia el valor del cliente porque este no existe

                    }
                    else
                    {
                        c = daoClientes.BuscarNit(c.nit);

                    }
                }
                Session["ClienteC"] = "";
                Session["ClienteC"] = c;
                Cotizaciones f = new Cotizaciones()
                {
                    nitCliente = c.nit,
                    nombre = c.nombre,
                    direccion = c.direccion,

                    fecha = DateTime.Now.Date
                };
                f.idCotizacion = Convert.ToInt32(Session["idCotizacion"]);
                if (f.idCotizacion == 0)
                {
                    f.idCotizacion = daoFacturas.GetIdFactura();
                    f.fecha = DateTime.Now.Date;
                    Session["idCotizacion"] = f.idCotizacion;
                }
                f.usuario = Session["Usuario"].ToString();
                Session["Cotizacion"] = f;
                ViewBag.Mensaje = "Se cargo el cliente a la cotizacion";
               
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Mensaje de error: " + ex.Message ;
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }
        }
        [HttpPost]
        public ActionResult CargarProducto(FormCollection form)
        {
            try
            {
                // buscamos el producto
                string idProducto = form["idProducto"];
                decimal cantidad = Convert.ToDecimal(form["cantidad"]);
                decimal descuento = Convert.ToDecimal(form["descuento"]);
                Productos p = new Productos();
                p = daoProductos.BuscarId(idProducto);
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();

                if (String.IsNullOrEmpty(p.idProducto))
                {
                    ViewBag.ErrorProducto = "No existe  el producto o la existencia es menor a la cantidad solicitada";
                    ViewBag.Productos = daoProductos.Listar();
                    return View("Cotizar");
                }
                else
                {
                    // evaluamos si la existencia es menor a la cantidad solicitada
                    if (p.existencia < cantidad)
                    {
                        ViewBag.ErrorProducto = "Error, La cantidad solicitada es menor a la existencia,";
                        ViewBag.Productos = daoProductos.Listar();
                        return View("Cotizar");

                    }
                    else
                    {
                        // verificamos si existe algun detalle
                        List<DetallesCotizacion> DetallesCotizacion = new List<DetallesCotizacion>();

                        if (Session["DetallesC"].ToString() == "")
                        {

                            // si no hay DetallesCotizacion no se hace nada, puesto que se creo el objeto arriba
                        }
                        else
                        {
                            // si existe detalle, lo pasamos a la variable local DetallesCotizacion
                            DetallesCotizacion = (List<DetallesCotizacion>)Session["DetallesC"];
                        }
                        // creamos un objeto temporal para saber si existe el detalle
                        DetallesCotizacion temp = new DetallesCotizacion();
                        temp = DetallesCotizacion.Find(r => r.idProducto == p.idProducto);
                        if (temp == null)
                        {
                            // creamos un nuevo detalle, obtenemos el id de la factura
                            int idCotizacion = Int32.Parse(Session["idCotizacion"].ToString());
                            // iniciamos el contador para llenar la lista de DetallesCotizacion
                            int idDetalle = Int32.Parse(Session["idDetalleC"].ToString()) + 1;

                            DetallesCotizacion d = new DetallesCotizacion()
                            {
                                idCotizacion = idCotizacion,
                                idDetalle = idDetalle,
                                idProducto = p.idProducto,
                                precio = p.precio,
                                cantidad = cantidad,
                                Productos = p
                            };
                            if (descuento > 0)
                            {
                                decimal desc = (descuento / 100);
                                decimal descontado = d.precio * desc;
                                decimal prec = d.precio - descontado;
                                d.subTotal = prec * d.cantidad;

                            }
                            else
                            {
                                d.subTotal = d.cantidad * d.precio;
                            }
                            //d.Productos.nombre = p.nombre;
                            d.descuento = descuento;
                            //  d.descuento = desc;
                            DetallesCotizacion.Add(d);


                            decimal totalFactura = Convert.ToDecimal(Session["totalC"]);
                            totalFactura = totalFactura + Convert.ToDecimal(d.subTotal);
                            Session["totalC"] = totalFactura;
                            //volvemos a parsear el al objeto sessio
                            Session["idDetalleC"] = idDetalle;
                            Session["DetallesC"] = DetallesCotizacion;
                            // para devolver la lista de productos
                        }
                        else
                        {
                            // el producto ya esta registrado en el detalle asi que solo actualizamos los datos

                            // el producto ya existe en el detalle, entonces lo aumentamos
                            DetallesCotizacion.Remove(temp);
                            temp.cantidad = temp.cantidad + cantidad;
                            temp.subTotal = temp.cantidad * temp.precio;
                            DetallesCotizacion.Add(temp);
                            //recalculamos el total otra vez
                            decimal totalC = 0;
                            foreach (var i in DetallesCotizacion)
                            {
                                totalC = totalC + Convert.ToDecimal(i.subTotal);
                            }
                            Session["totalC"] = totalC;
                            Session["DetallesC"] = DetallesCotizacion;
                            ViewBag.ErrorProducto = "se Modifico " + cantidad + " " + p.nombre + " precio Q" + p.precio + "sub Total  Q" + temp.subTotal;
                        }
                        ViewBag.Productos = daoProductos.Listar();
                        return View("Cotizar");
                    }
                }
            }
            catch
            {
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Error = "No hay conexion con la base de datos";
                ViewBag.ErrorProducto = "No se pudo agregar el producto";
                return View("Cotizar");
            }
        }
        public ActionResult EliminarDetalle(FormCollection form, int id)
        {
            try
            {

                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                //Obtenemos el objeto de session de DetallesCotizacion
                List<DetallesCotizacion> DetallesCotizacion = new List<DetallesCotizacion>();
                DetallesCotizacion = (List<DetallesCotizacion>)Session["DetallesC"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCotizacion d = new DetallesCotizacion();
                //busca el detalle en la lista, segun el id
                d = DetallesCotizacion.Find(r => r.idDetalle == id);
                // elimina el detalle de la lista
                DetallesCotizacion.Remove(d);
                // en esta parate reordenamos los objetos, para que no queden ids de detalle saltados
                //creamos una nueva lista de DetallesCotizacion a quien pasaremos los objetos restantes despues de haber eliminado el detalle

                List<DetallesCotizacion> nuevaLista = new List<DetallesCotizacion>();
                //con un bucle recorremos la lista actual y pasamos a la lista nueva, modificando los ids, de detalle
                int idDetalle = 1;
                decimal totalC = 0;
                foreach (var e in DetallesCotizacion)
                {
                    //Creamos un nuevo de talle para pasarle los objetos
                    DetallesCotizacion dn = new DetallesCotizacion();
                    dn = e;
                    dn.idDetalle = idDetalle;
                    idDetalle++;
                    totalC = totalC + Convert.ToDecimal(dn.subTotal);
                    nuevaLista.Add(dn);

                    //con este metodo terminamos de pasar la lista antigua a la lista nueva;
                }
                // instanceamos las variables de session con los nuevos datos
                Session["DetallesC"] = nuevaLista;
                idDetalle = nuevaLista.Count();
                Session["idDetalleC"] = idDetalle;
                Session["totalC"] = totalC;
                //devolvemos los datos


                ViewBag.ErrorProducto = "Se elimino =" + d.Productos.nombre;
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }
            catch
            {
                ViewBag.Error = "No se pudo eliminar el producto del detalle";
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");

            }

        }
        public ActionResult EditarDetalle(FormCollection form, int id)
        {
            try
            {
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                //Obtenemos el objeto de session de DetallesCotizacion
                List<DetallesCotizacion> DetallesCotizacion = new List<DetallesCotizacion>();
                DetallesCotizacion = (List<DetallesCotizacion>)Session["DetallesC"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCotizacion d = new DetallesCotizacion();
                //busca el detalle en la lista, segun el id
                d = DetallesCotizacion.Find(r => r.idDetalle == id);
                //obteniendo el producto devolvemos la cantidad y el ID para poder ser Modificados
                ViewBag.modIdProducto = d.idProducto;
                ViewBag.modCantidad = d.cantidad;
                ViewBag.modIdDetalle = d.idDetalle;
                ViewBag.modDescuento = d.descuento;
                ViewBag.ErrorProducto = "Se elimino =" + d.Productos.nombre;
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }
            catch
            {
                ViewBag.Error = "No se logro editar el producto =" + id;
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }

        }
        [HttpPost]
        public ActionResult EditarDetalle(FormCollection form)
        {
            try
            {

                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();

                int idProducto = Convert.ToInt32(form["idProducto"]);
                decimal cantidad = Convert.ToDecimal(form["cantidad"]);
                decimal descuento = Convert.ToDecimal(form["descuento"]);
                int id = Convert.ToInt32(form["idDetalle"]);

                //Obtenemos el objeto de session de DetallesCotizacion
                List<DetallesCotizacion> DetallesCotizacion = new List<DetallesCotizacion>();
                DetallesCotizacion = (List<DetallesCotizacion>)Session["DetallesC"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCotizacion d = new DetallesCotizacion();
                //busca el detalle en la lista, segun el id
                d = DetallesCotizacion.Find(r => r.idDetalle == id);

                if (d.cantidad == cantidad && d.descuento ==descuento)
                {
                    //las cantidad ingresada es la misma, entonces no hacemos algo solo retornamos la vista
                    ViewBag.ErrorProducto = "La cantidad ingresada es la misa, no se modifico el producto";
                    // para devolver la lista de productos
                    ViewBag.Productos = daoProductos.Listar();
                    return View("Cotizar");

                }
                else
                {
                    //eliminamos el objeto de la lista
                    DetallesCotizacion.Remove(d);
                    //obteniendo el producto devolvemos la cantidad y el ID para poder ser Modificados
                    d.cantidad = cantidad;
                    if (descuento > 0)
                    {
                        decimal desc = (descuento / 100);
                        decimal descontado = d.precio * desc;
                        decimal prec = d.precio - descontado;
                        d.subTotal = prec * d.cantidad;

                    }
                    else
                    {
                        d.subTotal = d.cantidad * d.precio;
                    }
                    //d.Productos.nombre = p.nombre;
                    d.descuento = descuento;
                  
                    //  d.descuento = desc;
                    DetallesCotizacion.Add(d);
                    //calculamos el nuevo total de la factura
                    decimal totalC = 0;
                    foreach (var e in DetallesCotizacion)
                    {
                        totalC = totalC + Convert.ToDecimal(e.subTotal);
                    }
                    Session["totalC"] = totalC;
                    //modificamos el detalle en la lista
                    ViewBag.ErrorProducto = "Se modifico el producto =" + d.Productos.nombre + " cantidad= " + d.cantidad;
                    // para devolver la lista de productos
                    ViewBag.Productos = daoProductos.Listar();
                    return View("Cotizar");
                }
            }
            catch
            {
                ViewBag.Error = "No se logro editar el producto ";
                ViewBag.Clientes = daoClientes.Listar();
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("Cotizar");
            }

        }
        public ActionResult CancelarEdicion(FormCollection form)
        {
            return RedirectToAction("Cotizar");
        }

    }
}
