using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Administracion.ServicioFarma;

namespace Administracion
{
    public partial class MenuEncargado : Form
    {
        private Encargado encargadoLogueado;

        public MenuEncargado(Encargado usuLogin)
        {
            encargadoLogueado = usuLogin;
            InitializeComponent();
        }

        private void mantenimientoDeFarmaceuticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMFarmaceuticas formFarmaceuticas = new ABMFarmaceuticas(encargadoLogueado);
            formFarmaceuticas.ShowDialog();
        }

        private void mantenimientoDeMedicamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMMedicamentos formMedicamentos = new ABMMedicamentos(encargadoLogueado);
            formMedicamentos.ShowDialog();
        }

        private void altaEncargadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaEncargados formEncargados = new AltaEncargados(encargadoLogueado);
            formEncargados.ShowDialog();
        }

        private void mantenimientoDeEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMEmpleados formEmpleados = new ABMEmpleados(encargadoLogueado);
            formEmpleados.ShowDialog();
        }

        private void cambiarContrasenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevaContrasena formCambioContrasena = new NuevaContrasena(encargadoLogueado);
            formCambioContrasena.ShowDialog();
        }

        private void cambioDeEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioEstadoPedidos formCambioEstadoPedido = new CambioEstadoPedidos(encargadoLogueado);
            formCambioEstadoPedido.ShowDialog();
        }

        private void listadoDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadoPedidos formListadoPedidos = new ListadoPedidos(encargadoLogueado);
            formListadoPedidos.ShowDialog();
        }
    }
}
