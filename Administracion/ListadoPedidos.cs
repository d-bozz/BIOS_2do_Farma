using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administracion.ServicioFarma;

namespace Administracion
{
    public partial class ListadoPedidos : Form
    {
        private Encargado encargadoLogueado;
        private List<PedidoCabezal> listaPedidos;

        public ListadoPedidos(Encargado usuLogin)
        {
            InitializeComponent();
            encargadoLogueado = usuLogin;
        }

        private void ListadoPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                IServicioWCF servicioFarma = new ServicioWCFClient();
                listaPedidos = servicioFarma.ListaPedidosEsteAno(encargadoLogueado).ToList();

                ListadoInicial();


            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void ListadoInicial()
        {
            gvResultado.DataSource = null;
            errorProvider.Clear();

            var resultado = (from unPedido in listaPedidos
                             select new
                             {
                                 Numero = unPedido.Numero,
                                 Fecha_Pedido = unPedido.FecReal,
                                 Direccion_de_Entrega = unPedido.DirEtga,
                                 Estado = unPedido.Estado,
                                 Nombre_de_Empleado = unPedido.UnEmp.NomCom
                             }).ToList();

            gvResultado.DataSource = resultado;
        }

        private void btnResumenEmpleado_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            try
            {
                gvResultado.DataSource = null;

                var resultado = (from unPedido in listaPedidos
                                 group unPedido by unPedido.UnEmp.NomCom into grupo
                                 select new
                                 {
                                     Nombre_de_Empleado = grupo.Key,
                                     Cantidad_de_Pedidos = grupo.Count()
                                 }).ToList();

                gvResultado.DataSource = resultado;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            try
            {
                ListadoInicial();
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnResumenMedicamento_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            try
            {
                List<PedidosLinea> listaLineas = new List<PedidosLinea>();
                gvResultado.DataSource = null;

                var resultado = (from unPedido in listaPedidos
                                  from unaLinea in unPedido.Lineas
                                  group unaLinea by unaLinea.UnMedicamento.NomMed into grupo
                                  select new
                                  {
                                      Nombre_de_Medicamento = grupo.Key,
                                      Veces_Pedido = grupo.Sum(x => x.Cant)
                                  }).ToList();

                gvResultado.DataSource = resultado;
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnResumenFarmaceutica_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            try
            {
                List<PedidosLinea> listaLineas = new List<PedidosLinea>();

                if (txtFarma.Text.Trim() == "")
                    throw new Exception("Ingrese una Farmaceutica.");

                Farmaceutica unaFarmaceutica = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarma.Text, encargadoLogueado);

                if (unaFarmaceutica == null)
                    throw new Exception(txtFarma.Text + " no es una Farmaceutica.");

                gvResultado.DataSource = null;


                var resultado = (from unPedido in listaPedidos
                                  from unaLinea in unPedido.Lineas
                                  where unaLinea.UnMedicamento.UnaFarmaceutica.NomFar.Equals(unaFarmaceutica.NomFar)
                                  orderby unPedido.FecReal, unaLinea.UnMedicamento.NomMed, unaLinea.Cant
                                  select new
                                  {
                                      Numero = unPedido.Numero,
                                      Fecha_Pedido = unPedido.FecReal,
                                      Direccion_de_Entrega = unPedido.DirEtga,
                                      Estado = unPedido.Estado,
                                      Nombre_de_Empleado = unPedido.UnEmp.NomCom,
                                      Nombre_Medicamento = unaLinea.UnMedicamento.NomMed,
                                      Cantidad = unaLinea.Cant
                                  }).ToList();

                gvResultado.DataSource = resultado;

            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtFarma_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();
            try
            {
                if (txtFarma.Text.Trim() == "")
                    throw new Exception("Ingrese una Farmaceutica.");

            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtFarma, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
