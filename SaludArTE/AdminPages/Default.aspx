<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SaludArTE.AdminPages.Default" %>
<%@ Import Namespace="SaludArTE" %>
<%@ Register Src="~/AdminPages/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="NavArea"  ContentPlaceHolderID="NavAreaPlaceHolder" runat="server">
    <uc1:Menu runat="server" id="Menu" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <div>
        Hola <%=this.CurrentUser().GivenName %> al panel de WebMaster
    </div>
</asp:Content>
