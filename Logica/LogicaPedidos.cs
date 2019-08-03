using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;

using Entidades_Compartidas;
namespace Logica
{
    internal class LogicaPedidos : ILogicaPedidos
    {
        #region Singleton
        private static LogicaPedidos _instancia = null;

        private LogicaPedidos()
        {
        }

        public static LogicaPedidos GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaPedidos();
            return _instancia;
        }
        #endregion

        public List<PedidoCabezal> ListaPedidosEsteAno(Encargado pEncargado)
        {
            try
            {
                return FabricaPersistencia.GetPersistenciaPedidoCabezal().ListaPedidosEsteAno(pEncargado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PedidoCabezal> ListaPedidosGeneradoEnviado(Encargado pEncargado)
        {
            try
            {
                return FabricaPersistencia.GetPersistenciaPedidoCabezal().ListaPedidosGeneradoEnviado(pEncargado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PedidoCabezal BuscarPedido(int pCodigo)
        {
            PedidoCabezal _unPedido = null;
            try
            {
                _unPedido = FabricaPersistencia.GetPersistenciaPedidoCabezal().BuscarPedido(pCodigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _unPedido;
        }

        //El usuario ya se encuentra dentro del pedido.
        public void AltaPedido(PedidoCabezal pPedido)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaPedidoCabezal().AltaPedido(pPedido);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        public void CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaPedidoCabezal().CambioDeEstado(pPedido, pEstadoNuevo, E);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

    }
}
