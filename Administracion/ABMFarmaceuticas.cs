using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administracion.ServicioFarma;
using System.Text.RegularExpressions;


namespace Administracion
{
    public partial class ABMFarmaceuticas : Form
    {

        private Farmaceutica unaFarmaceutica;
        private Encargado encargadoLogueado;



        private void ABMFarmaceuticas_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        public ABMFarmaceuticas(Encargado usuLogin)
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
            txtFarma.Enabled = true;

            txtDirFis.Enabled = true;
            txtTfno.Enabled = true;
            txtCorreo.Enabled = true;

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnDeshacer.Enabled = true;
            
            txtFarma.Focus();
            
            txtFarma.Text = "";
            txtDirFis.Text = "";
            txtTfno.Text = "";
            txtCorreo.Text = "";

        }

        public void EstadoAgregar()
        {
            txtFarma.Enabled = false;

            txtDirFis.Enabled = true;
            txtTfno.Enabled = true;
            txtCorreo.Enabled = true;

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnDeshacer.Enabled = true;
        }

        public void EstadoEliminarModificar()
        {
            txtFarma.Enabled = false;

            txtDirFis.Enabled = true;
            txtTfno.Enabled = true;
            txtCorreo.Enabled = true;

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
                string correo = txtCorreo.Text;
                string dirFisc = txtDirFis.Text;
                string nomFar = txtFarma.Text;
                int telefono = Convert.ToInt32(txtTfno.Text);

                unaFarmaceutica = new Farmaceutica();

                unaFarmaceutica.NomFar = nomFar;
                unaFarmaceutica.DirFisc = dirFisc;
                unaFarmaceutica.Telefono = telefono;
                unaFarmaceutica.Correo = correo;


                IServicioWCF servicioFarma = new ServicioWCFClient();
                servicioFarma.AltaFarmaceutica(unaFarmaceutica, encargadoLogueado);
                EstadoAgregar();
                lblError.Text = "Farmaceutica " + nomFar+ " agregada correctamente.";
                Limpiar();

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
                new ServicioWCFClient().EliminarFarmaceutica(unaFarmaceutica, encargadoLogueado);
                Limpiar();
                lblError.Text = "Farmaceutica " + unaFarmaceutica.NomFar.Trim() + " eliminada correctamente.";
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

                Farmaceutica _unaFarma = new Farmaceutica();
                
                _unaFarma.Correo = unaFarmaceutica.Correo;
                _unaFarma.DirFisc = unaFarmaceutica.DirFisc;
                _unaFarma.NomFar = unaFarmaceutica.NomFar;
                _unaFarma.Telefono = unaFarmaceutica.Telefono;                
                
                _unaFarma.Correo = txtCorreo.Text;
                _unaFarma.DirFisc = txtDirFis.Text;
                _unaFarma.NomFar = txtFarma.Text;
                _unaFarma.Telefono = Convert.ToInt32(txtTfno.Text);

                
                new ServicioWCFClient().ModificarFarmaceutica(_unaFarma, encargadoLogueado);
                Limpiar();
                lblError.Text = "Farmaceutica " + _unaFarma.NomFar.Trim() + " modificada correctamente.";
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

        private void txtFarma_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                lblError.Text = "";

                if (txtFarma.Text.Trim() == "")
                {
                    Limpiar();
                    throw new Exception("El campo Farmaceutica no puede ser vacio.");
                }
                if (txtFarma.Text.All(char.IsNumber))
                    throw new Exception("La Farmaceutica no puede contener numeros.");

                if (txtFarma.Text.Trim().Length > 20)
                    throw new Exception("El nombre no debe exceder los 20 caracteres.");

                Farmaceutica _Farma = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarma.Text.Trim(),encargadoLogueado);
                if (_Farma != null)
                {
                    unaFarmaceutica = _Farma;
                    txtCorreo.Text = _Farma.Correo;
                    txtDirFis.Text = _Farma.DirFisc;
                    txtFarma.Text = _Farma.NomFar;
                    txtTfno.Text =_Farma.Telefono.ToString();
                    lblError.Text = "La Farmaceutica " + _Farma.NomFar.Trim() + " fue encontrada.";
                    EstadoEliminarModificar();
                }
                else
                {
                    lblError.Text = "No se ha encontrado la Farmaceutica " + txtFarma.Text + ", si lo desea puede agregarla.";
                    EstadoAgregar();
                }
            }
            
            catch (Exception ex)
            {
                Ep1.SetError(txtFarma, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtDirFis_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (txtDirFis.Text.Trim() == "")
                    throw new Exception("Ingrese direccion.");

                if (txtDirFis.Text.Trim().Length > 50)
                    throw new Exception("La direccion no debe exceder los 50 caracteres.");
            }
            
            catch (Exception ex)
            {
                Ep1.SetError(txtDirFis, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtTfno_Validating(object sender, CancelEventArgs e)
        {
            try
            {
             Ep1.Clear();

                if (txtTfno.Text.Trim() == "")
                {
                    throw new Exception("El numero de telefono no puede ser vacio.");
                }
                if (!txtTfno.Text.Trim().All(char.IsNumber))
                    throw new Exception("El telefono solo puede contener numeros.");

                if (Convert.ToInt32(txtTfno.Text.Trim()) < 0)
                    throw new Exception("El numero de telefono debe ser positivo.");
            }
            catch (FormatException)
            {
                Ep1.SetError(txtTfno, "El campo telefono debe ser un numero.");
                lblError.Text = "El campo telefono debe ser un numero.";
            }
           
            catch (Exception ex)
            {
                Ep1.SetError(txtTfno, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtCorreo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (txtCorreo.Text.Trim() == "")
                    throw new Exception("El email no puede ser vacio.");

                if (txtCorreo.Text.Trim().Length > 50)
                    throw new Exception("El email no debe exceder los 50 caracteres.");

                if (ComprobarFormatoEmail(txtCorreo.Text) == false)
                {
                    throw new Exception("El email introducido no tiene el formato correcto");
                }
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtCorreo, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private static bool ComprobarFormatoEmail(string sEmailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(sEmailAComprobar, sFormato))
            {
                if (Regex.Replace(sEmailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
