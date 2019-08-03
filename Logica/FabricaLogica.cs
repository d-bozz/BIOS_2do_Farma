using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaUsuarios getLogicaUsuarios()
        {
            return LogicaUsuarios.GetInstance();
        }

        public static ILogicaFarmaceuticas getLogicaFarmaceuticas()
        {
            return LogicaFarmaceuticas.GetInstance();
        }

        public static ILogicaMedicamentos getLogicaMedicamentos()
        {
            return LogicaMedicamentos.GetInstance();
        }
        
        public static ILogicaPedidos getLogicaPedidos()
        {
            return LogicaPedidos.GetInstance();
        }
    }
}
