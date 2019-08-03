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
    public partial class AltaPedidos : Form
    {
        Empleado empleadoLogueado;
        PedidosLinea _unaLinea = null;
        List<PedidosLinea> _lineas = null;
        Medicamento _unMedi = null;
        DataTable dt = null;

        public AltaPedidos(Empleado usuLogin)
        {
            InitializeComponent();
            empleadoLogueado = usuLogin;
        }

        //Estados
        public void EstadoInicial()
        {
            Limpiar();
        }

        public void EstadoMedAgregado()
        {

            txtMed.Text = "";
            txtFarmaceutica.Text = "";
            txtCant.Text = "";

            txtFarmaceutica.Focus();
        }

        public void Limpiar()
        {
            _unaLinea = null;
            _lineas = null;
            _unMedi = null;
            dt = null;
            DGVMedicamentos.DataSource = null;

            txtFarmaceutica.Focus();
            txtMed.Text = "";
            txtFarmaceutica.Text = "";
            txtCant.Text = "";
            txtDire.Text = "";
            
        }
        //Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_lineas == null)
                {
                    throw new Exception("No se puede crear el pedido sin Medicamentos");
                }
                else
                {
                    PedidoCabezal nuevoPedido = null;
                    string DirEtga = txtDire.Text;
                    string Estado = "Generado";
                    DateTime FecReal = DateTime.Today;
                    int Numero = 1;


                    nuevoPedido = new PedidoCabezal();

                    nuevoPedido.Numero = Numero;
                    nuevoPedido.FecReal = FecReal;
                    nuevoPedido.DirEtga = DirEtga;
                    nuevoPedido.Estado = Estado;
                    nuevoPedido.UnEmp = empleadoLogueado;
                    nuevoPedido.Lineas = _lineas.ToArray();

                    IServicioWCF servicioFarma = new ServicioWCFClient();
                    servicioFarma.AltaPedido(nuevoPedido);
                    lblError.Text = "Pedido agregado correctamente";
                    EstadoInicial();
                }

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
            lblError.Text = "";
        }

        private void btnAgMed_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFarmaceutica.Text))
                    throw new Exception("La Farmaceutica no puede ser vacia.");
                if (string.IsNullOrWhiteSpace(txtMed.Text))
                    throw new Exception("El Medicamento no puede ser vacio.");
                if (string.IsNullOrWhiteSpace(txtCant.Text))
                    throw new Exception("La cantidad no puede ser vacia.");

                int codMedicamento;
                if (!Int32.TryParse(txtMed.Text, out codMedicamento))
                    throw new Exception("Debe ingresar un codigo valido.");
                if (codMedicamento < 0)
                    throw new Exception("Debe ingresar un codigo positivo.");

                int cantidad;
                if (!Int32.TryParse(txtCant.Text, out cantidad))
                    throw new Exception("Debe ingresar una cantidad valida.");
                if (cantidad < 0)
                    throw new Exception("Debe ingresar una cantidad positiva.");


                //Busco la farmaceutica.
                Farmaceutica _unaFarmaceutica = new ServicioWCFClient().BuscarFarmaceuticaActiva(txtFarmaceutica.Text, empleadoLogueado);

                if (_unaFarmaceutica == null)
                    throw new Exception("No se encontro la farmaceutica ingresada.");


                //Busco el medicamento
                _unMedi = new ServicioWCFClient().BuscarMedicamentosActivos(_unaFarmaceutica, Convert.ToInt32(txtMed.Text), empleadoLogueado);

                if (_unMedi == null)
                    throw new Exception("No se encontro el medicamento ingresado.");


                //Valido si la cantidad de stock es suficiente para cumplir con el pedido.
                if (_unMedi.Stock < Convert.ToInt32(txtCant.Text))
                {
                    throw new Exception("Lo sentimos, solamente hay " + _unMedi.Stock + " en stock.");
                }

                _unaLinea = new PedidosLinea();

                _unaLinea.UnMedicamento = _unMedi;
                _unaLinea.Cant = Convert.ToInt32(txtCant.Text);


                if (_lineas == null)
                {
                    _lineas = new List<PedidosLinea>();
                    dt = new DataTable();
                    dt.Columns.Add("Nombre Medicamento");
                    dt.Columns.Add("Cantidad");
                }

                if (_lineas.Count > 0)
                {
                    int i = 0;
                    bool esta = false;
                    while (i < _lineas.Count && !esta)
                    {
                        //Controlo que el medicamento no este ingresado.
                        if (_unMedi.Codigo == _lineas[i].UnMedicamento.Codigo && _unMedi.UnaFarmaceutica.NomFar == _lineas[i].UnMedicamento.UnaFarmaceutica.NomFar)
                            esta = true;
                        i++;
                    }
                    if (!esta)
                    {
                        _lineas.Add(_unaLinea);
                        Actualizar();
                        lblError.Text = "Medicamento agregado correctamente.";
                    }
                    else
                        throw new Exception("Medicamento Repetido.");
                }
                else
                {
                    _lineas.Add(_unaLinea);
                    Actualizar();
                }

                EstadoMedAgregado();
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void Actualizar()
        {
            dt.Rows.Clear();
            foreach (PedidosLinea p in _lineas)
            {
                dt.Rows.Add(p.UnMedicamento.NomMed, p.Cant);
                lblError.Text = p.UnMedicamento.NomMed;
            }

            DGVMedicamentos.DataSource = null;
            DGVMedicamentos.DataSource = dt;
        }

        private void btnBorrarMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVMedicamentos.SelectedRows.Count == 0)
                    throw new Exception("Seleccione un medicamento primero");

                _lineas.RemoveAt(DGVMedicamentos.SelectedRows[0].Index);
                dt.Rows.RemoveAt(DGVMedicamentos.SelectedRows[0].Index);
                DGVMedicamentos.DataSource = null;
                DGVMedicamentos.DataSource = dt;

                lblError.Text = "Medicamento eliminado correctamente.";
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        //Validaciones

        private void txtMed_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();

                if (string.IsNullOrWhiteSpace(txtMed.Text))
                    throw new Exception("El Medicamento no puede estar vacio.");
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtMed, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtCant_Validating(object sender, CancelEventArgs e)
        {
            Ep1.Clear();
            try
            {
                if (string.IsNullOrWhiteSpace(txtCant.Text.Trim()))
                    throw new Exception("Ingrese Cantidad");

                if (!txtCant.Text.Trim().All(char.IsNumber))
                    throw new Exception("La cantidad solo puede contener numeros.");

                if (Convert.ToInt32(txtCant.Text) < 1)
                    throw new Exception("La cantidad debe ser mayor a una unidad");
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtDire, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtDire_Validating(object sender, CancelEventArgs e)
        {
            Ep1.Clear();
            try
            {
                if (string.IsNullOrWhiteSpace(txtDire.Text.Trim()))
                    throw new Exception("Ingrese Direccion");
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtDire, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        private void txtFarmaceutica_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Ep1.Clear();
                if (txtFarmaceutica.Text.Trim() == "")
                    throw new Exception("El campo Farmaceutica no puede ser vacio.");

                if (txtFarmaceutica.Text.All(char.IsNumber))
                    throw new Exception("La Farmaceutica no puede contener numeros.");

                if (txtFarmaceutica.Text.Trim().Length > 20)
                    throw new Exception("El nombre no debe exceder los 20 caracteres.");
            }
            catch (Exception ex)
            {
                Ep1.SetError(txtFarmaceutica, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
                
    }
}
