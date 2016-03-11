using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void Save(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new Page1()); 
            Application.Current.MainPage = new Login();
        }
    }
}
