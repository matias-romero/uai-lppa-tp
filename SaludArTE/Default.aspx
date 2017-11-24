<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Common.Master" CodeBehind="Default.aspx.cs" Inherits="SaludArTE.Default" %>
<%@ Import Namespace="SaludArTE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavAreaPlaceHolder" runat="server">
    <li class="active"><a href="<%=Page.ResolveUrl("~/RequestAppointment.aspx") %>">Home</a></li>
    <li><a href="#">About</a></li>
    <li><a href="#">Contact</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <div>
        Hola <%= this.CurrentUser().GivenName %>
    </div>
    <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click"/>
</asp:Content>