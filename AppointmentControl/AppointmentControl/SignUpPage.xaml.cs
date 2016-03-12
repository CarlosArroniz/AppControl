using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        public void Save(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new Page1()); 
            Application.Current.MainPage = new Login();
        }
    }
}
