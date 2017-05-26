using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeFacturacion.Models;

namespace SistemaDeFacturacion.Dao
{
    interface IAgregarComprasDao
    {
        bool Crear(mCompras mc);
        mCompras BuscarCompra(int idCompra);

    }
}
