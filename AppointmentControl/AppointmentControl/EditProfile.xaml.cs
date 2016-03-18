using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromHex("#FFF");

            //<StackLayout 2>

            //</StackLayout 2>
        }

        private void Save(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}
