using System;

using Xamarin.Forms;
namespace AppointmentControl
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using global::AppointmentControl.Data;
    using global::AppointmentControl.Models;

    public partial class CreateAppointment : ContentPage
    {
        PatientManager manager;

        public CreateAppointment()
        {
            InitializeComponent();

            manager = new PatientManager();
        }

        protected override async void OnAppearing()
        {
             base.OnAppearing();

            var list = await manager.GetPatientsAsync();

            foreach (var patients in list)
            {
                patientPicker.Items.Add(patients.Name);
            }
        }

        private void Save(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.FromHex("#FFF");
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}