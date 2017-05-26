using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    interface IComprasDao
    {
        bool Crear(Compras c);
        bool Actualizar(Compras c);
        Compras BuscarId(int id);
        List<Compras> Listar();
        int ObtenerIdActual();
    }
}
