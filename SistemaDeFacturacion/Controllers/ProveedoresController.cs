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
    public class ProveedoresController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Proveedores
        public ActionResult Index()
        {
            try
            {
                return View(db.Proveedores.ToList());
            }
            
             catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Proveedores>());
            }
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (id == null)
                {
                    // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    //return HttpNotFound();
                    RedirectToAction("Index");
                }
                return View(proveedores);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new Proveedores());
            }

            
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.Proveedores.Where(r => r.idProveedor == proveedores.idProveedor).Count() > 0)
                    {
                        ViewBag.Error = "El Codigo del registro que ingreso ya esta siendo utilizado con otro registro, pruebe cambiar el id. ";
                        return View(proveedores);
                    }
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
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(proveedores);
            }
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    //return HttpNotFound();
                    RedirectToAction("Index");
                }
                return View(proveedores);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new Proveedores());
            }
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProveedor,empresa,nombre,direccion,telefono,email,creado,modificado")] Proveedores proveedores)
        {
            try
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
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(proveedores);
            }
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    //return HttpNotFound();
                    RedirectToAction("Index");
                }
                return View(proveedores);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new Proveedores());
            }
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
            catch(Exception ex)
            {
                ViewBag.Error = "No se pudo elimianar el registro, mesaje de error: "+ex.Message;
                Proveedores proveedores = db.Proveedores.Find(id);
                return View(proveedores);
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
