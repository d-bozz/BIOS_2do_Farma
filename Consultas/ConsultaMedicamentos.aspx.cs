using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioFarma;

using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data;

public partial class ConsultaMedicamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                XmlDocument _documentoXML = new XmlDocument();
                _documentoXML.LoadXml(new ServicioWCFClient().MedicamentosStock());

                Session["listaMedicamentosEnStock"] = _documentoXML;

                DataSet dataSet = new DataSet();
                XmlNodeReader reader = new XmlNodeReader(_documentoXML);

                dataSet.ReadXml(reader);

                if (dataSet.Tables.Count == 0)
                    throw new Exception("No hay datos para mostrar.");

                RTMedicamentos.DataSource = dataSet;
                RTMedicamentos.DataBind();

            }
        }
        catch (Exception ex)
        {
            LblMensaje.Text = ex.Message;
        }
    }

    protected void RTMedicamentos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            try
            {
                XElement _doc = XElement.Parse(((XmlDocument)Session["listaMedicamentosEnStock"]).OuterXml);

                var _resultado = (from unNodo in _doc.Elements("Medicamento")
                                  where Convert.ToString(unNodo.Element("NombreMedicamento").Value).Equals(((Label)e.Item.Controls[1]).Text)
                                  select unNodo);

                string Res = "<Medicamentos>";
                foreach (var unNodo in _resultado)
                {
                    Res += unNodo.ToString();
                }
                Res += "</Medicamentos>";


                Xml1.DocumentContent = Res;

            }

            catch (Exception ex)
            {
                LblMensaje.Text = ex.Message;
            }

        }
    }

    protected void bnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            RTMedicamentos.DataSource = null;
            RTMedicamentos.DataBind();
            Xml1.DocumentContent = "";

            XElement _doc = XElement.Parse(((XmlDocument)Session["listaMedicamentosEnStock"]).OuterXml);

            var _resultado = (from unNodo in _doc.Elements("Medicamento")
                              where Convert.ToString(unNodo.Element("Tipo").Value).Equals(ddTipo.SelectedItem.Text)
                              select unNodo).ToList();

            string Res = "<Medicamentos>";
            foreach (var unNodo in _resultado)
            {
                Res += unNodo.ToString();
            }
            Res += "</Medicamentos>";

            XmlDocument _documentoXML = new XmlDocument();
            _documentoXML.LoadXml(Res);


            DataSet dataSet = new DataSet();
            XmlNodeReader reader = new XmlNodeReader(_documentoXML);

            dataSet.ReadXml(reader);

            if (dataSet.Tables.Count == 0)
                throw new Exception("No hay datos para mostrar.");

            RTMedicamentos.DataSource = dataSet;
            RTMedicamentos.DataBind();
            
            LblMensaje.Text = "Mostrando datos filtrados.";
            //Session["listadoFiltrado"] = _documentoXML;
            //Session["ultimoFiltro"] = ddTipo.SelectedItem;

        }
        catch (Exception ex)
        {
            LblMensaje.Text = ex.Message;
        }

    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            LblMensaje.Text = "";
            RTMedicamentos.DataSource = null;
            RTMedicamentos.DataBind();
            Xml1.DocumentContent = "";

            XmlDocument _documentoXML = (XmlDocument)Session["listaMedicamentosEnStock"];

            DataSet dataSet = new DataSet();
            XmlNodeReader reader = new XmlNodeReader(_documentoXML);

            dataSet.ReadXml(reader);

            if (dataSet.Tables.Count == 0)
                throw new Exception("No hay datos para mostrar.");

            RTMedicamentos.DataSource = dataSet;
            RTMedicamentos.DataBind();
        }
        catch (Exception ex)
        {
            LblMensaje.Text = ex.Message;
        }
    }
}