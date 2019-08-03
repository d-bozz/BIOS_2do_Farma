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
    public partial class ABMMedicamentos : Form
    {
        private Encargado encargadoLogueado;
        private Medicamento unMedicamento;
        private Farmaceutica farmaceutica;


        private void ABMMedicamentos_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        public ABMMedicamentos(Encargado usuLogin)
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

            txtcode.Enabled = true;
            txtDesc.Enabled = true;
            txtMed.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtTipo.Enabled = true;
          
            btnAgregar.Enabled = true;
            btnDeshacer.Enabled = true;

            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            txtFarma.Focus();

            txtFarma.Text = "";
            txtcode.Text = "";
            txtDesc.Text = "";
            txtMed.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtTipo.Text = "";

            }

        public void EstadoAgregar()
        {
            txtFarma.Enabled = false;
            txtcode.Enabled = false;

            txtDesc.Enabled = true;
            txtMed.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtTipo.Enabled = true;

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnDeshacer.Enabled = true;
        }

        public void EstadoEliminarModificar()
        {
            txtFarma.Enabled = false;
            txtcode.Enabled = false;

            txtDesc.Enabled = true;
            txtMed.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = false;
            txtTipo.Enabled = true;


            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnDeshacer.Enabled = true;
        }

        public void Inmovilizar()
        {
            txtFarma.Enabled = true;

            txtcode.Enabled = false;
            txtDesc.Enabled = false;
            txtMed.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
            txtTipo.Enabled = false;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
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
               farmaceutica = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarma.Text, encargadoLogueado);

               if (farmaceutica == null)
               
                   throw new Exception("No existe la farmaceutica ingresada");
               

                int Codigo = Convert.ToInt32(txtcode.Text);
                string Desc = txtDesc.Text;
                string NomMed = txtMed.Text;
                int Precio = Convert.ToInt32(txtPrecio.Text);
                int Stock = Convert.ToInt32(txtStock.Text);
                string Tipo = txtTipo.Text;

                unMedicamento = new Medicamento();

                unMedicamento.UnaFarmaceutica = farmaceutica;
                unMedicamento.Codigo = Codigo;
                unMedicamento.NomMed = NomMed;
                unMedicamento.Desc = Desc;
                unMedicamento.Precio = Precio;
                unMedicamento.Tipo = Tipo;
                unMedicamento.Stock = Stock;
                
                IServicioWCF servicioFarma = new ServicioWCFClient();
                servicioFarma.AgregarMedicamentos(unMedicamento, encargadoLogueado);
                Limpiar();
                lblError.Text = "Medicamento " + unMedicamento.NomMed + " agregado correctamente.";
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
                new ServicioWCFClient().EliminarMedicamentos(unMedicamento, encargadoLogueado);
                Limpiar();
                lblError.Text = "Medicamento " + unMedicamento.NomMed.Trim() + " eliminado correctamente.";
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
                Farmaceutica farmaceutica = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarma.Text, encargadoLogueado);

                if (farmaceutica == null)
                    throw new Exception("No existe la farmaceutica ingresada");

                //Uso otra variable por si da algun error el modificar y luego quiero eliminar el empleado.


                Medicamento _unMedi = new Medicamento();

                _unMedi.Codigo = unMedicamento.Codigo;
                _unMedi.Desc = unMedicamento.Desc;
                _unMedi.NomMed = unMedicamento.NomMed;
                _unMedi.Precio = unMedicamento.Precio;
                _unMedi.Stock = unMedicamento.Stock;
                _unMedi.Tipo = unMedicamento.Tipo;
                
                _unMedi.Codigo = Convert.ToInt32(txtcode.Text);
                _unMedi.Desc = txtDesc.Text;
                _unMedi.NomMed = txtMed.Text;
                _unMedi.Precio = Convert.ToInt32(txtPrecio.Text);
                _unMedi.Stock = Convert.ToInt32(txtStock.Text);
                _unMedi.Tipo = txtTipo.Text;
                _unMedi.UnaFarmaceutica = farmaceutica;
 
                
                new ServicioWCFClient().ModificarMedicamentos(_unMedi, encargadoLogueado);
                Limpiar();
                lblError.Text = "Medicamento " + _unMedi.NomMed.Trim() + " modificado correctamente.";
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
                if (txtFarma.Text.Trim() == "")
                    throw new Exception("El campo Farmaceutica no puede ser vacio.");

                if (txtFarma.Text.All(char.IsNumber))
                    throw new Exception("La Farmaceutica no puede contener numeros.");

                if (txtFarma.Text.Trim().Length > 20)
                    throw new Exception("El nombre no debe exceder los 20 caracteres.");

                Farmaceutica _Farma = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarma.Text.Trim(), encargadoLogueado);
                if (_Farma != null)
                {
                    farmaceutica = _Farma;
                    lblError.Text = "La Farmaceutica " + _Farma.NomFar.Trim() + " fue encontrada.";
                }

                else
                {
                    lblError.Text = "No se ha encontrado la Farmaceutica " + txtFarma.Text + ", Presione deshacer e intente nuevamente.";
                    Inmovilizar();
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

        private void txtcode_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                Ep1.Clear();

                if (txtcode.Text.Trim() == "")
                    throw new Exception("El codigo no puede ser vacio.");

                if (!txtcode.Text.Trim().All(char.IsNumber))
                    throw new Exception("El codigo solo puede contener numeros.");

                if (Convert.ToInt32(txtcode.Text.Trim()) < 0)
                    throw new Exception("El codigo debe ser positivo.");


                unMedicamento = new ServicioWCFClient().BuscarMedicamentosActivos(farmaceutica, Convert.ToInt32(txtcode.Text), encargadoLogueado);
                if (unMedicamento != null)
                {
                    txtcode.Text = unMedicamento.Codigo.ToString();
                    txtDesc.Text = unMedicamento.Desc;
                    txtMed.Text = unMedicamento.NomMed;
                    txtPrecio.Text = unMedicamento.Precio.ToString();
                    txtStock.Text = unMedicamento.Stock.ToString();
                    txtTipo.SelectedItem = unMedicamento.Tipo;
                    lblError.Text = "Medicamento " + unMedicamento.NomMed + " fue encontrado.";
                    EstadoEliminarModificar();
                }

                else
                {
                    lblError.Text = "No se ha encontrado el medicamento " + txtcode.Text + ", si lo desea puede agregarlo.";
                    EstadoAgregar();
                }

            }
            catch (Exception ex)
            {
                Ep1.SetError(txtcode, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtMed_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                if (txtMed.Text.Trim() == "")
                    throw new Exception("El campo Medicamento no puede ser vacio.");

                if (txtMed.Text.All(char.IsNumber))
                    throw new Exception("El Medicamento no puede contener numeros.");

                if (txtMed.Text.Trim().Length > 20)
                    throw new Exception("El nombre no debe exceder los 20 caracteres.");
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

        private void txtDesc_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (txtDesc.Text.Trim() == "")
                    throw new Exception("Ingrese direccion.");

                if (txtDesc.Text.Trim().Length > 50)
                    throw new Exception("La direccion no debe exceder los 50 caracteres.");
            }

            catch (Exception ex)
            {
                Ep1.SetError(txtDesc, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (txtPrecio.Text.Trim() == "")
                    throw new Exception("El precio no puede ser vacio.");

                if (!txtPrecio.Text.Trim().All(char.IsNumber))
                    throw new Exception("El precio solo puede contener numeros.");

                if (Convert.ToInt32(txtPrecio.Text.Trim()) < 0)
                    throw new Exception("El precio debe ser positivo.");

            }


            catch (Exception ex)
            {
                Ep1.SetError(txtPrecio, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtTipo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (!(txtTipo.Text.Trim().ToLower() == "cardiologico" || txtTipo.Text.Trim().ToLower() == "diabeticos" || txtTipo.Text.Trim().ToLower() == "otros"))
                    throw new Exception("El tipo ingresado no es correcto.");
            }

            catch (Exception ex)
            {
                Ep1.SetError(txtTipo, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtStock_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (string.IsNullOrWhiteSpace(txtStock.Text.Trim()))
                    throw new Exception("El stock no puede ser vacio.");

                if (!txtStock.Text.Trim().All(char.IsNumber))
                    throw new Exception("El stock solo puede contener numeros.");

                if (Convert.ToInt32(txtStock.Text.Trim()) < 0)
                    throw new Exception("El stock debe ser positivo.");
            }

            catch (Exception ex)
            {
                Ep1.SetError(txtPrecio, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
