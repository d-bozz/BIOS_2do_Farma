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
    public partial class MenuEmpleado : Form
    {
        private Empleado empleadoLogueado;

        public MenuEmpleado(Empleado usuLogin)
        {
            empleadoLogueado = usuLogin;
            InitializeComponent();
        }

        private void MenuEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void cambioDeContrasenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevaContrasena formCambioContrasena = new NuevaContrasena(empleadoLogueado);
            formCambioContrasena.ShowDialog();
        }

        private void crearPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaPedidos formAltaPedidos = new AltaPedidos(empleadoLogueado);
            formAltaPedidos.ShowDialog();
        }

        private void MenuEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(Constantes.xmlPath))
                File.Delete(Constantes.xmlPath);
        }
    }
}
