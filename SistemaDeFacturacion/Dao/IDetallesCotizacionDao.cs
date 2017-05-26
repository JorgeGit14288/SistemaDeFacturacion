using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    interface IDetallesCotizacionDao
    {
        bool Crear(DetallesCotizacion d);
        bool Actualizar(DetallesCotizacion d);
        bool Eliminar(int idCotizacion, int idDetalle);
        DetallesCotizacion Buscar(int idCotizacion, int idDetalle);
        List<DetallesCotizacion> DetallesFactura(int idCotizacion);
        bool ExisteDetalle(int idCotizacion, int idDetalle);

    }
}
