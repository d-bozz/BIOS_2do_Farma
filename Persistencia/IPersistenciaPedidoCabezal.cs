using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    public interface IPersistenciaPedidoCabezal
    {
        List<PedidoCabezal> ListaPedidosEsteAno(Encargado encargado);

        List<PedidoCabezal> ListaPedidosGeneradoEnviado(Encargado pEncargado);

        PedidoCabezal BuscarPedido(int pCodigo);

        //Aca no paso un Empleado porque ya esta adentro del Pedido
        void AltaPedido(PedidoCabezal pPedido);

        void CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E);
    }
}
