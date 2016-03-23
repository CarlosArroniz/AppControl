using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppointmentControl.Data;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class CreateAppointment : ContentPage
    {
        private readonly DoctorManager doctorManager;
        private readonly PatientManager patientManager;
        private readonly AppointmentManager appointmentManager;

        public CreateAppointment()
        {
            InitializeComponent();
            doctorManager = new DoctorManager();
            patientManager = new PatientManager();
            appointmentManager = new AppointmentManager();
        }

        private async void Save(object sender, EventArgs e)
        {
            if (!await ValidateFields())
            {
                return;
            }

            try
            {
                Appointment appointment = await CreateNewAppointment();
                await appointmentManager.SaveTaskAsync(appointment);

                BackgroundColor = Color.FromHex("#FFF");
                Application.Current.MainPage = new PrincipalPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                Debug.WriteLine(@"ERROR {0}", ex);
            }
        }

        private async Task<bool> ValidateFields()
        {
            if (StartHour.Time >= EndHour.Time)
            {
                if (await DisplayAlert("Error en la hora.",
                    "La hora de término no debe ser menor o igual a la hora de inicio. ¿Desea cambiar la hora de término de la cita?",
                    "Ok", "Cancel"))
                {
                    EndHour.Focus();
                }
                return false;
            }
            return true;
        }

        private async Task<Appointment> CreateNewAppointment()
        {
            bool isDoctor = true;
            Appointment appointment = null;
            if (isDoctor)
            {
                appointment = await CreateAppointmentAsDoctor();
            }
            else
            {
                appointment = await CreateAppointmentAsPatient();
            }

            appointment.Reason = Reason.Text;
            string appointmentStartDate = GetTimestamp(Date.Date, StartHour.Time);
            string appointmentEndDate = GetTimestamp(Date.Date, EndHour.Time);
            appointment.StartDate = appointmentStartDate;
            appointment.EndDate = appointmentEndDate;

            return appointment;

        }

        private string GetTimestamp(DateTime date, TimeSpan time)
        {
            return date.ToString("yyyy-MM-dd") + "T" + time.ToString("hh\\:mm\\:ss\\.ffff") + "+00:00";
        }

        private async Task<Appointment> CreateAppointmentAsPatient()
        {
            //List<Patient> patientResult = await patientManager.FindPatientByName(PatientName.Text);
            
            return new Appointment()
            {
                DoctorId = "doctor_id", //change for global value
                status = Appointment.ACCEPTED,
              //  PatientId = patientResult.First().Id
                PatientId = "dummy"
            };
            throw new NotImplementedException();
        }

        private async Task<Appointment> CreateAppointmentAsDoctor()
        {
            return new Appointment()
            {
                PatientId = "patient_id", //change for global value
                status = Appointment.REQUESTED,
                DoctorId = "doctor_id" // get the doctor object or doctor_id before create the appointment
            };
        }
    }
}
