using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    internal class Conexion
    {
        public static string MiConexion(Usuario usuario)
        {
            return "Data Source = .; Initial Catalog = BiosFarma; User Id=" + usuario.NomUser +
                                                           ";Password =" + usuario.Contra + ";" ;
        }

        public const string MiConexionTrusted = "Data Source = .; Initial Catalog = BiosFarma; Integrated Security = True;";
    }
}
