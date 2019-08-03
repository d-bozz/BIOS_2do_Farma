using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    public interface IPersistenciaUsuario
    {
        void CambioContrasena(Usuario pUsuario, string newContrasena);
    }
}
