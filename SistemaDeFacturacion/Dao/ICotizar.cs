using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    interface ICotizarDao
    {
        string Cotizar(CotizarModel c);
        
        string DetallesCotizacion(int idCotizacion);
        string ActualizarCotizacion(CotizarModel c);
        int noCotizacion();
        string EliminarCotizacion(int id);
        string CrearDetalle(DetallesCotizacion detalle);
        string ModificarDetalle(DetallesCotizacion detalle);
        string EliminarDetalle(DetallesCotizacion detalle);
        string ActualizarTotal(decimal total, int idCotizacion);
    }
}
