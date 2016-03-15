using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
        }

        private void EditProfile(object sender, EventArgs e)
        {
            Application.Current.MainPage = new EditProfile();
        }
    }
}
