<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style2
    {
    }
    .style3
    {
        width: 123px;
    }
        .style6
        {
            text-align: left;
        }
        .style7
        {
            text-align: left;
            width: 122px;
        }
        .style8
        {
            width: 236px;
        }
        .style10
        {
            width: 654px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
    <tr>
        <td class="style8">
            &nbsp;</td>
        <td class="style3">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/medicina.png" />
        </td>
        <td class="Titulo" colspan="3" style="text-align: center">
            <strong style="font-size: xx-large">BiosFarma</strong></td>
    </tr>
    <tr>
        <td class="style8">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style8">
            &nbsp;</td>
        <td class="style2" colspan="2">
            <strong>Nuestro Objetivo:&nbsp;&nbsp; </strong></td>
        <td class="style7">
                &nbsp;</td>
        <td class="style10">
                Nuestro objetivo prioritario es la atención integral al paciente en torno al 
                medicamento como bien sanitario por sobre todas las cosas, contribuimos de forma 
                decisiva a asegurar un uso seguro y eficiente del mismo y obtener los mejores 
                resultados en salud.</td>
        <td class="style6" width="500">
                &nbsp;</td>
    </tr>
</table>
</asp:Content>

