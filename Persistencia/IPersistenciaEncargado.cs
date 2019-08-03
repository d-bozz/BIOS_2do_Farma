using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    public interface IPersistenciaEncargado
    {
        Encargado Login(string pNombreUsuario, string pContrasena);
        void AltaEncargado(Encargado nuevoEncargado, Encargado encargadoAutorizante);
        Encargado BuscarEncargados(int pci, Encargado unEncargado);
        //List<Encargado> ListarEncargados(Encargado unEncargado);
    }
}
