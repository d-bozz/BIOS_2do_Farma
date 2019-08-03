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
    public partial class ABMEmpleados : Form
    {
        private Encargado encargadoLogueado;
        private Empleado unEmpleado;


        private void ABMEmpleados_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
        }

        public ABMEmpleados(Encargado usuLogin)
        {
            InitializeComponent();
            encargadoLogueado = usuLogin;
        }

        //Estados
        public void EstadoInicial()
        {
            Limpiar();
            lblError.Text = "";
        }

        public void Limpiar()
        {
            txtCi.Enabled = true;

            txtUser.Enabled = true;
            txtPass.Enabled = true;
            txtNom.Enabled = true;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnDeshacer.Enabled = true;

            dtpInicio.Enabled = true;
            dtpFin.Enabled = true;
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;

            txtCi.Focus();

            txtCi.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            txtNom.Text = "";
        }

        public void EstadoAgregar()
        {
            txtCi.Enabled = false;

            txtPass.Enabled = true;
            txtUser.Enabled = true;
            txtNom.Enabled = true;
            dtpInicio.Enabled = true;
            dtpFin.Enabled = true;

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnDeshacer.Enabled = true;
        }

        public void EstadoEliminarModificar()
        {
            txtCi.Enabled = false;

            txtUser.Enabled = false;
            txtPass.Enabled = false;
            txtNom.Enabled = true;
            dtpInicio.Enabled = true;
            dtpFin.Enabled = true;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnDeshacer.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        //Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int Ci = Convert.ToInt32(txtCi.Text);
                string NomUser = txtUser.Text;
                string Contra = txtPass.Text;
                string NomCom = txtNom.Text;
                string Inicio = dtpInicio.Value.Hour.ToString()+":"+dtpInicio.Value.Minute.ToString();
                string Fin = dtpFin.Value.Hour.ToString() + ":" + dtpFin.Value.Minute.ToString();

                unEmpleado = new Empleado();

                unEmpleado.Ci = Ci;
                unEmpleado.NomUser = NomUser;
                unEmpleado.Contra = Contra;
                unEmpleado.NomCom = NomCom;
                unEmpleado.Inicio = Inicio;
                unEmpleado.Fin = Fin;

                IServicioWCF servicioFarma = new ServicioWCFClient();
                servicioFarma.AgregarUsuario(unEmpleado, encargadoLogueado);
                Limpiar();
                lblError.Text = "Empleado " + unEmpleado.NomCom + " agregado correctamente.";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try

            {
                //Uso otra variable por si da algun error el modificar y luego quiero eliminar el empleado.

                Empleado _unEmpleado = new Empleado();
                _unEmpleado.Ci = unEmpleado.Ci;
                _unEmpleado.Contra = unEmpleado.Contra;
                _unEmpleado.NomCom = unEmpleado.NomCom;
                _unEmpleado.NomUser = unEmpleado.NomUser;
                _unEmpleado.Inicio = unEmpleado.Inicio;
                _unEmpleado.Fin = unEmpleado.Fin;

                _unEmpleado.NomCom = txtNom.Text;
                _unEmpleado.NomUser = txtUser.Text;
                _unEmpleado.Inicio = dtpInicio.Value.ToShortTimeString();
                _unEmpleado.Fin = dtpFin.Value.ToShortTimeString();

                new ServicioWCFClient().ModificarUsuario(_unEmpleado, encargadoLogueado);
                Limpiar();
                lblError.Text = "Empleado " + _unEmpleado.NomCom.Trim() + " modificado correctamente.";
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                new ServicioWCFClient().EliminarUsuario(unEmpleado, encargadoLogueado);
                Limpiar();
                lblError.Text = "Empleado " + unEmpleado.NomCom.Trim() + " eliminado correctamente.";
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
            EstadoInicial();
        }

        //Validaciones

        private void txtCi_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                Ep1.Clear();
                lblError.Text = "";

                //DateTime inicio = dtpInicio.Value;
                //DateTime fin = dtpFin.Value;

                
                if (txtCi.Text.Trim() == "")
                {
                    throw new Exception("La CI no puede ser vacia.");
                }
                if (txtCi.Text.Length != 8 || txtCi.Text.Length < 0)
                    throw new Exception("El numero de cedula es incorrecto.");

                if (!txtCi.Text.All(char.IsNumber))
                    throw new Exception("La cedula solo puede contener numeros.");

                Usuario empleado =  new ServicioWCFClient().BuscarUsuariosActivos(Convert.ToInt32(txtCi.Text.Trim()),encargadoLogueado);
                
                if (empleado is Empleado)
                {
                    unEmpleado = (Empleado)empleado;
                    txtNom.Text = empleado.NomCom;
                    txtUser.Text = empleado.NomUser;
                    dtpInicio.Value = Convert.ToDateTime(((Empleado)empleado).Inicio);
                    dtpFin.Value = Convert.ToDateTime(((Empleado)empleado).Fin);

                    lblError.Text = "Empleado " + empleado.NomCom.Trim() + " fue encontrado.";
                    EstadoEliminarModificar();
                }
                else if (empleado is Encargado)
                {
                    lblError.Text = "La CI " + txtCi.Text + " pertenece a un Encargado.";
                    Limpiar();
                }
                else
                {
                    lblError.Text = "No se ha encontrado un empleado con CI " + txtCi.Text + ", si lo desea puede agregarlo.";
                    EstadoAgregar();
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

                if (txtNom.Text == "")
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
    }
}
