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
using SistemaDeFacturacion.Models.CloneModel;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class FacturasController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        IFacturasDao daoFacturas = new FacturasDao();
        // GET: Facturas
        public ActionResult Index()
        {
            try
            {
                return View(db.Facturas.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Facturas>());
            }
           
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            try
            {
                if (id == 0)
                {
                    return View(db.Facturas.ToList());
                }
                else
                {
                    ViewBag.Detalles = db.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
                    return View(db.Facturas.ToList());
                }
            }
            catch
            {


                return View(db.Facturas.ToList());
            }
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            ViewBag.Detalles = db.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
            if (facturas == null)
            {
                ViewBag.Error = "No se encuentra la factura que busca, intente buscar en la tabla general";
                return View("Index", db.Facturas.ToList());
            }
            return View(facturas);
        }

        public ActionResult ExportarFactura(int idCotizacion)
        {//reporte que exporta todos los productos
         //creamos la lista para el origen de datos
            try
            {
                FacturacionDbEntities ctx = new FacturacionDbEntities();
                //agregamos la lista de facturas, para lo cual utilizamos el generico ClonFacturas
                List<Facturas> listaf = new List<Facturas>();
                listaf = ctx.Facturas.ToList();
                /// metemos al generico facturas
                List<ClonFacturas> facturas = new List<ClonFacturas>();
                foreach (var f in listaf)
                {
                    ClonFacturas fc = new ClonFacturas();
                    fc.idFactura = f.idFactura;
                    fc.idCotizacion = Convert.ToInt32(f.idCotizacion);
                    fc.fecha = f.fecha;
                    fc.descuento = Convert.ToDecimal(f.descuento);
                    fc.direccion = f.direccion;
                    fc.idPago = f.idPago;
                    fc.iva = Convert.ToDecimal(f.iva);
                    fc.subTotal = Convert.ToDecimal(f.subTotal);
                    fc.total = Convert.ToDecimal(f.total);
                    fc.usuario = f.usuario;
                    fc.nombre = f.nombre;
                    fc.nitCliente = f.nitCliente;

                    facturas.Add(fc);

                }

                List<Cotizaciones> listac = new List<Cotizaciones>();
                //Cotizaciones coti = new Cotizaciones();

                listac = ctx.Cotizaciones.ToList();
                List<CotizacionClon> cotizaciones = new List<CotizacionClon>();
                foreach (var coti in listac)
                {
                    CotizacionClon clonCoti = new CotizacionClon();
                    clonCoti.idCotizacion = coti.idCotizacion;
                    clonCoti.estado = coti.estado;
                    clonCoti.fecha = coti.fecha;
                    clonCoti.direccion = coti.direccion;
                    clonCoti.nitCliente = coti.nitCliente;
                    clonCoti.subTotal = Convert.ToDecimal(coti.subTotal);
                    clonCoti.descuento = Convert.ToDecimal(coti.descuento);
                    clonCoti.usuario = coti.usuario;
                    clonCoti.nombre = coti.nombre;
                    clonCoti.total = Convert.ToDecimal(coti.total);
                    cotizaciones.Add(clonCoti);
                }
                List<DetallesCotizacion> detalles1 = new List<DetallesCotizacion>();
                List<DetallesCotizacionClon> detalles = new List<DetallesCotizacionClon>();

                detalles1 = db.DetallesCotizacion.Where(d => d.idCotizacion == idCotizacion).ToList();
                foreach (var d in detalles1)
                {
                    DetallesCotizacionClon dc = new DetallesCotizacionClon();
                    dc.idCotizacion = d.idCotizacion;
                    dc.idDetalle = d.idDetalle;
                    dc.idProducto = d.idProducto;
                    dc.cantidad = Convert.ToDecimal(d.cantidad);
                    dc.precio = Convert.ToDecimal(d.precio);
                    dc.subTotal = Convert.ToDecimal(d.subTotal);
                    dc.descripcion = d.descripcion;
                    dc.descuento = Convert.ToDecimal(d.descuento);

                    detalles.Add(dc);
                }



                //creamos el report document
                ReportDocument repDocument = new ReportDocument();
                repDocument.Load(Path.Combine(Server.MapPath
                    ("~/Reports/Facturas"), "crFactura.rpt"));
                // le agregamos los dos datasource, uno para cada tabla
                repDocument.Database.Tables[0].SetDataSource(facturas);
                repDocument.Database.Tables[1].SetDataSource(detalles);
                repDocument.Database.Tables[2].SetDataSource(cotizaciones);

                //agregamos el parametro
                repDocument.SetParameterValue("cotizacionId", idCotizacion);

                //esto no se para que sea 
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = repDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Factura.pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lo sentimos, no se pudo exportar el informe " + ex.Message;
                return RedirectToAction("Details", "Facturas", new { id = idCotizacion });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /**
         * 
         * DESCOMENTAR SI UTILIZA ALGUN METODO
        // GET: Facturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFactura,nitCliente,nombre,direccion,fecha,total,usuario")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Facturas.Add(facturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFactura,nitCliente,nombre,direccion,fecha,total,usuario")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturas facturas = db.Facturas.Find(id);
            db.Facturas.Remove(facturas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        **/
       

    }
}
