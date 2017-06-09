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
    public class DetallesCotizacionsController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: DetallesCotizacions
        public async Task<ActionResult> Index()
        {
            var detallesCotizacion = db.DetallesCotizacion.Include(d => d.Cotizaciones).Include(d => d.Productos);
            return View(await detallesCotizacion.ToListAsync());
        }

        // GET: DetallesCotizacions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCotizacion detallesCotizacion = await db.DetallesCotizacion.FindAsync(id);
            if (detallesCotizacion == null)
            {
                return HttpNotFound();
            }
            return View(detallesCotizacion);
        }

        // GET: DetallesCotizacions/Create
        public ActionResult Create()
        {
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre");
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre");
            return View();
        }

        // POST: DetallesCotizacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idDetalle,idCotizacion,idProducto,cantidad,precio,descuento,subTotal")] DetallesCotizacion detallesCotizacion)
        {
            if (ModelState.IsValid)
            {
                db.DetallesCotizacion.Add(detallesCotizacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", detallesCotizacion.idCotizacion);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCotizacion.idProducto);
            return View(detallesCotizacion);
        }

        // GET: DetallesCotizacions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCotizacion detallesCotizacion = await db.DetallesCotizacion.FindAsync(id);
            if (detallesCotizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", detallesCotizacion.idCotizacion);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCotizacion.idProducto);
            return View(detallesCotizacion);
        }

        // POST: DetallesCotizacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idDetalle,idCotizacion,idProducto,cantidad,precio,descuento,subTotal")] DetallesCotizacion detallesCotizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallesCotizacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", detallesCotizacion.idCotizacion);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCotizacion.idProducto);
            return View(detallesCotizacion);
        }

        // GET: DetallesCotizacions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCotizacion detallesCotizacion = await db.DetallesCotizacion.FindAsync(id);
            if (detallesCotizacion == null)
            {
                return HttpNotFound();
            }
            return View(detallesCotizacion);
        }

        // POST: DetallesCotizacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DetallesCotizacion detallesCotizacion = await db.DetallesCotizacion.FindAsync(id);
            db.DetallesCotizacion.Remove(detallesCotizacion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
