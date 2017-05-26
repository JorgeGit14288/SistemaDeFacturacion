using SistemaDeFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeFacturacion.Dao
{
    interface IProductosDao
    {
        bool Crear(Productos p);
        bool Actualizar(Productos p);
        bool Eliminar(Productos p);
        Productos BuscarId(string idProducto);
        Productos BuscarNombre(string nombre);
        List<Productos> Listar();
        List<Productos> ListarMenoresA(decimal cantidad);
        List<Productos> SinExistencia();
        List<Productos> MenoresA5();
    }
}
