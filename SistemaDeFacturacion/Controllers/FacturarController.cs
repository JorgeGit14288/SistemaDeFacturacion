using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using SistemaDeFacturacion.Dao;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using SistemaDeFacturacion.Models.CloneModel;

namespace SistemaDeFacturacion.Controllers
{
    [Authorize]
    public class FacturarController : Controller
    {
        IClientesDao daoCliente = new ClientesDao();
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        IFacturarDao daoFacturar = new FacturarDao();
        IFacturasDao daoFacturas = new FacturasDao();
        public ActionResult RealizarVenta(int? id=0)
        {
            if (id.ToString() == null)
            {
                ViewBag.Mensaje = " Bienvenido, ingrese el numero de cotizacion";
                return View("Index");
            }
            if(id==0)
            {
                ViewBag.Mensaje = " Bienvenido, ingrese el numero de cotizacion";
                return View("Index");
            }
            Cotizaciones coti = new Cotizaciones();
            List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
            List<TipoPago> tipoPagos = new List<TipoPago>();
                     
            tipoPagos = ctx.TipoPago.ToList();
            coti = ctx.Cotizaciones.Find(id);
            if (coti == null)
            {
                ViewBag.Error = "No Existe la cotizacion con ese id";
                return View("Index");
            }

            detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
            if(detalles.Count==0|| detalles==null)
            {
                ViewBag.Error = "La cotizacion no tiene productos a cotizar";
                return View("Index");
            }
            Facturas factura = new Facturas();
            factura.idFactura = daoFacturas.GetIdFactura();
            factura.nombre = coti.nombre;
            factura.nitCliente = coti.nitCliente;
            factura.direccion = coti.direccion;
            factura.total = coti.total;
            ViewBag.Factura = factura;
            ViewBag.TipoPago = tipoPagos;
            ViewBag.Detalles = detalles;
            ViewBag.Cotizacion = coti;
            return View(coti);
        }
        [HttpPost]
        public ActionResult RealizarVenta(FormCollection form, int id)
        {
            try
            {            
                if (id == 0)
                {
                    ViewBag.Mensaje = " Bienvenido, ingrese el numero de cotizacion";
                    return View("Index");
                }
                Cotizaciones coti = new Cotizaciones();               
                coti = ctx.Cotizaciones.Find(id);
                Facturas factura = new Facturas();
                factura.idFactura = daoFacturas.GetIdFactura();
                factura.nombre = coti.nombre;
                factura.nitCliente = coti.nitCliente;
                factura.direccion = coti.direccion;
                factura.total = coti.total;
                ViewBag.Factura = factura;
                string resultado = daoFacturar.RealizarVenta(id);
                if (resultado == "ok")
                {
                    ViewBag.Mensaje = "Venta Registrada con exito";
                }
                else
                {
                    ViewBag.Error = resultado;
                }
                Cotizaciones cotiz = new Cotizaciones();
                List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                cotiz = ctx.Cotizaciones.Find(id);              
                detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
                List<TipoPago> tipoPagos = new List<TipoPago>();
                cotiz.estado = "Vendido";
                tipoPagos = ctx.TipoPago.ToList();
                ViewBag.Factura = factura;
                ViewBag.TipoPago = tipoPagos;
                ViewBag.Detalles = detalles;
                ViewBag.Cotizacion = cotiz;
                return View(coti);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(id);
            }           
        }
        [HttpPost]
        public ActionResult FacturarVenta(FormCollection form, int id)
        {
            try
            {
                Cotizaciones cotiz = new Cotizaciones();
                cotiz = ctx.Cotizaciones.Find(id);
                //creamos los datos de la factura
                Facturas factura = new Facturas();
                factura.idFactura = Convert.ToInt32(form["idFactura"]);
                factura.nitCliente = form["nitCliente"];
                factura.nombre = form["nombre"];
                factura.direccion = form["direccion"];
                factura.idCotizacion = Convert.ToInt32(form["idCotizacion"]);
                factura.usuario = Session["Usuario"].ToString();
                factura.fecha = DateTime.Now.Date;
                factura.total = cotiz.total;
                factura.descuento = cotiz.descuento;
                factura.subTotal = cotiz.subTotal;
                factura.tipoPago = Convert.ToInt32(form["tipoPago"]);
                factura.idPago = form["idPago"];
                if (cotiz.estado == "Facturado")
                {
                    ViewBag.Error = "La cotizacion ya ha sido facturada ";
                    //ViewBag.Mensaje = "Venta Registrada con exito";
                    Cotizaciones coti = new Cotizaciones();
                    List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                    coti = ctx.Cotizaciones.Find(id);
                    detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
                    //  factura = ctx.Facturas.SingleOrDefault(f => f.idCotizacion == coti.idCotizacion);
                    List<TipoPago> tipoPagos = new List<TipoPago>();
                    tipoPagos = ctx.TipoPago.ToList();
                    ViewBag.Factura = factura;
                    ViewBag.TipoPago = tipoPagos;
                    ViewBag.Detalles = detalles;
                    ViewBag.Cotizacion = coti;
                    return View("RealizarVenta", coti);
                }

                
                // creamos los datos del cliente
                Clientes cliente = new Clientes();
                cliente.nit = factura.nitCliente;
                cliente.nombre = factura.nombre;
                cliente.direccion = factura.direccion;
                // creamos el objeto a facturar
                Facturar datosFacturar = new Facturar();
                datosFacturar.factura = factura;
                datosFacturar.idCotizacion = cotiz.idCotizacion;
                datosFacturar.cliente = cliente;

                string resultado = daoFacturar.FacturarVenta(datosFacturar);
                if (resultado == "ok")
                {
                    ViewBag.Mensaje = "VENTA FACTURADA CON EXITO";
                    Cotizaciones cotizacion = new Cotizaciones();
                    List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                    cotizacion = ctx.Cotizaciones.Find(id);
                    cotizacion.estado = "Facturado";
                    detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
                    //  factura = ctx.Facturas.SingleOrDefault(f => f.idCotizacion == coti.idCotizacion);
                    List<TipoPago> tipoPagos = new List<TipoPago>();
                    tipoPagos = ctx.TipoPago.ToList();
                    factura = new Facturas();
                    factura = ctx.Facturas.SingleOrDefault(f => f.idCotizacion == cotizacion.idCotizacion);
                    ViewBag.Factura = factura;
                    ViewBag.TipoPago = tipoPagos;
                    ViewBag.Detalles = detalles;
                    ViewBag.Cotizacion = null;
                    ViewBag.Cotizacion = cotizacion;
                    return View("RealizarVenta", cotizacion);
                }
                else
                {
                    ViewBag.Error = resultado;
                    Cotizaciones coti = new Cotizaciones();
                    List<DetallesCotizacion> detalles = new List<DetallesCotizacion>();
                    coti = ctx.Cotizaciones.Find(id);
                    detalles = ctx.DetallesCotizacion.Where(r => r.idCotizacion == id).ToList();
                    //  factura = ctx.Facturas.SingleOrDefault(f => f.idCotizacion == coti.idCotizacion);
                    List<TipoPago> tipoPagos = new List<TipoPago>();
                    tipoPagos = ctx.TipoPago.ToList();
                    ViewBag.Factura = factura;
                    ViewBag.TipoPago = tipoPagos;
                    ViewBag.Detalles = detalles;
                    ViewBag.Cotizacion = coti;
                    return View("RealizarVenta",coti);
                }               
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(id);
            }
        }
        // GET: Facturar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facturar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }    
        // GET: Facturar/Create
        public ActionResult Create()
        {
            return View();
        }    
        // POST: Facturar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Facturar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Facturar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Facturar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Facturar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Exportarcotizacion(int idCotizacion)
        {//reporte que exporta todos los productos
            //creamos la lista para el origen de datos
         
           
            Cotizaciones coti = new Cotizaciones();
            coti = ctx.Cotizaciones.Find(idCotizacion);
            CotizacionClon clonCoti = new CotizacionClon();
            clonCoti.idCotizacion = coti.idCotizacion;
            clonCoti.estado = coti.estado;
            clonCoti.fecha = coti.fecha;
            clonCoti.direccion = coti.direccion;
            clonCoti.nitCliente = coti.nitCliente;
            clonCoti.subTotal = Convert.ToDecimal( coti.subTotal);
            clonCoti.descuento =Convert.ToDecimal( coti.descuento);
            clonCoti.usuario = coti.usuario;
            clonCoti.nombre = coti.nombre;
            clonCoti.total = Convert.ToDecimal(coti.total);

            List<CotizacionClon> cotizaciones = new List<CotizacionClon>();
            cotizaciones.Add(clonCoti);

            List<DetallesCotizacion> detalles1 = new List<DetallesCotizacion>();
            List<DetallesCotizacionClon> detalles = new List<DetallesCotizacionClon>();

            detalles1 = ctx.DetallesCotizacion.Where(d => d.idCotizacion == idCotizacion).ToList();
            foreach (var d in detalles1)
            {
                DetallesCotizacionClon dc = new DetallesCotizacionClon();
                dc.idCotizacion = d.idCotizacion;
                dc.idDetalle = d.idDetalle;
                dc.idProducto = d.idProducto;
                dc.cantidad = Convert.ToDecimal(d.cantidad);
                dc.precio = Convert.ToDecimal(d.precio);
                dc.subTotal = Convert.ToDecimal(d.subTotal);
                dc.descripcion = d.descripcion;
                dc.descuento = Convert.ToDecimal(d.descuento);

                detalles.Add(dc);
            }


            //creamos el report document
            ReportDocument repDocument = new ReportDocument();
            repDocument.Load(Path.Combine(Server.MapPath
                ("~/Reports/Cotizaciones"), "crCotizacion.rpt"));
            // le agregamos los dos datasource, uno para cada tabla
            repDocument.Database.Tables[0].SetDataSource(cotizaciones);
            repDocument.Database.Tables[1].SetDataSource(detalles);

            //esto no se para que sea 
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = repDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Cotizacion.pdf");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lo sentimos, no se pudo exportar el informe " + ex.Message;
                return View("RealizarVenta", new { id = idCotizacion });
            }
        }
    }
}
