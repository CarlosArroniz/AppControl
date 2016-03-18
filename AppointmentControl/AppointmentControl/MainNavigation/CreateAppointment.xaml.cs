namespace AppointmentControl.MainNavigation
{
    using System;

    using Xamarin.Forms;

    public partial class CreateAppointment : ContentPage
    {
        public CreateAppointment()
        {
            InitializeComponent();
        }

        private void Save(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.FromHex("#FFF");
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}
