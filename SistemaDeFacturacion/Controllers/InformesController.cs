using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using SistemaDeFacturacion.Models.CloneModel;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class InformesController : Controller
    {
        private FacturacionDbEntities ctx = new FacturacionDbEntities();
        //GET: Index

        //informes sobre cotizaciones
        public ActionResult ImprimirCotizacion(int idCotizacion, string vista)
        {
            
             return View();
        }
        public ActionResult Index()
        {

            return View();
        }
        // GET: Informes
        public ActionResult ExportarProductos()
        {
            //reporte que exporta todos los productos
            //creamos la lista para el origen de datos
            List<Productos> listaProductos = new List<Productos>();
            List<ProductosClon> listaClon = new List<ProductosClon>();
            listaProductos = ctx.Productos.ToList();

            foreach(var p in listaProductos)
            {
                ProductosClon pc = new ProductosClon();
                pc.idProducto = p.idProducto;
                pc.nombre = p.nombre;
                pc.idCategoria = Convert.ToInt32(p.idCategoria);
                pc.existencia = Convert.ToDecimal(p.existencia);
                pc.precioCompra = Convert.ToDecimal(p.precioCompra);
                pc.precio = Convert.ToDecimal(p.precio);
                pc.creado = Convert.ToDateTime(p.creado);
                pc.modificado = Convert.ToDateTime(p.modificado);
                pc.observacion = p.observacion;
                pc.imagen = p.imagen;
                listaClon.Add(pc);


            }

            //creamos el report document
            ReportDocument repDocument = new ReportDocument();
            repDocument.Load(Path.Combine(Server.MapPath
                ("~/Reports/Productos"), "crProductos.rpt"));

            // le pasamos los datos al reporte 
            repDocument.SetDataSource(listaClon);

            //esto no se para que sea 
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = repDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "InformeProductos.pdf");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lo sentimos, no se pudo exportar el informe " + ex.Message;
                return View("Index");
            }
            
            //return View();
        }
        public ActionResult ExportarProductosCategorias()
        {
            //reporte que exporta todos los productos
            //creamos la lista para el origen de datos
            List<Productos> listaProductos = new List<Productos>();
            List<ProductosClon> listaClon = new List<ProductosClon>();
            listaProductos = ctx.Productos.ToList();

            foreach (var p in listaProductos)
            {
                ProductosClon pc = new ProductosClon();
                pc.idProducto = p.idProducto;
                pc.nombre = p.nombre;
                pc.idCategoria = Convert.ToInt32(p.idCategoria);
                pc.existencia = Convert.ToDecimal(p.existencia);
                pc.precioCompra = Convert.ToDecimal(p.precioCompra);
                pc.precio = Convert.ToDecimal(p.precio);
                pc.creado = Convert.ToDateTime(p.creado);
                pc.modificado = Convert.ToDateTime(p.modificado);
                pc.observacion = p.observacion;
                pc.imagen = p.imagen;
                listaClon.Add(pc);
            }
            List<Categorias> listaCat = new List<Categorias>();
            listaCat = ctx.Categorias.ToList();
            List<CategoriasClon> listaClonCat = new List<CategoriasClon>();
            foreach (var c in listaCat)
            {
                CategoriasClon cc = new CategoriasClon();
                cc.idCategoria = c.idCategoria;
                cc.nombre = c.nombre;
                cc.descripcion = c.descripcion;
                cc.imagen = c.imagen;
                listaClonCat.Add(cc);
            }
           

            //creamos el report document
            ReportDocument repDocument = new ReportDocument();
            repDocument.Load(Path.Combine(Server.MapPath
                ("~/Reports/Productos"), "crProductosCategorias.rpt"));
       
           // le agregamos los dos datasource, uno para cada tabla
            repDocument.Database.Tables[0].SetDataSource(listaClon);
            repDocument.Database.Tables[1].SetDataSource(listaClonCat);

            //esto no se para que sea 
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = repDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "InformeProductosCategorias.pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lo sentimos, no se pudo exportar el informe " + ex.Message;
                return View("Index");
            }

            //return View();
        }

        //REPORTES DE PRODUCTOS
        public ActionResult TodosProductos()
        {
            return View(ctx.Productos.ToList());
        }
    }
}