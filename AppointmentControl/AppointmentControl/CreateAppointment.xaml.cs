﻿using System;
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

        public CreateAppointment()
        {
            InitializeComponent();

            StartHour.Time = new TimeSpan(
                System.DateTime.Now.Hour,
                System.DateTime.Now.Minute,
                System.DateTime.Now.Second);

            EndHour.Time = StartHour.Time;

            userManager = new UserManager();
            appointmentManager = new AppointmentManager();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (((User) Application.Current.Properties[Constants.UserPropertyName]).isdoctor)
            {
                patientList = await userManager.GetPatientsAsync();
                patientPicker.IsEnabled = true;
                patientPicker.IsVisible = true;
                foreach (var patients in patientList)
                {
                    patientPicker.Items.Add(patients.Name);
                }
            }
            else
            {
                patientPicker.IsEnabled = false;
                patientPicker.IsVisible = false;
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
            User selectedPatient = patientList[patientPicker.SelectedIndex];
            return new Appointment()
            {
                PatientId = selectedPatient.Id,
                status = Appointment.REQUESTED,
                DoctorId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id
            };
        }

        private async Task<Appointment> CreateAppointmentAsPatient()
        {
            return new Appointment()
            {
                DoctorId = "doctor_id",
                status = Appointment.ACCEPTED,
              //  PatientId = patientResult.First().Id
                PatientId = ((User) Application.Current.Properties[Constants.UserPropertyName]).Id
            };
            throw new NotImplementedException();
        }
    }
}
