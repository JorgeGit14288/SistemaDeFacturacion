using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Dao.Helpers;

namespace SistemaDeFacturacion.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            try
            {
                IniciarEntidades varini = new IniciarEntidades();
                varini.CrearEntidades();
                ViewBag.Mensaje = "Ingreso Exitoso";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = "No se han creado las entidades necesarias para iniciar el sistema";
                return View();
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}