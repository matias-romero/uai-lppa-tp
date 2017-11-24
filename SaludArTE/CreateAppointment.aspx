<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="CreateAppointment.aspx.cs" Inherits="SaludArTE.CreateAppointment" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <h1 ID="lblNewAppointment" runat="server"></h1>
    <h2><%=ViewModel.Title%></h2>
    <form method="post">
        <div class="form-group">
            <label class="col-sm-2 control-label">Paciente</label>
            <div class="col-sm-10">
                <p class="form-control-static"><%=ViewModel.PatientName%></p>
                <input type="hidden" name="PatientId" value="<%=ViewModel.PatientId.ToString()%>"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Profesional</label>
            <div class="col-sm-10">
                <p class="form-control-static"><%="Carlos Gonzales"%></p>
                <input type="hidden" name="PhysicianId" value="<%=Guid.Empty.ToString()%>"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Horario</label>
            <div class="col-sm-10">
                <p class="form-control-static"><%=ViewModel.Start.ToLongDateString()%></p>
                <input type="hidden" name="EventTime" value="<%=ViewModel.Start.ToString("O")%>"/>
            </div>
        </div>
        <button type="submit" class="btn btn-default">Confirmar</button>
        <button type="button" class="btn btn-default" onclick="returnToAgenda()">Cancelar</button>
    </form>
    <script>
        function returnToAgenda() {
            window.location.href = '<%=Page.ResolveClientUrl("~/RequestAppointment.aspx")%>';
        }
    </script>
</asp:Content>
