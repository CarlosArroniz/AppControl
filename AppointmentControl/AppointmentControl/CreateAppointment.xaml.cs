using System;
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
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}
