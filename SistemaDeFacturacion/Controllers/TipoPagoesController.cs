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
    public class TipoPagoesController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: TipoPagoes
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoPago.ToListAsync());
        }

        // GET: TipoPagoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = await db.TipoPago.FindAsync(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // GET: TipoPagoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPagoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nombre,descripcion")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                db.TipoPago.Add(tipoPago);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPago);
        }

        // GET: TipoPagoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = await db.TipoPago.FindAsync(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPagoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nombre,descripcion")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoPago);
        }

        // GET: TipoPagoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = await db.TipoPago.FindAsync(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoPago tipoPago = await db.TipoPago.FindAsync(id);
            db.TipoPago.Remove(tipoPago);
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
