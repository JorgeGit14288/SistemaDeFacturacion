﻿
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = await db.Categorias.FindAsync(id);
            if (categorias == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(categorias);
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
                if(db.Categorias.FindAsync(categorias.idCategoria)!=null)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = await db.Categorias.FindAsync(id);
            if (categorias == null)
            {
               //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(categorias);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCategoria,nombre,descripcion,imagen")] Categorias categorias)
        {            
            if (ModelState.IsValid)
            {
                db.Entry(categorias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorias);

        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = await db.Categorias.FindAsync(id);
            if (categorias == null)
            {
                //return HttpNotFound();
                RedirectToAction("Index");
            }
            return View(categorias);
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
                return View(id);
            }
           
        }
        public bool Existe(int id)
        {
            try
            {
                if (db.Categorias.Where(r=> r.idCategoria==id).Count() >0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;

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
