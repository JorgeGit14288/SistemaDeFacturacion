using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using SistemaDeFacturacion.Dao;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
       

        private FacturacionDbEntities db = new FacturacionDbEntities();
        private IProductosDao daoProductos = new ProductosDao();

        // GET: Productos
        public ActionResult Index()
        {
            try
            {
                return View(db.Productos.ToList());
            }
            
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Productos>());
            }
        }
        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports/Productos"), "ReporteProductos.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index", db.Productos.ToList());
            }
            List<Productos> cm = new List<Productos>();
            using (FacturacionDbEntities dc = new FacturacionDbEntities())
            {
                cm = dc.Productos.ToList();
            }
            ReportDataSource rd = new ReportDataSource("dsProductos", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        // GET: Productos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,nombre,precio,existencia,observacion,idCategoria,imagen")] Productos productos, HttpPostedFileBase imagenMunicipio, FormCollection form)
        {
            //HttpPostedFileBase laImagen = Convert.toh form["imagenProducto"];

            if (imagenMunicipio != null && imagenMunicipio.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(imagenMunicipio.InputStream))
                {
                    imageData = binaryReader.ReadBytes(imagenMunicipio.ContentLength);
                }
                //setear la imagen a la entidad que se creara
                productos.imagen = imageData;
            }
            productos.existencia = 0;
            productos.precioCompra = 0;

            if (ModelState.IsValid)
            {
                if (db.Productos.FindAsync(productos.idProducto) != null)
                {
                    ViewBag.Error = "El Codigo del registro que ingreso ya esta siendo utilizado con otro registro, pruebe cambiar el id. ";
                    return View(productos);
                }
                productos.creado = DateTime.Now;
                productos.modificado = DateTime.Now;
                db.Productos.Add(productos);
                db.SaveChanges();
                ViewBag.Mensaje = "Se ha registrado un nuevo producto";
                return View("Index", db.Productos.ToList());
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            ViewBag.Error = "No se ha podido registrar el producto, verifique que no exista ya uno similar o con el mismo id, de lo contrario contacte con el tecnico";
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,nombre,precio,existencia,observacion,idCategoria,imagen")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.modificado = DateTime.Now;
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Mensaje = "Se ha actualizado el producto con exito";
                return View(productos);
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "idCategoria", "nombre");
            ViewBag.Error = "No se ha podido modificar el producto, compruebe que los nuevos datos son validos, si el problema persiste, contacte al tecnico";
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Productos productos = db.Productos.Find(id);
                db.Productos.Remove(productos);
                db.SaveChanges();
                ViewBag.Mensaje = "Se ha eliminado un registro de la base de datos";
                return View("Index", db.Productos.ToList());
            }
            catch
            {
                ViewBag.Error = "No se ha podido eliminar el registro de la base de datos, compruebe que este no este en el detalle de alguna factura, si el problema persiste contacte al tecnico";
                return View(id);
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
                Productos p = new Productos();
                p = daoProductos.BuscarId(idProducto);

                if (String.IsNullOrEmpty(p.idProducto))
                {
                    ViewBag.Error = "No se logro encontrar el producto";
                    ViewBag.Productos = daoProductos.Listar();
                    return View("Index", daoProductos.Listar());
                }
                else
                {
                    // evaluamos si la existencia es menor a la cantidad solicitada
                    if (p.existencia < cantidad)
                    {
                        ViewBag.Error = "Error, La cantidad solicitada es menor a la existencia,";
                        ViewBag.Productos = daoProductos.Listar();
                        return View("Index", daoProductos.Listar());

                    }
                    else
                    {
                        // creamos un nuevo detalle, obtenemos el id de la factura
                        int idFactura = Int32.Parse(Session["idFactura"].ToString());
                        // iniciamos el contador para llenar la lista de detalles
                        int idDetalle = Int32.Parse(Session["idDetalle"].ToString()) + 1;

                        DetallesCotizacion d = new DetallesCotizacion()
                        {
                            idCotizacion = idFactura,
                            idDetalle = idDetalle,
                            idProducto = p.idProducto,
                            precio = p.precio,
                            cantidad = cantidad,
                            Productos = p
                        };
                        //d.Productos.nombre = p.nombre;
                        d.subTotal = d.precio * d.cantidad;

                        decimal totalFactura = Convert.ToDecimal(Session["totalFactura"]);
                        totalFactura = totalFactura + Convert.ToDecimal(d.subTotal);
                        Session["totalFactura"] = totalFactura;
                        // verificamos si existe algun detalle
                        List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                        if (Session["Detalles"].ToString() == "")
                        {

                        }
                        else
                        {
                            // si existe detalle, lo pasamos a la variable local detalles
                            detalles = (List<DetallesCotizacion>)Session["Detalles"];
                        }
                        //agregamos el nuevo detalle a los detalles
                        detalles.Add(d);
                        //volvemos a parsear el al objeto session
                        Session["Detalles"] = detalles;
                        Session["idDetalle"] = idDetalle;
                        // para devolver la lista de productos

                        ViewBag.Mensaje = "se agrego " + cantidad + " " + p.nombre + " precio Q" + p.precio + "sub Total  Q" + d.subTotal;

                        ViewBag.Productos = daoProductos.Listar();
                        return View("Index", daoProductos.Listar());
                    }
                }
            }
            catch
            {
                // para devolver la lista de productos
                ViewBag.Productos = daoProductos.Listar();
                ViewBag.Error = "No hay conexion con la base de datos";
                ViewBag.ErrorProducto = "No se pudo agregar el producto";
                return View("Facturar");
            }
        }
        // GET: Productos/Edit/5
        public ActionResult AgregarExistencia(string id, FormCollection form)
        {
            if (id == null)
            {

                return View(new Productos());
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarExistencia([Bind(Include = "idProducto,nombre,precio,existencia,observacion,imagen")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }
        public ActionResult ConertirImagen(string idProducto)
        {
            var imagen = db.Productos.Where(p => p.idProducto == idProducto).FirstOrDefault();
            return File(imagen.imagen, "image/png");

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
