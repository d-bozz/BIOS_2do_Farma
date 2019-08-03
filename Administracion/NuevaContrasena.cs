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
    public partial class NuevaContrasena : Form
    {
        private Usuario usuarioLogueado;
        
        public NuevaContrasena(Usuario usuLogin)
        {
            InitializeComponent();
            usuarioLogueado = usuLogin;
        }

        public void EstadoInicial()
        {
            txtActual.Show();
            lblActual.Show();
            btnSiguiente.Show();
            txtNueva.Hide();
            lblNueva.Hide();
            btnAceptar.Hide();
            txtActual.Text = "";
            txtNueva.Text = "";
            txtActual.Focus();
        }

        private void EstadoCambiar()
        {
            txtActual.Hide();
            lblActual.Hide();
            btnSiguiente.Hide();
            txtNueva.Show();
            lblNueva.Show();
            btnAceptar.Show();
            txtNueva.Focus();
            lblError.Text = "";
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioLogueado.Contra != txtActual.Text)
                    throw new Exception("La contrasena ingresada no coincide con la actual.");

                EstadoCambiar();
            }
            catch ( Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                IServicioWCF servicioFarma = new ServicioWCFClient();
                servicioFarma.CambioContrasena(usuarioLogueado, txtNueva.Text);
                usuarioLogueado.Contra = txtNueva.Text;
                lblError.Text = "Contrasena cambiada correctamente.";
                EstadoInicial();
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void NuevaContrasena_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            EstadoInicial();
        }

        //Validaciones
        private void txtActual_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblError.Text = "";
                if (txtActual.Text != usuarioLogueado.Contra)
                    throw new Exception("La contrasena no coincide con la actual.");

                string letras = txtActual.Text;
                letras = letras.Replace("0", "");
                letras = letras.Replace("1", "");
                letras = letras.Replace("2", "");
                letras = letras.Replace("3", "");
                letras = letras.Replace("4", "");
                letras = letras.Replace("5", "");
                letras = letras.Replace("6", "");
                letras = letras.Replace("7", "");
                letras = letras.Replace("8", "");
                letras = letras.Replace("9", "");

                if (letras.Length != 5)
                    throw new Exception("La contrasena debe ser con el formato abcde12");


                if (txtActual.Text.ToString().Trim().Length == 7)
                {
                    string valor = txtActual.Text.Substring(5, 2);
                    int salida;
                    if (!Int32.TryParse(valor, out salida))
                        throw new Exception("La contrasena debe ser con el formato abcde12");
                }
                else
                { throw new Exception("La contraseña debe tener 7 caracteres."); }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtActual, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtNueva_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblError.Text = "";

                string letras = txtNueva.Text;
                letras = letras.Replace("0", "");
                letras = letras.Replace("1", "");
                letras = letras.Replace("2", "");
                letras = letras.Replace("3", "");
                letras = letras.Replace("4", "");
                letras = letras.Replace("5", "");
                letras = letras.Replace("6", "");
                letras = letras.Replace("7", "");
                letras = letras.Replace("8", "");
                letras = letras.Replace("9", "");

                if (letras.Length != 5)
                    throw new Exception("La contrasena debe ser con el formato abcde12");

                if (txtNueva.Text.ToString().Trim().Length == 7)
                {
                    string valor = txtNueva.Text.Substring(5, 2);
                    int salida;
                    if (!Int32.TryParse(valor, out salida))
                        throw new Exception("La contrasena debe ser con el formato abcde12");
                }
                else
                { throw new Exception("La contraseña debe tener 7 caracteres."); }
            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtNueva, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

    }
}
