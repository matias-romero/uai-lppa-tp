<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SaludArTE.Login" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContainerPlaceHolder" runat="server">
    <form id="formMain" runat="server">
        <div style="padding: 20px; margin: auto; max-width: 500px">
            <div class="form-group">
                <label for="username" class="control-label"><%=Resources.Login_Username %></label>
                <input type="text" class="form-control" id="username" name="username" value="" required="" title="Please enter you username" placeholder="nombre_usuario" />
                <span class="help-block"></span>
            </div>
            <div class="form-group">
                <label for="password" class="control-label"><%=Resources.Login_Password %></label>
                <input type="password" class="form-control" id="password" name="password" value="" required="" title="Please enter your password" />
                <span class="help-block"></span>
            </div>
            <div id="validationSummary" class="alert alert-danger" role="alert" runat="server" visible="False">
                <%=Resources.Login_InvalidCredentials %>
            </div>
            <!--
            <div class="checkbox">
                <label>
                    <input type="checkbox" name="remember" id="remember" />
                    Remember login
                </label>
                <p class="help-block">(if this is a private computer)</p>
            </div>
            -->
            <button type="submit" class="btn btn-success btn-block"><%=Resources.Login_Submit %></button>
        </div>
    </form>
</asp:Content>
