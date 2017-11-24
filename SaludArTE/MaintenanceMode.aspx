<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceMode.aspx.cs" Inherits="SaludArTE.MaintenanceMode" %>
<%@ Import Namespace="SaludArTE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="<%=Page.UICulture%>">
<head runat="server">
    <title><%=Resources.Title%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="<%=Page.ResolveUrl("~/Resources/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveUrl("~/Resources/jquery/jquery-3.2.1.min.js") %>"></script>
    <script>
        function retry() {
            window.location.href = '<%=Page.ResolveUrl("~/Login.aspx")%>';
        }

        function restorePreviousBackup() {
            window.location.href = '<%=Page.ResolveUrl("~/AdminPages/BackupRestore.aspx")%>';
        }
    </script>
</head>
<body>
    <div class="container-fluid">
        <div class="alert alert-warning" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close" onclick="retry();"><span aria-hidden="true">&times;</span></button>
            <strong>Disculpe :( </strong><span>En estos momentos el sistema se encuentra en mantenimiento.</span>
            <p>Por favor intente m&aacute;s tarde</p>
        </div>
        <% if (this.CurrentUser().IsInRole("admin")) { %>
            <button type="button" onclick="restorePreviousBackup();"><%=Resources.BackupRestore_RestoreBackup %></button>
        <% } %>
    </div>
</body>
</html>
