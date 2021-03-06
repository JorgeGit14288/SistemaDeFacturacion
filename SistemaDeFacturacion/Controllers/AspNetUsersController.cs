﻿/**
 * Este controlador  dirige las operaciones de login, registro y autenticacion de usuarios
 * creado con el asistente de microsoft identy.
 *14-06-17 
 * Jorge Fuentes
 * */

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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class AspNetUsersController : Controller
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            List<AspNetUsers> lista = new List<AspNetUsers>();
            try
            {
                lista = ctx.AspNetUsers.ToList();
                return View(lista);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar la vista, Mensaje de error :" + ex.Message;
                return View(lista);
            }
           
           
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                return View(ctx.AspNetUsers.SingleOrDefault((r => r.Id == id)));
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, Mensaje de error: " + ex.Message;
                return View(new  AspNetUsers());
            }
            
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register", "Account");

        }
        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                ViewBag.Roles = ctx.AspNetRoles.ToList();
                return View(ctx.AspNetUsers.SingleOrDefault((r => r.Id == id)));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo obtener la vista";
               return RedirectToAction("Index");
            }
            
            
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection, AspNetUsers user)
        {
            ViewBag.Roles = ctx.AspNetRoles.ToList();
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Los datos a guardar no son validos.";
                    return View(user);
                }

                AspNetUsers actual = new AspNetUsers();
                actual = ctx.AspNetUsers.Find(id);
                actual.nombre = user.nombre;
                actual.direccion = user.direccion;
                actual.Activo = user.Activo;
                actual.Email = user.Email;
                actual.UserName = user.UserName;
                actual.tel_casa = user.tel_casa;

                ctx.Entry(actual).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error " + ex.ToString();
                return View(user);
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                return View(ctx.AspNetUsers.SingleOrDefault((r => r.Id == id)));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, Mensaje de error: " + ex.Message;
                return View(new AspNetUsers());
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                AspNetUsers user = new AspNetUsers();
                user = ctx.AspNetUsers.Find(id);
              //  ctx.AspNetUsers.Remove(user);
               // ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar " + ex.ToString();
                return View(id);
            }
        }
        public ActionResult AgregarRol(string id)
        {
            ViewBag.Roles = ctx.AspNetRoles.ToList();
            return View(ctx.AspNetUsers.Find(id));
        }
        [HttpPost]
        public ActionResult AgregarRol(string idUser, string idRol)
        {
            AspNetUsers user = ctx.AspNetUsers.Find(idUser);
            AspNetRoles rol = ctx.AspNetRoles.Find(idRol);
            if (user.AspNetRoles.FirstOrDefault(r => r.Id == rol.Id) == null)
            {
                user.AspNetRoles.Add(rol);
                ctx.SaveChanges();

                return RedirectToAction("Edit", "AspNetUsers", new { id = idUser });

            }

            ViewBag.Error = "El rol ya existe en el usuario actual";
            return RedirectToAction("Edit", "AspNetUsers", new { id = idUser });
        }
        [HttpPost]
        public ActionResult EliminarRol(string idUser, string idRol)
        {
            AspNetUsers user = ctx.AspNetUsers.Find(idUser);
            AspNetRoles rol = ctx.AspNetRoles.Find(idRol);
            if (user.AspNetRoles.FirstOrDefault(r => r.Id == rol.Id) == null)
            {
                ViewBag.Error = "No se pudo eliminar el rol del usuario";
                return RedirectToAction("Edit", "AspNetusers", new { id = idUser });

            }
            user.AspNetRoles.Remove(rol);
            ctx.SaveChanges();
            return RedirectToAction("Edit", "AspNetUsers", new { id = idUser });

        }
        [HttpGet]
        public ActionResult EditarUsuarios()      
        {
            try
            {
                string nombre = Session["Usuario"].ToString();
                if (String.IsNullOrEmpty(nombre))
                {
                    Session["Usuario"] = User.Identity.GetUserName();
                    nombre = User.Identity.GetUserName();
                    // return View("Error", "Home");
                }
                ViewBag.Roles = ctx.AspNetRoles.ToList();
                return View(ctx.AspNetUsers.SingleOrDefault((r => r.UserName == nombre)));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se ha podido cargar el registro, Mensaje de error: " + ex.Message;
                return View(new AspNetUsers());
            }
            
        }
        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult EditarUsuarios( FormCollection collection, AspNetUsers user)
        {
            ViewBag.Roles = ctx.AspNetRoles.ToList();
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Los datos a guardar no son validos.";
                    return View(user);
                }
                string nombre = Session["Usuario"].ToString();
                if (String.IsNullOrEmpty(nombre))
                {
                    Session["Usuario"] = User.Identity.GetUserName();
                    nombre = User.Identity.GetUserName();
                    // return View("Error", "Home");
                }

                AspNetUsers actual = new AspNetUsers();
                actual = ctx.AspNetUsers.SingleOrDefault(r => r.UserName == nombre);
                actual.nombre = user.nombre;
                actual.direccion = user.direccion;
                actual.Activo = user.Activo;
                actual.Email = user.Email;
                actual.UserName = user.UserName;
                actual.tel_casa = user.tel_casa;

                ctx.Entry(actual).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
                return View(actual);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error " + ex.ToString();
                return View(user);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
