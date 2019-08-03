using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades_Compartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaFarmaceuticas : ILogicaFarmaceuticas
    {
        //Singleton*************************************
        private static LogicaFarmaceuticas _instancia = null;

        private LogicaFarmaceuticas()
        {
        }

        public static LogicaFarmaceuticas GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaFarmaceuticas();
            return _instancia;
        }
        //************************************************

        //Operaciones
        public void AltaFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaFarmaceutica().AltaFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void EliminarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaFarmaceutica().EliminarFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void ModificarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaFarmaceutica().ModificarFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        public Farmaceutica BuscarFarmaceuticaActiva(string pnombre, Usuario unUsuario)
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaFarmaceutica().BuscarFarmaceuticaActiva(pnombre, unUsuario));
        }
        
        public List<Farmaceutica> ListarFarmaceuticasActivas(Encargado unEncargado)
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaFarmaceutica().ListarFarmaceuticasActivas(unEncargado));        
        }

        
    }
}
