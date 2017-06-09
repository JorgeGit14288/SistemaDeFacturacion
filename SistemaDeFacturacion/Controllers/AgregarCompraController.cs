using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using SistemaDeFacturacion.Dao;
using Microsoft.AspNet.Identity;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Administrador, Bodega")]
    public class AgregarCompraController : Controller
    {
        // objetos de acceso a datoa
       private IProductosDao daoProductos = new ProductosDao();
       private IComprasDao daoCompras = new ComprasDao();
      private  IAgregarComprasDao daoAgregarCompras = new AgregarComprasDao();
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        // GET: AgregarCompra
        public ActionResult CrearCompra()
        {
            int idCompra = daoCompras.ObtenerIdActual();
            if (idCompra==0)
            {
                idCompra = 1;
            }
            Session["idCompra"] = idCompra;
            ViewBag.Proveedores = ctx.Proveedores.ToList();
            ViewBag.Categorias = ctx.Categorias.ToList();
            ViewBag.Productos = daoProductos.Listar();
            return View();
        }
        [HttpPost]
        public ActionResult CrearCompra(FormCollection form)
        {
            try
            {
                //pasamos todas las variables para crear el objeto mCompras
                mCompras varCompra = new mCompras();
                //atrapamos los objetos de las sesiones
                Proveedores proveedor = new Proveedores();
                proveedor = (Proveedores)Session["Proveedor"];
                Compras compra = new Compras();
                compra = (Compras)Session["Compra"];
                compra.total = decimal.Parse(Session["totalCompra"].ToString());
                compra.idProveedor = proveedor.idProveedor;
                compra.usuario = User.Identity.GetUserName();
   

                List<DetallesCompra> detalles = new List<DetallesCompra>();
                detalles = (List<DetallesCompra>)Session["detCompra"];

                //antes de mandar al proceso de compras, si alguien ingreso otra compra antes,
                //actualizamos el idcomrpa para todos los productos de la lista

                int idCompra = daoCompras.ObtenerIdActual();
                compra.idCompra = idCompra;
                foreach(var d in detalles)
                {
                    d.idCompra = idCompra;
                }

                // llenamos nuestro objeto que contiene a todos los otors
                varCompra.compra = compra;
                varCompra.detalles = detalles;
                varCompra.proveedor = proveedor;

                // enviamos a realizar el proceso
                if (daoAgregarCompras.Crear(varCompra))
                {
                    //PARA COMPRAS
                    Session["Comprar"] = "";
                    Session["Compra"] = "";
                    Session["detCompra"] = "";
                    Session["totalCompra"] = "";
                    int idCompra2 = daoCompras.ObtenerIdActual();
                    if (idCompra2 == 0)
                    {
                        idCompra2 = 1;
                    }
                    Session["idCompra"] = idCompra2;
                    Session["idDetalle"] = 0;
                    Session["Proveedor"] = "";

                    ViewBag.Proveedores = ctx.Proveedores.ToList();
                    ViewBag.Categorias = ctx.Categorias.ToList();
                    ViewBag.Productos = daoProductos.Listar();
                    ViewBag.Mensaje = "Se ha Registrado la Compra Corretamente";
                    return View();
                }
                else
                {
                    ViewBag.Proveedores = ctx.Proveedores.ToList();
                    ViewBag.Categorias = ctx.Categorias.ToList();
                    ViewBag.Productos = daoProductos.Listar();
                    ViewBag.Error = "No se pudo realizar la transaccion";
                    return View();
                }

            }
            catch
            {
                ViewBag.Error = "Ha ocurrido un error al contactar con el servidor";
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View();
            }
        }
        [HttpPost]
        public ActionResult BorrarCompra(FormCollection form)
        {
            try
            {

                int idCompra = daoCompras.ObtenerIdActual();
          
                //PARA COMPRAS
                Session["Comprar"] = "";
                Session["Compra"] = "";
                Session["detCompra"] = "";
                Session["totalCompra"] = "";
                Session["idCompra"] =  idCompra;
                Session["idDetalle"] = 1;
                Session["Proveedor"] = "";

                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Mensaje = "Ahora puede ingresar una compra al inventario";
                return View("CrearCompra");
            }
            catch
            {

                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Error = "No se ha podido reiniciar los datos.";
                return View("CrearCompra");
            }
        }
        [HttpPost]
        public ActionResult CargarCompra(FormCollection form)
        {
            try
            {
                Compras compra = new Compras();
                Session["Compra"] = "";
                compra.idCompra = Convert.ToInt32(Session["idCompra"]);
                compra.fecha = DateTime.Now;
                compra.idFactura = Convert.ToInt32(form["idFactura"]);
                compra.modificado = DateTime.Now;

                Session["Compra"] = compra;
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Mensaje = "Se han cargado los datos de la compra";
                return View("CrearCompra");
            }
            catch
            {
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Error = "NO se ha podido cargar la compra";
                return View("CrearCompra");
            }
        }

        [HttpPost]
        public ActionResult BuscarProveedor(FormCollection form)
        {
            try
            {
                //Session["Cliente"] = "";
                string idProveedor = form["idProveedor"];
                Proveedores proveedor = new Proveedores();
                proveedor = ctx.Proveedores.Find(idProveedor);

                if (proveedor == null)
                {
                    Session["Proveedor"] = "";
                    proveedor = new Proveedores();
                    proveedor.idProveedor = idProveedor;
                    Session["Proveedor"] = proveedor;
                    // para devolver la lista de productos
                    ViewBag.Proveedores = ctx.Proveedores.ToList();
                    ViewBag.Categorias = ctx.Categorias.ToList();
                    ViewBag.Productos = daoProductos.Listar();
                    ViewBag.Error = "No existe un proveedor con el id ingresado";
                    return View("CrearCompra");
                }
                else
                {
                    ViewBag.Mensaje = "se han encontrado los datos del proveedor, pulse en el boton cargar, para cargarlo a la transaccion";
                    // para devolver la lista de productos
                    ViewBag.Proveedores = ctx.Proveedores.ToList();
                    ViewBag.Categorias = ctx.Categorias.ToList();
                    ViewBag.Productos = daoProductos.Listar();
                    Session["Proveedor"] = proveedor;
                    return View("CrearCompra");
                }

            }
            catch
            {
                ViewBag.Error = "No se ha podido conectar con el servidor";
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
        }
        [HttpPost]
        public ActionResult CargarProveedor(FormCollection form)
        {
            try
            {
                Proveedores proveedor = new Proveedores();
                proveedor.idProveedor = form["idProveedor"];
                proveedor.nombre = form["nombre"];
                proveedor.empresa = form["empresa"];
                proveedor.direccion = form["direccion"];
                proveedor.telefono = form["telefono"];
                proveedor.email = form["email"];
                proveedor.creado = DateTime.Now;
                proveedor.modificado = DateTime.Now;

                Proveedores temp = new Proveedores();
                temp = ctx.Proveedores.Find(proveedor.idProveedor);
                if(temp==null)
                {
                    // el proveedor no existe, lo agregamos a la bd
                    ctx.Proveedores.Add(proveedor);
                    ctx.SaveChanges();
                }
                else
                {
                    proveedor = temp;
                }
                
                //cargamos los datos del proveedor a la session;
                Session["Proveedor"] = "";
                Session["Proveedor"] = proveedor;

                // evalueamos si la variable de session copra tiene algo, si no la creamos e iniciamos
                Compras compra = new Compras();
                if(Session["Compra"].ToString()=="")
                {
                    // no se ha cargado nada en la variable

                }
                else
                {
                    // la variable esta creada, por tanto le pasamos el valor del id proveedor
                    compra = (Compras)Session["Compra"];
                    compra.idProveedor = proveedor.idProveedor;
                    int idCompra = daoCompras.ObtenerIdActual();
                    compra.idCompra = (int)Session["idCompra"];
                    compra.usuario = Session["Usuario"].ToString();
                    Session["Compra"] = compra;

                }

                ViewBag.Mensaje = "Se ha cargado el proveedor de la compra";
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
            catch
            {
                ViewBag.Error = "Error, No se ha podido conectar con el servidor";
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
        }
       
        public ActionResult EliminarDetalle(FormCollection form, int id)
        {
            try
            {

                //Obtenemos el objeto de session de detalles
                List<DetallesCompra> detalles = new List<DetallesCompra>();
                detalles = (List<DetallesCompra>)Session["detCompra"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCompra d = new DetallesCompra();
                //busca el detalle en la lista, segun el id
                d = detalles.Find(r => r.idDetalle == id);
                // elimina el detalle de la lista
                detalles.Remove(d);
                // en esta parate reordenamos los objetos, para que no queden ids de detalle saltados
                //creamos una nueva lista de detalles a quien pasaremos los objetos restantes despues de haber eliminado el detalle

                List<DetallesCompra> nuevaLista = new List<DetallesCompra>();
                //con un bucle recorremos la lista actual y pasamos a la lista nueva, modificando los ids, de detalle
                int idDetalle = 1;
                decimal totalCompra = 0;

                foreach (var e in detalles)
                {
                    //Creamos un nuevo de talle para pasarle los objetos
                    DetallesCompra dn = new DetallesCompra();
                    dn = e;
                    dn.idDetalle = idDetalle;
                    idDetalle++;
                    totalCompra = totalCompra + Convert.ToDecimal(dn.subTotal);
                    nuevaLista.Add(dn);

                    //con este metodo terminamos de pasar la lista antigua a la lista nueva;
                }
                // instanceamos las variables de session con los nuevos datos
                Session["detCompras"] = nuevaLista;
                idDetalle = nuevaLista.Count();
                Session["idDetalle"] = idDetalle;
                Session["totalCompra"] = totalCompra;
                //devolvemos los datos

   
                ViewBag.ErrorProducto = "Se elimino =" + d.Productos.nombre;
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
            catch
            {
                ViewBag.Error = "No se pudo eliminar el producto del detalle";
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");

            }
        }

        public ActionResult EditarDetalle(FormCollection form, int id)
        {
            try
            {
                //Obtenemos el objeto de session de detalles
                List<DetallesCompra> detalles = new List<DetallesCompra>();
                detalles = (List<DetallesCompra>)Session["detCompra"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCompra d = new DetallesCompra();
                //busca el detalle en la lista, segun el id
                d = detalles.Find(r => r.idDetalle == id);
                //obteniendo el producto devolvemos la cantidad y el ID para poder ser Modificados
               // ViewBag.modIdProducto = d.idProducto;
                //ViewBag.modCantidad = d.cantidad;
                //ViewBag.modIdDetalle = d.idDetalle;

                ViewBag.modDetalle = d;
                ViewBag.Prod = ctx.Productos.Find(d.idProducto);

                ViewBag.ErrorProducto = "Se cargaron los datos para modificacion =" + d.Productos.nombre;
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
            catch
            {
                ViewBag.Error = "No se logro cargar el producto =" + id;
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }

        }
        [HttpPost]
        public ActionResult EditarDetalle(FormCollection form)
        {
            try
            {


                int id = Convert.ToInt32(form["idDetalle"]);
                // buscamos el producto
                string idProducto = form["idProducto"];
                decimal cantidad = Convert.ToDecimal(form["cantidad"]);
                decimal precio = Convert.ToDecimal(form["precio"]);
                decimal precioVenta = Convert.ToDecimal(form["precioVenta"]);
                //string observaciones = form["observaciones"];

                //Obtenemos el objeto de session de detalles
                List<DetallesCompra> detalles = new List<DetallesCompra>();
                detalles = (List<DetallesCompra>)Session["detCompra"];
                //obtenemos el detalle a eliminar de la lista
                DetallesCompra d = new DetallesCompra();
                //busca el detalle en la lista, segun el id
                d = detalles.Find(r => r.idDetalle == id);

                //eliminamos el objeto de la lista
                detalles.Remove(d);
                //obteniendo el producto devolvemos la cantidad y el ID para poder ser Modificados
                d.cantidad = cantidad;
                d.precio = precio;
                d.precioVenta = precioVenta;
               // d.observaciones = observaciones;
                d.subTotal = cantidad * precio;
               
                detalles.Add(d);

                //calculamos el nuevo total de la factura
                decimal totalCompra = 0;
                foreach (var e in detalles)
                {
                    totalCompra = totalCompra + Convert.ToDecimal(e.subTotal);
                }
                Session["totalCompra"] = totalCompra;
                //modificamos el detalle en la lista
                ViewBag.Mensaje= "Se modifico el producto =" + d.Productos.nombre + " cantidad= " + d.cantidad;
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");

            }
            catch
            {
                ViewBag.Error = "No se logro editar el producto ";
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }

        }

        public ActionResult CancelarEdicion(FormCollection form)
        {
            ViewBag.Proveedores = ctx.Proveedores.ToList();
            ViewBag.Categorias = ctx.Categorias.ToList();
            ViewBag.Productos = daoProductos.Listar();
            return RedirectToAction("CrearCompra");
        }
        public ActionResult BuscarProducto(string id)
        {
            Productos p = new Productos();
            p = ctx.Productos.Find(id);
            ViewBag.Proveedores = ctx.Proveedores.ToList();
            ViewBag.Categorias = ctx.Categorias.ToList();
            ViewBag.Productos = daoProductos.Listar();
           
            if (p == null)
            {
                p = new Productos();
                p.idProducto = id;
                ViewBag.Error = "No existe el producto con tal id, debe de crearlo";
                ViewBag.tempProd = p;
                return View("CrearCompra");
            }
            else
            {
                ViewBag.Prod = p;
                return View("CrearCompra");
            }

           
        }
        public ActionResult CrearProducto(FormCollection form)
        {
            try
            {
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                string idProducto = form["idProducto"];

                Productos temp = new Productos();
                temp = ctx.Productos.Find(idProducto);
                if (temp == null)
                {
                    temp = new Productos();
                    temp.idProducto = form["idProducto"];
                    temp.nombre = form["nombre"];
                    temp.observacion = form["observacion"];
                    temp.idCategoria = Convert.ToInt32( form["idCategoria"]);
                    temp.precio = 0;
                    temp.precioCompra = 0;
                    temp.creado = DateTime.Now;
                    temp.modificado = DateTime.Now;
                    temp.existencia = 0;

                    ctx.Productos.Add(temp);
                    ctx.SaveChanges();

                    ViewBag.Mensaje = "Se ha creado el producto";
                    ViewBag.Prod = temp;
                    return View("CrearCompra");
                }
                else
                {
                    ViewBag.Prod = temp;
                    ViewBag.Error = "Ya existe un producto con el mismo id";
                    return View("CrearCompra");
                }

                
                
            }
            catch
            {
                var temp = new Productos();
                temp.idProducto = form["idProducto"];
                temp.nombre = form["nombre"];
                temp.observacion = form["observacion"];
                //temp.observacion = form["observacion"];
                temp.idCategoria = Convert.ToInt32(form["idCategoria"]);

                ViewBag.tempProd = temp;

                ViewBag.Error = "Ha ocurrido un error al contactar al servidor";
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                return View("CrearCompra");
            }
        }
        public ActionResult CargarProducto(FormCollection form)
        {
            // buscamos el producto
            string idProducto = form["idProducto"];
            decimal cantidad = Convert.ToDecimal(form["cantidad"]);
            decimal precio = Convert.ToDecimal(form["precio"]);
            decimal precioVenta = Convert.ToDecimal(form["precioVenta"]);

            try
            {
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();


                // string observaciones= form["observaciones"];
                Productos p = new Productos();
                Compras compra = new Compras();
                if (Session["Compra"] == null || Session["Compra"].ToString() == "")
                {
                    ViewBag.Mensaje = "Error, no se ha creado una compra para agregar productos";
                    return View("CrearCompra");

                }
                compra = (Compras)Session["Compra"];
                p = daoProductos.BuscarId(idProducto);

                if (String.IsNullOrEmpty(p.idProducto))
                {
                    ViewBag.ErrorProducto = "No existe  el producto o la existencia es menor a la cantidad solicitada";
                    ViewBag.Productos = daoProductos.Listar();
                    return View("CrearCompra");
                }
                else
                {

                    // verificamos si existe algun detalle
                    List<DetallesCompra> detalles = new List<DetallesCompra>();

                    if (Session["detCompra"].ToString() == "")
                    {

                        // si no hay detalles no se hace nada, puesto que se creo el objeto arriba


                    }
                    else
                    {

                        // si existe detalle, lo pasamos a la variable local detalles
                        detalles = (List<DetallesCompra>)Session["detCompra"];

                    }
                    // creamos un objeto temporal para saber si existe el detalle
                    DetallesCompra temp = new DetallesCompra();
                    temp = detalles.Find(r => r.idProducto == p.idProducto);
                    if (temp == null)
                    {
                        // creamos un nuevo detalle, obtenemos el id de la factura
                        // int idDetalle = Int32.Parse(Session["idDetalle"].ToString());
                        // iniciamos el contador para llenar la lista de detalles
                        int idDetalle = Int32.Parse(Session["idDetalle"].ToString()) + 1;

                        //creamos el nuevo detalle
                        DetallesCompra d = new DetallesCompra();
                        d.idCompra = compra.idCompra;
                        d.idDetalle = idDetalle;
                        d.Productos = p;
                        d.idProducto = p.idProducto;
                        d.cantidad = cantidad;
                        d.precio = precio;
                        d.precioVenta = precioVenta;
                        // d.observaciones = observaciones;
                        d.subTotal = precio * cantidad;
                        decimal totalCompra = 0;
                        detalles.Add(d);
                        if (Session["totalCompra"].ToString() == "" || Session["totalCompra"] == null)
                        {

                        }
                        else
                        {
                            totalCompra = Decimal.Parse(Session["totalCompra"].ToString());
                        }

                        //decimal totalCompra = Convert.ToDecimal(Session["totalCompra"]);
                        totalCompra = totalCompra + Convert.ToDecimal(d.subTotal);
                        Session["totalCompra"] = totalCompra;
                        //volvemos a parsear el al objeto sessio
                        Session["idDetalle"] = idDetalle;
                        Session["detCompra"] = detalles;
                        // para devolver la lista de productos
                    }
                    else
                    {
                        // el producto ya esta registrado en el detalle asi que solo actualizamos los datos

                        // el producto ya existe en el detalle, entonces lo aumentamos

                        if (temp.cantidad == cantidad && temp.precio == precio && temp.precioVenta == precioVenta)
                        {
                            ViewBag.Error = "Los datos ingresados son los mismos por tanto no se modifico el detalle";
                            /// los datos ingresados son los mismos, por tanto no los cargamos ni modificamos el detalle

                        }
                        else
                        {
                            detalles.Remove(temp);
                            temp.precio = precio;
                            temp.precioVenta = precioVenta;
                            temp.cantidad = temp.cantidad + cantidad;
                            temp.subTotal = temp.cantidad * temp.precio;
                            detalles.Add(temp);
                            //recalculamos el total otra vez
                            decimal totalCompra = 0;
                            foreach (var i in detalles)
                            {
                                totalCompra = totalCompra + Convert.ToDecimal(i.subTotal);
                            }
                            Session["totalCompra"] = totalCompra;
                            Session["detCompra"] = detalles;
                            ViewBag.ErrorProducto = "se Modifico " + cantidad + " " + p.nombre + " precio Q" + p.precio + "sub Total  Q" + temp.subTotal;
                            ViewBag.Mensaje = "Se modifico el detalle de la compra";
                        }
                    }
                    ViewBag.Productos = daoProductos.Listar();
                    return View("CrearCompra");
                }

            }
            catch
            {
                // para devolver la lista de productos
                ViewBag.Proveedores = ctx.Proveedores.ToList();
                ViewBag.Categorias = ctx.Categorias.ToList();
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Error = "No hay conexion con la base de datos";
                ViewBag.ErrorProducto = "No se pudo agregar el producto";
                return View("CrearCompra");
            }
        }

    }
}