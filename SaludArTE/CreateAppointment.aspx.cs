using System;
using System.Globalization;
using SaludArTE.BLL;
using SaludArTE.Models;
using SaludArTE.Properties;

namespace SaludArTE
{
    public partial class CreateAppointment : System.Web.UI.Page
    {
        private readonly Appointments _appointmentsBll;

        public CreateAppointment()
            : this(Global.CurrentContainer.GetInstance<Appointments>())
        {
            
        }

        public CreateAppointment(Appointments appointmentsBll)
        {
            _appointmentsBll = appointmentsBll;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                ProcessPostBack();

            this.lblNewAppointment.InnerText = Resources.Appointment_NewAppointment;

            var appointmentDate = DateTime.Parse(this.Request.QueryString["dt"], null, DateTimeStyles.RoundtripKind);
            this.CreateViewModel(appointmentDate);
        }

        private void ProcessPostBack()
        {
            var eventStartTime = DateTime.Parse(this.Request.Form["EventTime"], null, DateTimeStyles.RoundtripKind);
            var patientId = Guid.Parse(this.Request.Form["PatientId"]);
            var physicianId = Guid.Parse(this.Request.Form["PhysicianId"]);

            _appointmentsBll.CreateAppointment(eventStartTime, patientId, physicianId);
            this.CurrentUnitOfWork().DbContext.SaveChanges();
            Response.Redirect(Page.ResolveUrl("~/RequestAppointment.aspx"));
        }

        private void CreateViewModel(DateTime appointmentDate)
        {
            var currentUser = this.CurrentUser();
            this.ViewModel = new Appointment
            {
                Start = appointmentDate,
                End = appointmentDate.AddMinutes(10),
                Title = "Consulta con " + "Neurología",
                PatientId = currentUser.Sid,
                PatientName = currentUser.GivenName
            };
        }

        public Appointment ViewModel { get; set; }
    }
}