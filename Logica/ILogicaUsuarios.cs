using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades_Compartidas;

namespace Logica
{
    public interface ILogicaUsuarios
    {
        void AgregarUsuario(Usuario U, Encargado encargadoAutorizante);
        void EliminarUsuario(Usuario U, Encargado encargadoAutorizante);
        void ModificarUsuario(Usuario U, Encargado encargadoAutorizante);
        Usuario BuscarUsuario(int pci, Encargado encargadoAutorizante);
        void CambioContrasena(Usuario pUsuario, string newContrasena);
        Usuario Login(string pNombreUsuario, string pContrasena);
        void AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos);
        //List<Usuario> Listar(Encargado encargadoAutorizante);
        //List<Empleado> ListarActivos(Encargado encargadoAutorizante);

    }
}
