using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class InformesController : Controller
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        //GET: Index
        public ActionResult Index()
        {

            return View();
        }
        // GET: Informes
        public ActionResult ListarProductos()
        {
            return View();
        }

        //REPORTES DE PRODUCTOS
        public ActionResult TodosProductos()
        {
            return View(ctx.Productos.ToList());
        }
    }
}