﻿using System;
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
    public class ClientesController : Controller
    {
        private FacturacionDbEntities db = new FacturacionDbEntities();
        // GET: Clientes
        public ActionResult Index()
        {
            try
            {
                return View(db.Clientes.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(new List<Clientes>());
            }
           
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nit,nombre,direccion,telefono")] Clientes clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.Clientes.Where(r => r.nit == clientes.nit).Count() > 0)
                    {
                        ViewBag.Error = "El id esta siendo utilizado por otro registro, intente cambiar el id ";
                        return View(clientes);
                    }
                    clientes.creado = DateTime.Now;
                    clientes.modificado = DateTime.Now;
                    db.Clientes.Add(clientes);
                    db.SaveChanges();
                    ViewBag.Mensaje = "Se ha creado un nuevo registro en la base de datos";
                    return View("Index", db.Clientes.ToList());
                }
                ViewBag.Error = "No se ha podido crear el registro, puede que exista ya un cliente con este nit";
                return View(clientes);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido crear el registro, mensaje de error: "+ex.Message;
                return View(clientes);
            }
            
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clientes clientes = db.Clientes.Find(id);
                if (clientes == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Index");
                }
                return View(clientes);
            }        
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, mensaje de error: " + ex.Message;
                return View(new Clientes());
            }

        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nit,nombre,direccion,telefono")] Clientes clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clientes.modificado = DateTime.Now;
                    db.Entry(clientes).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Mensaje = "Se ha actualizado el registro del cliente en la ase de datos";
                    return View(clientes.nit);
                }
                ViewBag.Error = "No se han podido guardar los cambios, si el problema persiste, contacte con el tecnico";
                return View(clientes);
            }
            
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido modificar el registro, mensaje de error: " + ex.Message;
                return View(clientes);
            }

        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clientes clientes = db.Clientes.Find(id);
                if (clientes == null)
                {
                    //return HttpNotFound();
                  return  RedirectToAction("Index");
                }
                return View(clientes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha cargar cargar el registro, mensaje de error: " + ex.Message;
                return View(new Clientes());
            }

        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Clientes clientes = db.Clientes.Find(id);
                db.Clientes.Remove(clientes);
                db.SaveChanges();
                ViewBag.Mensaje = "Se ha eliminado un registro de la base de datos";
                return View("Index", db.Clientes.ToList());
            }
            catch
            {
                ViewBag.Error = "No se pudo elimnar el registro, puede que la conexion a el servidor no este estable, o que el cliente tanga facturas a su nombre, contacte con el tecnico";
                Clientes clientes = db.Clientes.Find(id);
                return View(clientes);
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
