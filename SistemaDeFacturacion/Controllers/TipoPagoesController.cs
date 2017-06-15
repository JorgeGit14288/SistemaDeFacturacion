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
            try
            {
                return View(await db.TipoPago.ToListAsync());
            }
            
             catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<TipoPago>());
            }
        }

        // GET: TipoPagoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TipoPago tipoPago = await db.TipoPago.FindAsync(id);
                if (tipoPago == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(tipoPago);
            }
            catch(Exception ex)
            {
                ViewBag.Error = " No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new TipoPago());
            }
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
            try
            {


                if (ModelState.IsValid)
                {
                    if (db.TipoPago.FindAsync(tipoPago.id) != null)
                    {
                        ViewBag.Error = "El Codigo del registro que ingreso ya esta siendo utilizado con otro registro, pruebe cambiar el id. ";
                        return View(tipoPago);
                    }
                    db.TipoPago.Add(tipoPago);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = " No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(tipoPago);
            }

            return View(tipoPago);
        }

        // GET: TipoPagoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TipoPago tipoPago = await db.TipoPago.FindAsync(id);
                if (tipoPago == null)
                {
                    //return HttpNotFound();
                   return RedirectToAction("Index");
                }
                return View(tipoPago);
            }
            catch (Exception ex)
            {
                ViewBag.Error = " No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new TipoPago());
            }
        }

        // POST: TipoPagoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nombre,descripcion")] TipoPago tipoPago)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    db.Entry(tipoPago).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tipoPago);
            }
            catch (Exception ex)
            {
                ViewBag.Error = " No se ha podido modificar el registro, mensaje de error: " + ex.Message;
                return View(tipoPago);
            }
        }

        // GET: TipoPagoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                TipoPago tipoPago = await db.TipoPago.FindAsync(id);
                if (tipoPago == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(tipoPago);
            }
            catch (Exception ex)
            {
                ViewBag.Error = " No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new TipoPago());
            }
        }

        // POST: TipoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {


                TipoPago tipoPago = await db.TipoPago.FindAsync(id);
                db.TipoPago.Remove(tipoPago);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = " No se ha podido eliminar el registro, mensaje de error: " + ex.Message;
                TipoPago tipoPago = await db.TipoPago.FindAsync(id);
                return View(tipoPago);
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
