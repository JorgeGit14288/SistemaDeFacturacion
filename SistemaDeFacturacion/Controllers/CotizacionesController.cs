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

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class CotizacionesController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Cotizaciones
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.Cotizaciones.ToListAsync());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Cotizaciones>());
            }
           
        }

        // GET: Cotizaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cotizaciones cotizaciones = await db.Cotizaciones.FindAsync(id);
                if (cotizaciones == null)
                { //return HttpNotFound();
                    RedirectToAction("Index");
                }

                List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                detalles = db.DetallesCotizacion.Where(d => d.idCotizacion == id).ToList();
                ViewBag.Detalles = detalles;
                return View(cotizaciones);

            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error " + ex.Message;
                ViewBag.Detalles = new List<DetallesCotizacion>();
                return View(new Cotizaciones());
               
            }
            
        }
        public ActionResult DetallesVenta(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones =  db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return HttpNotFound();
            }
            if (cotizaciones.estado == "Facturado")
            {
                return RedirectToAction ("DetallesFactura", new { id = id });
            }
            if (cotizaciones.estado == "Cotizado")
            {
                return RedirectToAction("Details", new { id = id });
            }
            List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
            detalles = db.DetallesCotizacion.Where(d => d.idCotizacion == id).ToList();
            ViewBag.Detalles = detalles;
            return View(cotizaciones);
        }
        public ActionResult DetallesFactura(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones c = new Cotizaciones();
            c = db.Cotizaciones.Find(id);

            if (c.estado == "Cotizado")
            {
                return View("Details", new { id = id });
            }
            if (c.estado == "Vendido")
            {
                return View("DetallesVenta", new { id = id });
            }
            Facturas facturas = db.Facturas.SingleOrDefault(f=> f.idCotizacion ==id);
            ViewBag.Detalles = db.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
            if (facturas == null)
            {
                ViewBag.Error = "No se encuentra la factura que busca, intente buscar en la tabla general";
                return View("Index", db.Facturas.ToList());
            }
            return View(facturas);
        }

        // GET: Cotizaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cotizaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCotizacion,nombre,fecha,subTotal,descuento,total,usuario,estado")] Cotizaciones cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Cotizaciones.Add(cotizaciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cotizaciones);
        }

        // GET: Cotizaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = await db.Cotizaciones.FindAsync(id);
            if (cotizaciones == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCotizacion,nombre,fecha,subTotal,descuento,total,usuario,estado")] Cotizaciones cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizaciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotizaciones cotizaciones = await db.Cotizaciones.FindAsync(id);
            if (cotizaciones == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cotizaciones cotizaciones = await db.Cotizaciones.FindAsync(id);
            db.Cotizaciones.Remove(cotizaciones);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult Vender(int id)
        {
            try
            {
                Cotizaciones coti = new Cotizaciones();
                coti = db.Cotizaciones.Find(id);
                coti.estado = "Venta";
                db.SaveChanges();
                return RedirectToAction("Details", "Cotizaciones", new { id = id });
            }
            catch
            {
                return RedirectToAction("Details", "Cotizaciones", new { id = id });
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
    }
}
