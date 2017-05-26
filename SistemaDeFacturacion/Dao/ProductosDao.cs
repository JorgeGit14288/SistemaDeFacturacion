using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    public class ProductosDao : IProductosDao
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public bool Actualizar(Productos p)
        {
            throw new NotImplementedException();
        }

        public Productos BuscarId(string idProducto)
        {
            Productos p = new Productos();
            try
            {
                p = ctx.Productos.First(r => (r.idProducto == idProducto));
               // p = ctx.Productos.Find(idProducto);
                return p;
            }
            catch
            {
                return p;
            }
        }

        public Productos BuscarNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool Crear(Productos p)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Productos p)
        {
            throw new NotImplementedException();
        }

        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                lista = ctx.Productos.ToList();
                return lista;
            }
            catch
            {
                return lista;
            }
        }

        public List<Productos> ListarMenoresA(decimal cantidad)
        {
            throw new NotImplementedException();
        }

        public List<Productos> MenoresA5()
        {
            throw new NotImplementedException();
        }

        public List<Productos> SinExistencia()
        {
            throw new NotImplementedException();
        }
    }
}