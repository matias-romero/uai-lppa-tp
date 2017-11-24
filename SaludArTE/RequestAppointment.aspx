<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="RequestAppointment.aspx.cs" Inherits="SaludArTE.RequestAppointment" %>
<asp:Content ID="ExtraScripts" ContentPlaceHolderID="HeadAreaPlaceHolder" runat="server">
    <link href="<%=Page.ResolveUrl("~/Resources/fullcalendar/fullcalendar.min.css") %>" rel="stylesheet" />
    <!--<script src="<%=Page.ResolveUrl("~/Resources/jquery/jquery.min.js") %>" src='lib/jquery.min.js'></script>-->
    <script src="<%=Page.ResolveUrl("~/Resources/fullcalendar/moment.min.js") %>"></script>
    <script src="<%=Page.ResolveUrl("~/Resources/fullcalendar/fullcalendar.min.js") %>"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainFormAreaPlaceholder" runat="server">
    <div id="calendar"></div>
    <script>
        $(document).ready(function () {
            $('#calendar').fullCalendar({
                header : {
                    left: 'month,day',
                    center: 'title',
                    right: 'today prev,next'
                },
                nowIndicator : "true",
                events : 'api/appointments',
                dayClick: function (date, jsEvent, view) {
                    var dateString = date.toISOString();
                    if (view.name !== 'agendaDay') {
                        $('#calendar').fullCalendar('changeView', 'agendaDay', dateString);
                    } else {
                        window.location.href = '<%=Page.ResolveClientUrl("~/CreateAppointment.aspx")%>?dt=' + encodeURIComponent(dateString);
                    }
                },
                eventClick: function (calEvent, jsEvent, view) {

                    alert('Event: ' + calEvent.title);
                    alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                    alert('View: ' + view.name);

                    // change the border color just for fun
                    $(this).css('border-color', 'red');

                }
            });
        });
    </script>
</asp:Content>
