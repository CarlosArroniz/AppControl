using System;
using System.Diagnostics;
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

            StartHour.Time = new TimeSpan(
                System.DateTime.Now.Hour,
                System.DateTime.Now.Minute,
                System.DateTime.Now.Second
                );

            EndHour.Time = StartHour.Time;

            doctorManager = new DoctorManager();
            patientManager = new PatientManager();
            appointmentManager = new AppointmentManager();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var list = await patientManager.GetPatientsAsync();

            foreach (var patients in list)
            {
                patientPicker.Items.Add(patients.Name);
            }
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
            bool isDoctor = ((User) Application.Current.Properties[Constants.UserPropertyName]).isdoctor;
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
                DoctorId = ((User) Application.Current.Properties[Constants.UserPropertyName]).Id,
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
                PatientId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id,
                status = Appointment.REQUESTED,
                DoctorId = "doctor_id" // get the doctor object or doctor_id before create the appointment
            };
        }
    }
}
