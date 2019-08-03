using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Administracion
{
    public class Constantes
    {
        static string workingDir = Directory.GetCurrentDirectory();
        //public static string xmlPath = Directory.GetParent(workingDir).Parent.FullName + "\\sesion.xml";
        public static string xmlPath = workingDir + "\\sesion.xml";
    }
}
