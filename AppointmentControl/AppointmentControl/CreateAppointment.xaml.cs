using System;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class CreateAppointment : ContentPage
    {
        public CreateAppointment()
        {
            InitializeComponent();
        }

        private void Save(object sender, EventArgs e)
        {
            CreateAppointmentAsDoctor();
            CreateAppointmentAsPatient();

            this.BackgroundColor = Color.FromHex("#FFF");
            Application.Current.MainPage = new PrincipalPage();
        }

        private void CreateAppointmentAsPatient()
        {
            throw new NotImplementedException();
        }

        private void CreateAppointmentAsDoctor()
        {
            throw new NotImplementedException();
        }
    }
}
