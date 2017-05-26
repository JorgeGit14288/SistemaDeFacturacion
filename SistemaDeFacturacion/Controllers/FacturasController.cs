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
    public class FacturasController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Facturas
        public async Task<ActionResult> Index()
        {
            var facturas = db.Facturas.Include(f => f.Clientes).Include(f => f.Cotizaciones).Include(f => f.TipoPago1);
            return View(await facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.nitCliente = new SelectList(db.Clientes, "nit", "nombre");
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre");
            ViewBag.tipoPago = new SelectList(db.TipoPago, "id", "nombre");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idFactura,nitCliente,nombre,direccion,fecha,subTotal,descuento,total,idCotizacion,usuario,tipoPago,idPago")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Facturas.Add(facturas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.nitCliente = new SelectList(db.Clientes, "nit", "nombre", facturas.nitCliente);
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", facturas.idCotizacion);
            ViewBag.tipoPago = new SelectList(db.TipoPago, "id", "nombre", facturas.tipoPago);
            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.nitCliente = new SelectList(db.Clientes, "nit", "nombre", facturas.nitCliente);
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", facturas.idCotizacion);
            ViewBag.tipoPago = new SelectList(db.TipoPago, "id", "nombre", facturas.tipoPago);
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idFactura,nitCliente,nombre,direccion,fecha,subTotal,descuento,total,idCotizacion,usuario,tipoPago,idPago")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.nitCliente = new SelectList(db.Clientes, "nit", "nombre", facturas.nitCliente);
            ViewBag.idCotizacion = new SelectList(db.Cotizaciones, "idCotizacion", "nombre", facturas.idCotizacion);
            ViewBag.tipoPago = new SelectList(db.TipoPago, "id", "nombre", facturas.tipoPago);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Facturas facturas = await db.Facturas.FindAsync(id);
            db.Facturas.Remove(facturas);
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
