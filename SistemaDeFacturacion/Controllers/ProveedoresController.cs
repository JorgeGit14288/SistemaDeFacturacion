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
    public class ProveedoresController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Proveedores
        public ActionResult Index()
        {
            return View(db.Proveedores.ToList());
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProveedor,empresa,nombre,direccion,telefono,email,creado,modificado")] Proveedores proveedores)
        {

            if (ModelState.IsValid)
            {
                proveedores.creado = DateTime.Now;
                proveedores.modificado = DateTime.Now;
                db.Proveedores.Add(proveedores);
                db.SaveChanges();
                ViewBag.Mensaje = "Registro Creado";
                return View("Index", db.Proveedores.ToList());
            }
            ViewBag.Error = "No se pudo crear el registro en la base de datos";
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProveedor,empresa,nombre,direccion,telefono,email,creado,modificado")] Proveedores proveedores)
        {

            if (ModelState.IsValid)
            {
                proveedores.modificado = DateTime.Now;
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Mensaje = "Registro Actualizado";
                return View("Index", db.Proveedores.ToList());
            }
            ViewBag.Error = "No se pudo actualizar los datos del proveedor";
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {


                Proveedores proveedores = db.Proveedores.Find(id);
                db.Proveedores.Remove(proveedores);
                db.SaveChanges();
                ViewBag.Mensaje = "Se ha eliminado un registro de la base de datos";
                return View("Index", db.Proveedores.ToList());
            }
            catch
            {
                ViewBag.Error = "No se pudo elimianar el registro";
                return View(id);
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
