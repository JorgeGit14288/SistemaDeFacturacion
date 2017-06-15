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
    public class ComprasController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Compras
        public ActionResult Index()
        {
            try
            {
                var compras = db.Compras.Include(c => c.Proveedores);
                return View(compras.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Compras>());
            }
            
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Compras compras = db.Compras.Find(id);
                //var detallesCompra = db.DetallesCompra.Include(d => d.Compras).Include(d => d.Productos);
                ViewBag.Detalles = db.DetallesCompra.Where(d => d.idCompra == id).ToList();
                if (compras == null)
                {

                    var compras2 = db.Compras.Include(c => c.Proveedores);
                    ViewBag.Error = "No se ha encontrado la compra con el id indicado, pruebe buscar en la tabla general";
                    return View("Index", compras2.ToList());
                }

                return View(compras);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se pudo cargar el registro, mensaje de error: " + ex.Message;
                return View(new Compras());

            }
            
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.idProveedor = new SelectList(db.Proveedores, "idProveedor", "empresa");
                return View();
            }
          
            catch(Exception ex)
            {
                ViewBag.Error = "Se ha producido una excepcion al cargar a los proveedores " + ex.Message;
                return View();
            }
        }

        /**
         * DESCOMENTAR PARA UTILIZAR LOS METODOS
        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompra,idFactura,idProveedor,total,fecha,creado,modificado")] Compras compras)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //compras.creado = DateTime.Now;
                    compras.modificado = DateTime.Now;
                    db.Compras.Add(compras);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idProveedor = new SelectList(db.Proveedores, "idProveedor", "empresa", compras.idProveedor);
                return View(compras);
            }           
           
             catch (Exception ex)
            {
                ViewBag.idProveedor = new SelectList(db.Proveedores, "idProveedor", "empresa", compras.idProveedor);
                ViewBag.Error = "No se pudo cargar el registro, mensaje de error: " + ex.Message;
                return View(compras);

            }
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            ViewBag.idProveedor = new SelectList(db.Proveedores, "idProveedor", "empresa", compras.idProveedor);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompra,idFactura,idProveedor,total,fecha,creado,modificado")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                compras.modificado = DateTime.Now;
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProveedor = new SelectList(db.Proveedores, "idProveedor", "empresa", compras.idProveedor);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    **/
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
