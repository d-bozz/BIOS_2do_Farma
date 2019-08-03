using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entidades_Compartidas;

namespace ServicioFarmaWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioWCF
    {

        #region Farmaceutica
        [OperationContract]
        void AltaFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);

        [OperationContract]
        void EliminarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);
        
        [OperationContract]
        void ModificarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);

        [OperationContract]
        Farmaceutica BuscarFarmaceuticaActiva(string pnombre, Usuario unUsuario);

        [OperationContract]
        List<Farmaceutica> ListarFarmaceuticasActivas(Encargado unEncargado);


        #endregion

        #region Medicamentos
        [OperationContract]
        void AgregarMedicamentos(Medicamento M, Encargado E);

        [OperationContract]
        void EliminarMedicamentos(Medicamento M, Encargado E);

        [OperationContract]
        void ModificarMedicamentos(Medicamento M, Encargado E);

        [OperationContract]
        string MedicamentosStock();

        [OperationContract]
        Medicamento BuscarMedicamentosActivos(Farmaceutica pnombreF, int pcodigo, Usuario unUsuario);

        [OperationContract]
        List<Medicamento> ListarMedicamentosActivos(Encargado unEncargado);

        #endregion

        #region Pedidos

        [OperationContract]
        List<PedidoCabezal> ListaPedidosEsteAno(Encargado pEncargado);

        [OperationContract]
        List<PedidoCabezal> ListaPedidosGeneradoEnviado(Encargado pEncargado);

        [OperationContract]
        PedidoCabezal BuscarPedido(int pCodigo);

        [OperationContract]
        void AltaPedido(PedidoCabezal pPedido);

        [OperationContract]
        void CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E);

        #endregion

        #region Usuarios

        [OperationContract]
        void AgregarUsuario(Usuario U, Encargado encargadoAutorizante);

        [OperationContract]
        void EliminarUsuario(Usuario U, Encargado encargadoAutorizante);

        [OperationContract]
        void ModificarUsuario(Usuario U, Encargado encargadoAutorizante);

        [OperationContract]
        void CambioContrasena(Usuario pUsuario, string newContrasena);

        [OperationContract]
        Usuario Login(string pNombreUsuario, string pContrasena);

        [OperationContract]
        Usuario BuscarUsuariosActivos(int pci, Encargado encargadoAutorizante);

        [OperationContract]
        void AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos);
        #endregion
    }
}
