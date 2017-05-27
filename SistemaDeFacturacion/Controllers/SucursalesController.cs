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
    public class SucursalesController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Sucursales
        public async Task<ActionResult> Index()
        {
            return View(await db.Sucursales.ToListAsync());
        }

        // GET: Sucursales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursales sucursales = await db.Sucursales.FindAsync(id);
            if (sucursales == null)
            {
                return HttpNotFound();
            }
            return View(sucursales);
        }

        // GET: Sucursales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sucursales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idSucursal,nombre,direccion,telefono1,telefono2")] Sucursales sucursales)
        {
            if (ModelState.IsValid)
            {
                db.Sucursales.Add(sucursales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sucursales);
        }

        // GET: Sucursales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursales sucursales = await db.Sucursales.FindAsync(id);
            if (sucursales == null)
            {
                return HttpNotFound();
            }
            return View(sucursales);
        }

        // POST: Sucursales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idSucursal,nombre,direccion,telefono1,telefono2")] Sucursales sucursales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sucursales);
        }

        // GET: Sucursales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursales sucursales = await db.Sucursales.FindAsync(id);
            if (sucursales == null)
            {
                return HttpNotFound();
            }
            return View(sucursales);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sucursales sucursales = await db.Sucursales.FindAsync(id);
            db.Sucursales.Remove(sucursales);
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
