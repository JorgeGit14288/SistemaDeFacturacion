using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;
using Microsoft.AspNet.Identity;

namespace SistemaDeFacturacion.Dao.Helpers
{
    public class IniciarEntidades
    {
        FacturacionDbEntities db = new FacturacionDbEntities();
        public void CrearEntidades()
        {
            //crea el primer tipo de pago
            if (db.TipoPago.Count() == 0)
            {
                TipoPago tp = new TipoPago();
                tp.id = 1;
                tp.nombre = "Efectivo";
                tp.descripcion = "Pago en efectivo";
                db.TipoPago.Add(tp);
                db.SaveChanges();
                TipoPago tp2 = new TipoPago();
                tp2.id = 2;
                tp2.nombre = "Tarjeta Credigo";
                tp2.descripcion = "Pago con tarjeta de credito";
                db.TipoPago.Add(tp2);
                db.SaveChanges();
                TipoPago tp3 = new TipoPago();
                tp3.id = 3;
                tp3.nombre = "Tarjeta Debito";
                tp3.descripcion = "Pago con tarjeta de debito";
                db.TipoPago.Add(tp3);
                db.SaveChanges();
                TipoPago tp4 = new TipoPago();
                tp.id = 4;
                tp.nombre = "Otro";
                tp.descripcion = "Especifique en el campo id, el detalle del pago";
                db.TipoPago.Add(tp);
                db.SaveChanges();
            }
            //crea la primera categoria de producos
            if(db.Categorias.Count()==0)
            {
                Categorias c = new Categorias();
                c.idCategoria = 1;
                c.nombre = "General";
                c.descripcion = "Productos sin clasificar";
                db.Categorias.Add(c);
                db.SaveChanges();

            }
            //crear los roles
            if(db.AspNetRoles.Count()==0)
            {
                AspNetRoles r1 = new AspNetRoles();
                AspNetRoles r2 = new AspNetRoles();
                AspNetRoles r3 = new AspNetRoles();
                AspNetRoles r4 = new AspNetRoles();

                r1.Id = "1";
                r1.Name = "Administrador";
                db.AspNetRoles.Add(r1);
                r2.Id = "2";
                r2.Name = "Ventas";
                db.AspNetRoles.Add(r2);
                r3.Id = "3";
                r3.Name = "Bodega";
                db.AspNetRoles.Add(r3);
                r4.Id = "4";
                r4.Name = "Reportes";
                db.AspNetRoles.Add(r4);
                db.SaveChanges();
            }
        }
    }
    
}