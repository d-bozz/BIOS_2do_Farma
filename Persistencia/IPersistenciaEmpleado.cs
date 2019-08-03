using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    public interface IPersistenciaEmpleado
    {
        Empleado Login(string pNombreUsuario, string pContrasena);
        void AltaEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante);
        void ModificarEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante);
        void EliminarEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante);
        Empleado BuscarEmpleadosActivos(int pci, Encargado encargadoAutorizante);
        void AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos);
        //List<Empleado> ListadoEmpleados(Encargado encargadoAutorizante);
        //List<Empleado> ListadoEmpleadosActivos(Encargado encargadoAutorizante);
    }
}
