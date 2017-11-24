<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SaludArTE.BackendPages.Default" %>
<%@ Import Namespace="SaludArTE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadAreaPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="NavArea"  ContentPlaceHolderID="NavAreaPlaceHolder" runat="server">
    <li><a href="#">Proveedores</a></li>
    <li><a href="#">Agendas</a></li>
    <li><a href="#">Turnos</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <div>
        Hola <%=this.CurrentUser().GivenName %> al panel de Backend
    </div>
</asp:Content>
