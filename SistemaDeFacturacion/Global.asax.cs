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
            //para cotizacion 
            Session["CotizarActual"] = null;
            Session["Cotizacion"] = "";
            Session["idCotizacion"] = "1";
            Session["DetallesC"] = "";
            Session["idDetalleC"] = "0";
            Session["Usuario"] = "";
            Session["totalC"] = "0";
            Session["DescuentoC"] = "0";
            Session["subTotalC"] = 0;
            Session["ClienteC"] = "";


            // para crear una factura
            Session["Factura"] = "";
            Session["Cliente"] = "";
            Session["idFactura"] = "1";
            Session["DetallesF"] = "";
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
