
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
/**
 * Aca podemos encontrar las categorias de productos, los procesos de CRUD, etc.
 * creado con entity framework y sql server
 *14-06-17 
 * Jorge Fuentes
 * */


namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.Categorias.ToListAsync());
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Categorias>());
            }
          
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {          
                if (id == null)
                {
                    RedirectToAction("Index");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Categorias categorias = await db.Categorias.FindAsync(id);
                if (categorias == null)
                {
                    //return HttpNotFound();
                   return RedirectToAction("Index");
                }
                return View(categorias);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new Categorias());
            }
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCategoria,nombre,descripcion,imagen")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
               // int existe = db.Categorias.Where(r => r.idCategoria == categorias.idCategoria).Count();
                if(db.Categorias.Where(r => r.idCategoria == categorias.idCategoria).Count()>0)
                {
                    ViewBag.Error = "El id esta siendo utilizado por otro registro, intente cambiar el id ";
                    return View(categorias);
                }
                db.Categorias.Add(categorias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorias);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {              
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
              
                Categorias categorias = await db.Categorias.FindAsync(id);
                if (categorias == null)
                {
                    //return HttpNotFound();
                  return  RedirectToAction("Index");
                }
                return View(categorias);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro " + ex.Message;

                return View(new Categorias());
            }
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCategoria,nombre,descripcion,imagen")] Categorias categorias)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    db.Entry(categorias).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(categorias);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido modificar el registro, mensaje de error = " + ex.Message;
                return View(categorias);
            }
       }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
               

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (db.Categorias.FindAsync(id) == null)
                {
                    ViewBag.Error = "El id esta siendo utilizado por otro registro, intente cambiar el id ";
                    return RedirectToAction("Index");
                }
                Categorias categorias = await db.Categorias.FindAsync(id);
                if (categorias == null)
                {
                    //return HttpNotFound();
                    RedirectToAction("Index");
                }
                return View(categorias);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error " + ex.Message;
                return View(new Categorias());
            }
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Categorias categorias = await db.Categorias.FindAsync(id);
                db.Categorias.Remove(categorias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido eliminar el registro, puede ser que este enlazado con otros registros de la base de datos, por tanto no podra eliminarlos, el mensaje de error es: "+ex.Message;
                Categorias categorias = await db.Categorias.FindAsync(id);                
                return View("Details", categorias);
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
