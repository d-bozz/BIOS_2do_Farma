using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades_Compartidas;
namespace Logica
{
    public interface ILogicaPedidos
    {
        List<PedidoCabezal> ListaPedidosEsteAno(Encargado pEncargado);

        List<PedidoCabezal> ListaPedidosGeneradoEnviado(Encargado pEncargado);

        PedidoCabezal BuscarPedido(int pCodigo);

        void AltaPedido(PedidoCabezal pPedido);

        void CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E);
    }

}
