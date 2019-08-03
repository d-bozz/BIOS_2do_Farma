using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administracion.ServicioFarma;
using System.Xml;

namespace Administracion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        #region visuals

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (_txtUser.Text == "USUARIO")
            {
                _txtUser.Text = "";
                _txtUser.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void _txtUser_Leave(object sender, EventArgs e)
        {
            if (_txtUser.Text == "")
            {
                _txtUser.Text = "USUARIO";
                _txtUser.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (_txtPass.Text == "CONTRASENIA")
            {
                _txtPass.Text = "";
                _txtPass.ForeColor = System.Drawing.Color.Black;
                _txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (_txtPass.Text == "")
            {
                _txtPass.Text = "CONTRASENIA";
                _txtPass.UseSystemPasswordChar = false;
                _txtPass.ForeColor = System.Drawing.Color.DimGray;

            }
        }

        #endregion

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                errorProvider1.Clear();
                Usuario user = new ServicioWCFClient().Login(_txtUser.Text.ToLower(), _txtPass.Text);

                if (user == null)
                    throw new Exception("Usuario o contrasenia incorrectos. Intentelo de nuevo.");

                this.Hide();
                if (user is Empleado)
                {
                    //crear xml
                    XmlDocument doc = new XmlDocument();
                    XmlElement raiz = doc.CreateElement("Sesion");
                    XmlElement usuario = doc.CreateElement("Empleado");

                    XmlElement ci = doc.CreateElement("CI");
                    ci.InnerText = user.Ci.ToString();
                    usuario.AppendChild(ci);

                    //XmlElement contrasenia = doc.CreateElement("Contrasenia");
                    //contrasenia.InnerText = user.Contra;
                    //usuario.AppendChild(contrasenia);

                    //XmlElement id = doc.CreateElement("Nombre");
                    //id.InnerText = user.NomUser;
                    //usuario.AppendChild(id);

                    //XmlElement nombreCompleto = doc.CreateElement("Nombre_Completo");
                    //nombreCompleto.InnerText = user.NomCom;
                    //usuario.AppendChild(nombreCompleto);

                    //Horarios.
                    XmlElement horarioInicio = doc.CreateElement("Horario_Inicio");
                    horarioInicio.InnerText = ((Empleado)user).Inicio;
                    usuario.AppendChild(horarioInicio);

                    XmlElement horarioFin = doc.CreateElement("Horario_Fin");
                    horarioFin.InnerText = ((Empleado)user).Fin;
                    usuario.AppendChild(horarioFin);

                    raiz.AppendChild(usuario);

                    doc.AppendChild(raiz);

                    doc.Save(Constantes.xmlPath);

                    MenuEmpleado menuEmpleado = new MenuEmpleado((Empleado)user);
                    menuEmpleado.ShowDialog();
                }
                else
                {
                    MenuEncargado menuEncargado = new MenuEncargado((Encargado)user);
                    menuEncargado.ShowDialog();
                }

                this.Close();

            }
            catch (Exception ex)
            {
                errorProvider1.SetError(_btnLogin, ex.Message);
                lblError.Text = ex.Message;
            }
        }

        #region validaciones
        
        private void _txtUser_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(_txtUser.Text))
                    throw new Exception("El usuario no puede ser vacio");
                if (_txtUser.Text.Length > 20)
                    throw new Exception("El nombre de usuario no puede contener mas de 20 caracteres.");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(_txtUser, ex.Message);
            }
        }

        private void _txtPass_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(_txtPass.Text))
                    throw new Exception("La contrasenia no puede ser vacia.");

                if (_txtPass.Text.Trim().Length == 0)
                    throw new Exception("La contraseña no puede ser vacia.");

                if (_txtPass.Text.Trim().Length != 7)
                    throw new Exception("La contraseña debe tener 7 caracteres.");

                string letras = _txtPass.Text;
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

                string numeros = _txtPass.Text.Substring(5, 2);
                int salida;
                if (!Int32.TryParse(numeros, out salida))
                    throw new Exception("La contrasena debe ser con el formato abcde12");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(_txtPass, ex.Message);
                lblError.Text = ex.Message;
            }
        }

        private void _txtUser_Validating_1(object sender, CancelEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(_txtUser.Text))
                    throw new Exception("El nombre de usuario no puede estar vacio");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(_txtUser, ex.Message);
                lblError.Text = ex.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        #endregion  
    }
}
