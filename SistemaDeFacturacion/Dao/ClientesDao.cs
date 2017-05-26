using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    public class ClientesDao : IClientesDao
    {
        FacturacionDbEntities ctx = new FacturacionDbEntities();
        public bool Actualizar(Clientes a)
        {
            throw new NotImplementedException();
        }

        public Clientes BuscarNit(string nit)
        {
            Clientes c = new Clientes();
            try
            {
                c = ctx.Clientes.Find(nit);
                return c;
            }
            catch
            {
                return c;
            }
        }
        public bool ExisteCliente(string nit)
        {
            try
            {
                Clientes c = new Clientes();
                c = ctx.Clientes.Find(nit);
                if(String.IsNullOrEmpty(nit))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            catch
            {
                return false;
            }

        }

        public Clientes BuscarNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool Crear(Clientes c)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(string nit)
        {
            throw new NotImplementedException();
        }

        public List<Clientes> Listar()
        {
            List<Clientes> lista = new List<Clientes>();
            try
            {
                lista = ctx.Clientes.ToList();
                return lista;
            }
            catch
            {
                return lista;
            }
        }
    }
}