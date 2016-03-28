using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppointmentControl.Data;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{

    public partial class CreateAppointment : ContentPage
    {
        private readonly UserManager userManager;
        private readonly AppointmentManager appointmentManager;
        private ObservableCollection<User> patientList;
        private ObservableCollection<User> doctorsList;

        public CreateAppointment()
        {
            var type = (User)Application.Current.Properties[Constants.UserPropertyName];

            InitializeComponent();

            StartHour.Time = new TimeSpan(
                System.DateTime.Now.Hour,
                System.DateTime.Now.Minute,
                System.DateTime.Now.Second);

            EndHour.Time = StartHour.Time;

            userManager = new UserManager();
            appointmentManager = new AppointmentManager();

            citiesPicker.SelectedIndexChanged += async (sender, args) =>
                {
                    var specsList = await userManager.GetFiltersAsync(citiesPicker.SelectedIndex.ToString());

                    foreach (var specs in specsList)
                    {
                        specialityPicker.Items.Add(specs.Speciality);
                    }
                };

            specialityPicker.SelectedIndexChanged += async (sender, args) =>
                {
                    var namesList = await userManager.GetFiltersAsync(specialityPicker.SelectedIndex.ToString());

                    foreach (var names in namesList)
                    {
                        namesPicker.Items.Add(names.Name);
                    }
                };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (((User)Application.Current.Properties[Constants.UserPropertyName]).isdoctor)
            {
                patientList = await userManager.GetPatientsAsync();

                namesPicker.IsEnabled = true;
                namesPicker.IsVisible = true;

                citiesPicker.IsVisible = false;
                specialityPicker.IsVisible = false;

                foreach (var patients in patientList)
                {
                    namesPicker.Items.Add(patients.Name);
                }
            }
            else
            {
                doctorsList = await userManager.GetDoctorsAsync();

                foreach (var doctor in doctorsList)
                {
                    citiesPicker.Items.Add(doctor.City);
                }
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
            bool isDoctor = ((User)Application.Current.Properties[Constants.UserPropertyName]).isdoctor;

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
            appointment.StartDate = GetTimestamp(Date.Date, StartHour.Time);
            appointment.EndDate = GetTimestamp(Date.Date, EndHour.Time);

            return appointment;

        }

        private string GetTimestamp(DateTime date, TimeSpan time)
        {
            return date.ToString("yyyy-MM-dd") + "T" + time.ToString("hh\\:mm\\:ss\\.ffff") + "+00:00";
        }

        private async Task<Appointment> CreateAppointmentAsDoctor()
        {
            User selectedPatient = patientList[namesPicker.SelectedIndex];
            return new Appointment()
            {
                PatientId = selectedPatient.Id,
                status = Appointment.REQUESTED,
                DoctorId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id
            };
        }

        private async Task<Appointment> CreateAppointmentAsPatient()
        {
            User selectedDoctor = doctorsList[namesPicker.SelectedIndex];
            return new Appointment()
            {
                DoctorId = selectedDoctor.Id,
                status = Appointment.ACCEPTED,
                PatientId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id
            };
        }
    }
}
