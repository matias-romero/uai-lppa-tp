<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="SaludArTE.AdminPages.Log" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/AdminPages/Menu.ascx" %>

<asp:Content ID="NavArea"  ContentPlaceHolderID="NavAreaPlaceHolder" runat="server">
    <uc1:Menu runat="server" id="Menu" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <th><%=Resources.Log_Date%></th>
                <th><%=Resources.Log_Username%></th>
                <th><%=Resources.Log_Description%></th>
            </tr>
        </thead>
        <tbody id="logRecords">
        </tbody>
    </table>
    <script>
        $(function() {
            $.getJSON("<%=Page.ResolveUrl("~/api/Logs")%>", function (logEntries) {
                var items = [];
                $.each(logEntries, function (key, entry) {
                    items.push('<tr id="' + entry.Id + '"><td>' + entry.Date + '</td><td>' + entry.Username + '</td><td>' + entry.Description + '</td></tr>');
                });

                $("#logRecords").html(items.join(""));
            });
        });
    </script>
</asp:Content>
