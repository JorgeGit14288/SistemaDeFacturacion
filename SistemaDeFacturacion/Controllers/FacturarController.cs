using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeFacturacion.Models;
using SistemaDeFacturacion.Dao;
using System.ComponentModel;

namespace SistemaDeFacturacion.Controllers
{
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
                factura = new Facturas();
                factura = ctx.Facturas.SingleOrDefault(f => f.idCotizacion == coti.idCotizacion);
                List<TipoPago> tipoPagos = new List<TipoPago>();
                tipoPagos = ctx.TipoPago.ToList();
                ViewBag.Factura = factura;
                ViewBag.TipoPago = tipoPagos;
                ViewBag.Detalles = detalles;
                ViewBag.Cotizacion = coti;
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
                    ViewBag.Mensaje = "Venta Registrada con exito";
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
    }
}
