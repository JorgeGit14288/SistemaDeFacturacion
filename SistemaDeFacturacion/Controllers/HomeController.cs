using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Dao.Helpers;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Controllers
{
    public class HomeController : Controller
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        private HomeHelpers helper = new HomeHelpers();
        public ActionResult Index()
        {
            try
            {
                ViewBag.noProductos = helper.ProductosHoy();
                ViewBag.ProductosSinExistencia = helper.ProductosSinExistencia();
                ViewBag.noCotizaciones = helper.CotizacionesHoy(DateTime.Now);
                ViewBag.noVentas = helper.VentasHoy(DateTime.Now);
                ViewBag.noFacturadas = helper.FacturasHoy(DateTime.Now);            
                ViewBag.noCompras = helper.ComprassHoy(DateTime.Now);

                // del mes
                ViewBag.CotizacionesMes = helper.CotizacionesMes();
                ViewBag.VentasMes = helper.VentasMes();
                ViewBag.ComprasMes = helper.ComprasMes();

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