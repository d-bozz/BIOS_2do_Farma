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
    public partial class CambioEstadoPedidos : Form
    {
        private Encargado encargadoLogueado;
        private List<PedidoCabezal> pedidos = new List<PedidoCabezal>();

        public CambioEstadoPedidos(Encargado usuLogin)
        {
            InitializeComponent();
            encargadoLogueado = usuLogin;
        }

        private void CambioEstadoPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                Actualizar();
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(dataGridView1, ex.Message);
                lblError.Text = ex.Message;
            }
        }

        private void Actualizar()
        {
            pedidos = new ServicioFarma.ServicioWCFClient().ListaPedidosGeneradoEnviado(encargadoLogueado).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Numero");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("Estado");

            foreach (PedidoCabezal p in pedidos)
            {
                dt.Rows.Add(p.Numero, p.FecReal, p.DirEtga, p.UnEmp.NomCom, p.Estado);
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.SelectedRows.Count == 0)
                    throw new Exception("Seleccione al menos un pedido.");

                string nuevoEstado = "";
                switch (dataGridView1.SelectedRows[0].Cells["Estado"].Value.ToString().ToUpper())
                {
                    case "GENERADO":
                        {
                            nuevoEstado = "ENVIADO";
                            break;
                        }
                    case "ENVIADO":
                        {
                            nuevoEstado = "ENTREGADO";
                            break;
                        }
                    default:
                        {
                            throw new Exception("Estado invalido");
                        }
                }

                new ServicioWCFClient().CambioDeEstado(pedidos[dataGridView1.SelectedRows[0].Index], nuevoEstado, encargadoLogueado);

                Actualizar();
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(dataGridView1, ex.Message);
                lblError.Text = ex.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }
    }
}
