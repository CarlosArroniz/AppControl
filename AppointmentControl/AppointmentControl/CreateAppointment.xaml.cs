using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
            InitializeComponent();

            StartHour.Time = new TimeSpan(
                System.DateTime.Now.Hour,
                System.DateTime.Now.Minute,
                System.DateTime.Now.Second);

            EndHour.Time = StartHour.Time.Add(new TimeSpan(1,0,0));

            userManager = UserManager.Instance;
            appointmentManager = AppointmentManager.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Date.MinimumDate = System.DateTime.Now;

            if (((User)Application.Current.Properties[Constants.UserPropertyName]).isdoctor)
            {
                FillPatientFilterPickers();
            }
            else
            {
                FillDoctorFilterPickers();
            }
        }

        private async void FillPatientFilterPickers()
        {
            ActIndicator.IsRunning = ActIndicator.IsVisible = true;
            patientList = await userManager.GetPatientsAsync();
            ActIndicator.IsRunning = ActIndicator.IsVisible = false;

            foreach (var patients in patientList)
            {
                namesPicker.Items.Add(patients.Name);
            }
            namesPicker.IsEnabled = true;
            namesPicker.IsVisible = true;

            citiesPicker.IsVisible = false;
            specialityPicker.IsVisible = false;

            pLabel.IsVisible = true;
            cLabel.IsVisible = false;
            sLabel.IsVisible = false;
            nLabel.IsVisible = false;
        }

        private async void FillDoctorFilterPickers()
        {
            ActIndicator.IsRunning = ActIndicator.IsVisible = true;
            doctorsList = await userManager.GetDoctorsAsync();
            ActIndicator.IsRunning = ActIndicator.IsVisible = false;
            
            SortedSet<string> citiesList = new SortedSet<string>();
            SortedSet<string> specialitiesList = new SortedSet<string>();
            foreach (var doctor in doctorsList)
            {
                Debug.WriteLine("FillDoctorFilterPickers {0} {1} {2}", doctor.Name, doctor.City, doctor.Speciality);
                if (!string.IsNullOrEmpty(doctor.City))
                {
                    citiesList.Add(doctor.City);
                }
                if (!string.IsNullOrEmpty(doctor.Speciality))
                {
                    specialitiesList.Add(doctor.Speciality);
                }
            }

            foreach (var city in citiesList)
            {
                Debug.WriteLine("FillDoctorFilterPickers city: {0}", city);
                citiesPicker.Items.Add(city);
            }

            foreach (var speciality in specialitiesList)
            {
                Debug.WriteLine("FillDoctorFilterPickers speciality: {0}", speciality);
                specialityPicker.Items.Add(speciality);
            }

            foreach (var doctor in doctorsList)
            {
                Debug.WriteLine("FillDoctorFilterPickers doctor name: {0}", doctor.Name);
                namesPicker.Items.Add(doctor.Name);
            }

            pLabel.IsVisible = false;
            cLabel.IsVisible = true;
            sLabel.IsVisible = true;
            nLabel.IsVisible = true;
        }

        private async void Save(object sender, EventArgs e)
        {
            if (!await ValidateFields())
            {
                return;
            }

            try
            {
                ActIndicator.IsRunning = ActIndicator.IsVisible = true;
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
            if (namesPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Invalid username", "You must select a name to save the appointment.", "Ok");
                namesPicker.Focus();
                return false;
            }

            if (Date.Date.Add(StartHour.Time) < System.DateTime.Now)
            {
                await DisplayAlert("Error on start hour",
                    "The appointment can't start in a past time",
                    "Ok");
                StartHour.Focus();
                return false;
            }

            if (StartHour.Time >= EndHour.Time)
            {
                if (await DisplayAlert("Termination hour wrong.",
                    "The appointment end hour can't be less or equal to the start hour. Do you want to change the appointment end hour?",
                    "Ok", "Cancel"))
                {
                    EndHour.Focus();
                }
                return false;
            }

            if (string.IsNullOrEmpty(Reason.Text))
            {
                await DisplayAlert("Invalid reason", "Please enter the reason of the appointment.", "Ok");
                Reason.Focus();
                return false;
            }

            return await ValidateWithDoctorAppointmens();
        }

        private async Task<bool> ValidateWithDoctorAppointmens()
        {
            User user = Application.Current.Properties[Constants.UserPropertyName] as User;
            
            var selectedAppointmentStart = Date.Date.Add(StartHour.Time);
            var selectedAppointmentEnd = Date.Date.Add(EndHour.Time);
            
            string doctorId = user.isdoctor ? user.Id : doctorsList[namesPicker.SelectedIndex].Id;
            var doctorAppointments = await appointmentManager.GetAppointmentsOfDoctorAsync(doctorId);


            foreach (var appointment in doctorAppointments)
            {
                var appointmentStart = DateTime.Parse(appointment.StartDate, new DateTimeFormatInfo());
                var appointmentEnd = DateTime.Parse(appointment.EndDate, new DateTimeFormatInfo());

                if ((appointment.status != Appointment.ACCEPTED && appointment.status != Appointment.REQUESTED) ||
                    (selectedAppointmentEnd <= appointmentStart || selectedAppointmentStart >= appointmentEnd))
                {
                    continue;
                }
                await DisplayAlert("Selected time unavailable.",
                    "The selected hour is already taken by another appointment.",
                    "Ok");
                return false;
            }
            return true;
        }

        private async Task<Appointment> CreateNewAppointment()
        {
            bool isDoctor = ((User)Application.Current.Properties[Constants.UserPropertyName]).isdoctor;

            Appointment appointment;
            if (isDoctor)
            {
                appointment = await CreateAppointmentAsDoctor();
            }
            else
            {
                appointment = await CreateAppointmentAsPatient();
            }

            appointment.Reason = Reason.Text;
            appointment.StartDate = Util.GetTimestamp(Date.Date, StartHour.Time);
            appointment.EndDate = Util.GetTimestamp(Date.Date, EndHour.Time);

            return appointment;

        }

        private async Task<Appointment> CreateAppointmentAsDoctor()
        {
            User selectedPatient = patientList[namesPicker.SelectedIndex];
            return new Appointment()
            {
                PatientId = selectedPatient.Id,
                status = Appointment.ACCEPTED,
                DoctorId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id
            };
        }

        private async Task<Appointment> CreateAppointmentAsPatient()
        {
            User selectedDoctor = doctorsList[namesPicker.SelectedIndex];
            return new Appointment()
            {
                DoctorId = selectedDoctor.Id,
                status = Appointment.REQUESTED,
                PatientId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id
            };
        }

        private async void CitiesPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("CitiesPicker_OnSelectedIndexChanged  citiesPicker.Items[citiesPicker.SelectedIndex]: " + citiesPicker.Items[citiesPicker.SelectedIndex]);
            var specsList = await userManager.GetUsersByCityAsync(citiesPicker.Items[citiesPicker.SelectedIndex]);
            Debug.WriteLine("CitiesPicker_OnSelectedIndexChanged  specsList: " + specsList.Count);

            specialityPicker.Items.Clear();
            foreach (var specs in specsList)
            {
                specialityPicker.Items.Add(specs.Speciality);
            }
        }

        private async void SpecialityPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("SpecialityPicker_OnSelectedIndexChanged  specialityPicker.Items[specialityPicker.SelectedIndex]: " + specialityPicker.Items[specialityPicker.SelectedIndex]);

            var namesList = await userManager.GetFiltersAsync(citiesPicker.Items[citiesPicker.SelectedIndex], specialityPicker.Items[specialityPicker.SelectedIndex]);

            foreach (var names in namesList)
            {
                namesPicker.Items.Add(names.Name);
            }
        }
    }
}
