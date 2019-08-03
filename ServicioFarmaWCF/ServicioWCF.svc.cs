using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Entidades_Compartidas;
using Logica;
using System.Xml;

namespace ServicioFarmaWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioWCF" en el código, en svc y en el archivo de configuración a la vez.
    public class ServicioWCF : IServicioWCF
    {
        //public Encargado ParaPoderSerializarEncargado()
        //{
        //    return new Encargado();
        //}

        //public Empleado ParaPoderSerializarEmpleado()
        //{
        //    return new Empleado();
        //}

        #region Farmaceutica

        void IServicioWCF.AltaFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaLogica.getLogicaFarmaceuticas().AltaFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
 
        }

        void IServicioWCF.EliminarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaLogica.getLogicaFarmaceuticas().EliminarFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        void IServicioWCF.ModificarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            try
            {
                FabricaLogica.getLogicaFarmaceuticas().ModificarFarmaceutica(unaFarmaceutica, unEncargado);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        Farmaceutica IServicioWCF.BuscarFarmaceuticaActiva(string pnombre, Usuario unUsuario)
        {
            try
            {
                return (FabricaLogica.getLogicaFarmaceuticas().BuscarFarmaceuticaActiva(pnombre, unUsuario));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        List<Farmaceutica> IServicioWCF.ListarFarmaceuticasActivas(Encargado unEncargado)
        {
            try
            {
                return (FabricaLogica.getLogicaFarmaceuticas().ListarFarmaceuticasActivas(unEncargado));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 

        #endregion

        #region Medicamentos
        void IServicioWCF.AgregarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaLogica.getLogicaMedicamentos().AgregarMedicamentos(M, E);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.EliminarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaLogica.getLogicaMedicamentos().EliminarMedicamentos(M, E);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.ModificarMedicamentos(Medicamento M, Encargado E)
        {
            try
            {
                FabricaLogica.getLogicaMedicamentos().ModificarMedicamentos(M, E);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string IServicioWCF.MedicamentosStock()
        {
            try
            {
                List<Medicamento> listaMedicamentos = FabricaLogica.getLogicaMedicamentos().MedicamentosStock();

                XmlDocument _documentoXML = new XmlDocument();
                XmlNode _NodoPPal = _documentoXML.CreateNode(XmlNodeType.Element, "Medicamentos", "");

                //Cargo datos de Medicamentos dentro del XML
                foreach (Medicamento medicamento in listaMedicamentos)
                {
                    XmlNode _NodoM = _documentoXML.CreateNode(XmlNodeType.Element, "Medicamento", "");

                    #region Farmaceutica
                    XmlNode _NodoF = _documentoXML.CreateNode(XmlNodeType.Element, "Farmaceutica", "");

                    XmlNode _NodoNomFar = _documentoXML.CreateNode(XmlNodeType.Element, "NombreFarmaceutica", "");
                    _NodoNomFar.InnerText = medicamento.UnaFarmaceutica.NomFar;
                    _NodoF.AppendChild(_NodoNomFar);

                    XmlNode _NodoDirFar = _documentoXML.CreateNode(XmlNodeType.Element, "DireccionFisica", "");
                    _NodoDirFar.InnerText = medicamento.UnaFarmaceutica.DirFisc;
                    _NodoF.AppendChild(_NodoDirFar);

                    XmlNode _NodoTelFar = _documentoXML.CreateNode(XmlNodeType.Element, "Telefono", "");
                    _NodoTelFar.InnerText = medicamento.UnaFarmaceutica.Telefono.ToString();
                    _NodoF.AppendChild(_NodoTelFar);

                    XmlNode _NodoMailFar = _documentoXML.CreateNode(XmlNodeType.Element, "Correo", "");
                    _NodoMailFar.InnerText = medicamento.UnaFarmaceutica.Correo;
                    _NodoF.AppendChild(_NodoMailFar);

                    _NodoM.AppendChild(_NodoF);
                    #endregion

                    XmlNode _NodoCod = _documentoXML.CreateNode(XmlNodeType.Element, "Codigo", "");
                    _NodoCod.InnerText = medicamento.Codigo.ToString();
                    _NodoM.AppendChild(_NodoCod);

                    XmlNode _NodoNom = _documentoXML.CreateNode(XmlNodeType.Element, "NombreMedicamento", "");
                    _NodoNom.InnerText = medicamento.NomMed;
                    _NodoM.AppendChild(_NodoNom);

                    XmlNode _NodoDesc = _documentoXML.CreateNode(XmlNodeType.Element, "Descripcion", "");
                    _NodoDesc.InnerText = medicamento.Desc;
                    _NodoM.AppendChild(_NodoDesc);

                    XmlNode _NodoPrecio = _documentoXML.CreateNode(XmlNodeType.Element, "Precio", "");
                    _NodoPrecio.InnerText = medicamento.Precio.ToString();
                    _NodoM.AppendChild(_NodoPrecio);

                    XmlNode _NodoTipo = _documentoXML.CreateNode(XmlNodeType.Element, "Tipo", "");
                    _NodoTipo.InnerText = medicamento.Tipo;
                    _NodoM.AppendChild(_NodoTipo);

                    XmlNode _NodoStock = _documentoXML.CreateNode(XmlNodeType.Element, "Stock", "");
                    _NodoStock.InnerText = medicamento.Stock.ToString();
                    _NodoM.AppendChild(_NodoStock);

                    _NodoPPal.AppendChild(_NodoM);
                }

                _documentoXML.AppendChild(_NodoPPal);


                return _documentoXML.OuterXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Medicamento IServicioWCF.BuscarMedicamentosActivos(Farmaceutica pnombreF, int pcodigo, Usuario unUsuario)
        {
            try
            {
                return (FabricaLogica.getLogicaMedicamentos().BuscarMedicamentosActivos(pnombreF, pcodigo, unUsuario));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<Medicamento> IServicioWCF.ListarMedicamentosActivos(Encargado unEncargado)
        {
            try
            {
                return (FabricaLogica.getLogicaMedicamentos().ListarMedicamentosActivos(unEncargado));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region Pedidos

        List<PedidoCabezal> IServicioWCF.ListaPedidosEsteAno(Encargado pEncargado) 
        {
            try
            {
                return FabricaLogica.getLogicaPedidos().ListaPedidosEsteAno(pEncargado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<PedidoCabezal> IServicioWCF.ListaPedidosGeneradoEnviado(Encargado pEncargado)
        {
            try
            {
                return FabricaLogica.getLogicaPedidos().ListaPedidosGeneradoEnviado(pEncargado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        PedidoCabezal IServicioWCF.BuscarPedido(int pCodigo) 
        {
            try
            {
                return FabricaLogica.getLogicaPedidos().BuscarPedido(pCodigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.AltaPedido(PedidoCabezal pPedido)
        {
            try
            {
                FabricaLogica.getLogicaPedidos().AltaPedido(pPedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E)
        {
            try
            {
                FabricaLogica.getLogicaPedidos().CambioDeEstado(pPedido, pEstadoNuevo, E);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Usuarios

        void IServicioWCF.AgregarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                FabricaLogica.getLogicaUsuarios().AgregarUsuario(U, encargadoAutorizante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.EliminarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                FabricaLogica.getLogicaUsuarios().EliminarUsuario(U, encargadoAutorizante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.ModificarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                FabricaLogica.getLogicaUsuarios().ModificarUsuario(U, encargadoAutorizante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IServicioWCF.CambioContrasena(Usuario pUsuario, string newContrasena)
        {
            try
            {
                FabricaLogica.getLogicaUsuarios().CambioContrasena(pUsuario, newContrasena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Usuario IServicioWCF.BuscarUsuariosActivos(int pci, Encargado encargadoAutorizante)
        {
            try
            {
                return (FabricaLogica.getLogicaUsuarios().BuscarUsuario(pci, encargadoAutorizante));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Usuario IServicioWCF.Login(string pNombreUsuario, string pContrasena)
        {
            Usuario usuLogueando = null;
            try
            {
                usuLogueando = FabricaLogica.getLogicaUsuarios().Login(pNombreUsuario, pContrasena);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return usuLogueando;
        }

        void IServicioWCF.AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos)
        {
            try
            {
                FabricaLogica.getLogicaUsuarios().AgregaHorasExtras(pCi, pFecha, pMinutos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
