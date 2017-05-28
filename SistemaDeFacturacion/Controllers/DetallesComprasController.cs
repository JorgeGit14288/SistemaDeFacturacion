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
    public class DetallesComprasController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: DetallesCompras
        public ActionResult Index()
        {
            var detallesCompra = db.DetallesCompra.Include(d => d.Compras).Include(d => d.Productos);
            return View(detallesCompra.ToList());
        }

        // GET: DetallesCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCompra detallesCompra = db.DetallesCompra.Find(id);
            if (detallesCompra == null)
            {
                return HttpNotFound();
            }
            return View(detallesCompra);
        }

        // GET: DetallesCompras/Create
        public ActionResult Create()
        {
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idProveedor");
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre");
            return View();
        }

        // POST: DetallesCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompra,idDetalle,idProducto,cantidad,precio,subTotal,observaciones")] DetallesCompra detallesCompra)
        {
            if (ModelState.IsValid)
            {
                db.DetallesCompra.Add(detallesCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idProveedor", detallesCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCompra.idProducto);
            return View(detallesCompra);
        }

        // GET: DetallesCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCompra detallesCompra = db.DetallesCompra.Find(id);
            if (detallesCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idProveedor", detallesCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCompra.idProducto);
            return View(detallesCompra);
        }

        // POST: DetallesCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompra,idDetalle,idProducto,cantidad,precio,subTotal,observaciones")] DetallesCompra detallesCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallesCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idProveedor", detallesCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "nombre", detallesCompra.idProducto);
            return View(detallesCompra);
        }

        // GET: DetallesCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallesCompra detallesCompra = db.DetallesCompra.Find(id);
            if (detallesCompra == null)
            {
                return HttpNotFound();
            }
            return View(detallesCompra);
        }

        // POST: DetallesCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallesCompra detallesCompra = db.DetallesCompra.Find(id);
            db.DetallesCompra.Remove(detallesCompra);
            db.SaveChanges();
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
