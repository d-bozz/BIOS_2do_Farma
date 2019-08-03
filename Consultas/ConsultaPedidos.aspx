<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="ConsultaPedidos.aspx.cs" Inherits="ConsultaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 486px;
            text-align: right;
        }
        .style2
        {
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="Titulo" colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Titulo" colspan="3">
                Busqueda de Pedidos</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                Ingrese el codugo del pedido:
                <asp:TextBox ID="txtPedido" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button class="Button1" ID="btnBuscar" runat="server" Text="Buscar" 
                    onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                <asp:Label ID="lblEstado" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

