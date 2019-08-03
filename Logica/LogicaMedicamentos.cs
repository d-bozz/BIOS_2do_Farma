using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades_Compartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaMedicamentos : ILogicaMedicamentos
    {

        //Singleton*************************************
        private static LogicaMedicamentos _instancia = null;

        private LogicaMedicamentos()
        {
        }

        public static LogicaMedicamentos GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaMedicamentos();
            return _instancia;
        }
        //************************************************

        //Operaciones
        public void AgregarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaMedicamentos().AgregarMedicamentos(M, E);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void EliminarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaMedicamentos().EliminarMedicamentos(M, E);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public void ModificarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaMedicamentos().ModificarMedicamentos(M, E);
            }
            catch (Exception ex)
            { throw ex; }
        }


        public Medicamento BuscarMedicamentosActivos(Farmaceutica pnombreF, int pcodigo, Usuario unUsuario)
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaMedicamentos().BuscarMedicamentosActivos(pnombreF, pcodigo, unUsuario));
        }

        public List<Medicamento> ListarMedicamentosActivos(Encargado unEncargado)
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaMedicamentos().ListarMedicamentosActivos(unEncargado));                    
        }

        public List<Medicamento> MedicamentosStock()
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaMedicamentos().MedicamentosStock());                            
        }
    }
}
