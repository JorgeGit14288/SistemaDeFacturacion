using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    public class ComprasDao : IComprasDao
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public bool Actualizar(Compras c)
        {
            throw new NotImplementedException();
        }

        public Compras BuscarId(int id)
        {
            Compras c = new Compras();
            try
            {
                c = ctx.Compras.Find(id);
                return c;

            }
            catch
            {
                return c;
            }
        }

        public bool Crear(Compras c)
        {
            throw new NotImplementedException();
        }

        public List<Compras> Listar()
        {
            List<Compras> lista = ctx.Compras.ToList();
            return lista;
        }
        //metodo que devuelve el numero de compra a ingresar
        public int ObtenerIdActual()
        {
            int id = 0;
            try
            {
                System.Nullable<Int32> maxIdFactura =
                (from c in ctx.Compras
                select c.idCompra)
                .Max();
                return (Convert.ToInt32(maxIdFactura) + 1);
            }
            catch
            {
                return id;
            }
        }
    }
}