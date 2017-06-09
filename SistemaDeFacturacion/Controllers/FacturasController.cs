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
            return View(db.Facturas.ToList());
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
                return HttpNotFound();
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
                return HttpNotFound();
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
        public ActionResult FacturasHoy(FormCollection form)
        {
            DateTime fecha = Convert.ToDateTime(form["fecha"]);

            List<Facturas> lista = new List<Facturas>();

            fecha = DateTime.Now.Date;

            ViewBag.Fecha = fecha;
            lista = daoFacturas.ListarFacturasHoy(fecha);
            return View(lista);
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
