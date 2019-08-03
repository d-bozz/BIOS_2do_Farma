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
    public partial class AltaEncargados : Form
    {
        private Encargado encargadoLogueado;

        private void AltaEncargados_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }


        public AltaEncargados(Encargado usuLogin)
        {
            InitializeComponent();
            encargadoLogueado = usuLogin;
        }

        //Estados
        public void EstadoInicial()
        {
            Limpiar();
            btnAgregar.Enabled = false;
            lblError.Text = "";
        }

        public void Limpiar()
        {
            txtCi.Enabled = true;

            txtUser.Enabled = true;
            txtPass.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;

            btnAgregar.Enabled = true;
            btnDeshacer.Enabled = true;

            txtCi.Focus();

            txtCi.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            txtNom.Text = "";
            txtTel.Text = "";
            
        }

        public void EstadoAgregar()
        {
            txtCi.Enabled = false;
            txtPass.Enabled = true;
            txtUser.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;

            btnAgregar.Enabled = true;
            btnDeshacer.Enabled = true;
        }

        //Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Encargado nuevoEncargado = null;
                int Ci = Convert.ToInt32(txtCi.Text);
                string Contra = txtPass.Text;
                string NomCom = txtNom.Text;
                string NomUser = txtUser.Text;
                int Telefono = Convert.ToInt32(txtTel.Text);

                nuevoEncargado = new Encargado();

                nuevoEncargado.Ci = Ci;
                nuevoEncargado.NomUser = NomUser;
                nuevoEncargado.Contra = Contra;
                nuevoEncargado.NomCom = NomCom;
                nuevoEncargado.Telefono = Telefono;

                IServicioWCF servicioFarma = new ServicioWCFClient();
                servicioFarma.AgregarUsuario(nuevoEncargado, encargadoLogueado);
                Limpiar();
                lblError.Text = "Encargado " + nuevoEncargado.NomCom + " agregado correctamente.";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        //Validaciones

        private void txtCi_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                Ep1.Clear();
                lblError.Text = "";

                if (txtCi.Text.Trim() == "")
                {
                    Limpiar();
                    throw new Exception("La CI no puede ser vacia.");
                }
                if (txtCi.Text.Length != 8 || txtCi.Text.Length < 0)
                    throw new Exception("El numero de cedula es incorrecto.");

                if (!txtCi.Text.All(char.IsNumber))
                    throw new Exception("La cedula solo puede contener numeros.");

                
                Usuario encargado = new ServicioWCFClient().BuscarUsuariosActivos(Convert.ToInt32(txtCi.Text.Trim()),encargadoLogueado);

                if (encargado is Encargado)
                {
                    throw new Exception("Ya existe el usuario con la cedula " + txtCi.Text);
                }
                else if (encargado is Empleado)
                {
                    throw new Exception("La CI " + txtCi.Text + " pertenece a un Empleado.");
                }
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtCi, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtUser_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    throw new Exception("El Usuario no puede ser Vacio.");
                }

                if (txtUser.Text.Length > 20)
                {
                    throw new Exception("El Usuario no puede exceder los 20 caracteres.");
                }
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtUser, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtPass.Text))
                {
                    throw new Exception("La contraseña no puede ser vacia.");
                }

                if (txtPass.Text.Length != 7)
                {
                    throw new Exception("La contraseña debe tener 7 caracteres.");
                }

            }
            catch (Exception ex)
            {
                Ep1.SetError(txtPass, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtNom_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtNom.Text))
                {
                    throw new Exception("El nombre no puede ser Vacio.");
                }

                if (txtNom.Text.Length > 50)
                {
                    throw new Exception("El nombre no debe exceder los 50 caracteres.");
                }
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtNom, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtTel_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (string.IsNullOrWhiteSpace(txtTel.Text.Trim()))
                    throw new Exception("Ingrese telefono.");

                if (!txtTel.Text.Trim().All(char.IsNumber))
                    throw new Exception("El telefono solo puede contener numeros.");

                if (Convert.ToInt32(txtTel.Text.Trim()) < 0)
                    throw new Exception("El telefono debe ser positiva.");
            }
            catch (FormatException)
            {
                Ep1.SetError(txtTel, "El campo telefono debe ser un numero.");
                lblError.Text = "El campo telefono debe ser un numero.";
            }
            
            catch (Exception ex)
            {
                Ep1.SetError(txtTel, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
