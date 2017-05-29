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
        int noCotizacion();
        string ActualizarCotizacion(CotizarModel c);
        string EliminarCotizacion(int id);
    }
}
