using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioFarma;

public partial class ConsultaPedidos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            lblMensaje.Text = "";
            lblEstado.Text = "";
    }


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {

            

            if (txtPedido.Text == "")
            {
                throw new Exception("El formato ingresado no es correcto");
            }

            if (!txtPedido.Text.Trim().All(char.IsNumber))
            {
                throw new Exception("El formato ingresado no es correcto");
            }

            if (Convert.ToInt32(txtPedido.Text.Trim()) < 0)
            {
                throw new Exception("El numero de pedido debe ser positivo.");
            }

            
            PedidoCabezal unPedido = null;
            IServicioWCF SPedido = new ServicioWCFClient();
            unPedido = SPedido.BuscarPedido(Convert.ToInt32(txtPedido.Text));
            if (unPedido == null)
            {
                 throw new Exception("No se encontro el pedido.");
            }
            else
            {
                lblEstado.Text = "El estado de su pedido es: "+unPedido.Estado;
                
            }
        }

        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
     }
    }
