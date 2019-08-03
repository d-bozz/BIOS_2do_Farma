<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="ConsultaMedicamentos.aspx.cs" Inherits="ConsultaMedicamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label class="Titulo" ID="Label2" runat="server" Text="Medicamentos en Stock"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Filtrar por Tipo de Medicamento: "></asp:Label>
&nbsp;<asp:DropDownList ID="ddTipo" runat="server">
                    <asp:ListItem>cardiologico</asp:ListItem>
                    <asp:ListItem>diabeticos</asp:ListItem>
                    <asp:ListItem>otros</asp:ListItem>
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="bnFiltrar" runat="server" onclick="bnFiltrar_Click" 
                    Text="Filtrar" />
                <asp:Button ID="btnLimpiar" runat="server" onclick="btnLimpiar_Click" 
                    Text="Limpiar" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Repeater ID="RTMedicamentos" runat="server" 
                    onitemcommand="RTMedicamentos_ItemCommand">
                <ItemTemplate>
                <table class="listadoMedicamentos">
                    <tr class="fila">
                        <td class= "celda">
                            <asp:Label ID="TxtNomMed" runat="server" Text='<%# Bind("NombreMedicamento") %>'></asp:Label>
                            <br />
                        </td>
                        
                        <td class= "celda">
                            <asp:Button ID="btnVerMedicamento" runat="server" CommandName="Ver" Style="text-align: center"
                                Text="Ver Detalle" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
                </asp:Repeater>
            &nbsp;</td>
            <td>
                <asp:Xml ID="Xml1" runat="server" 
                    TransformSource="~/Resources/Formato.xslt"></asp:Xml>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

