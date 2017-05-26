using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SistemaDeFacturacion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public void Session_Start()
        {
            Session["Factura"] = "";
            Session["Cliente"] = "";
            Session["idFactura"] = "1";
            Session["Detalles"] = "";
            Session["idDetalle"] = "0";
            Session["Usuario"] = "";
            Session["totalFactura"] = "0";
            Session["totalSinDescuento"] = "0";
            Session["subTotal"] = 0;
            Session["Iva"] = "0";

            //PARA COMPRAS
            Session["Comprar"] = "";
            Session["Compra"] = "";
            Session["detCompra"] = "";
            Session["totalCompra"] = "";
            Session["idCompra"] = "1";
            Session["idDetalleC"] = "0";
            Session["Proveedor"] = "";

            //para asignar roles
            Session["Roles"] = "";
            Session["idUser"] = "";
        }
    }
}
