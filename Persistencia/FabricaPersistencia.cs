using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaMedicamentos GetPersistenciaMedicamentos()
        {
            return PersistenciaMedicamentos.GetInstance(); 
        }
        
        public static IPersistenciaPedidoCabezal GetPersistenciaPedidoCabezal()
        {
            return PersistenciaPedidoCabezal.GetInstance();
        }

        public static IPersistenciaFarmaceutica GetPersistenciaFarmaceutica()
        {
            return PersistenciaFarmaceutica.GetInstance();
        }

        public static IPersistenciaEmpleado GetPersistenciaEmpleado()
        {
            return PersistenciaEmpleado.GetInstance();
        }

        public static IPersistenciaEncargado GetPersistenciaEncargado()
        {
            return PersistenciaEncargado.GetInstance();
        }

        public static IPersistenciaUsuario GetPersistenciaUsuario()
        {
            return PersistenciaUsuario.GetInstance();
        } 
        
    }
}
