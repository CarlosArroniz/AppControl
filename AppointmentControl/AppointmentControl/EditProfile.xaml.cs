using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        private void Save(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}
