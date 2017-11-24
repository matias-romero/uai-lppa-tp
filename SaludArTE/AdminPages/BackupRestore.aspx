<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="SaludArTE.AdminPages.BackupRestore"%>
<asp:Content ID="Content4" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <div ID="lblCorruptedDatabase" runat="server" Visible="False" class="alert alert-warning" role="alert">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <span>Se detectaron inconsistencias en las siguientes entidades:</span>
        <ul ID="lblAffectedEntities" runat="server"></ul>
    </div>
    <form class="form-inline">
        <div class="form-group">
            <label for="txtBackupName">Nombre</label>
            <input type="text" class="form-control" id="txtBackupName" name="txtBackupName" />
        </div>
        <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-default" OnClick="btnBackup_OnClick"/>
    </form>
    
    <form class="form">
        <asp:GridView ID="dtGridView" runat="server" AutoGenerateSelectButton="True" EnableModelValidation="False" OnSelectedIndexChanged="dtGridView_SelectedIndexChanged">
        </asp:GridView>
    </form>
</asp:Content>
