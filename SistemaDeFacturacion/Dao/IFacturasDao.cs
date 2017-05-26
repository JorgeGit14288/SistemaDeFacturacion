using SistemaDeFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeFacturacion.Dao
{
   public interface IFacturasDao
    {
        bool Crear(Facturas f);
        bool Actualizar(Facturas a);
        bool Eliminar(int id);
        List<Facturas> Listar();
        List<DetallesCotizacion> ListarDetalles(int id);
        Facturas BuscarId();
        List<Facturas> ListarFacturasHoy(DateTime fecha);
        List<Facturas> ListarFacturasFechas(DateTime fechaInicio, DateTime fechaFin);
        int GetIdFactura();
    }
}
