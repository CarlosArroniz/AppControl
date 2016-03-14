using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class MyAppointmentsPage : ContentPage
    {
        

        public MyAppointmentsPage()
        {
            var Title = new Label()
                            {
                                Text = "My today Appointments"
                            };

            var Entry = new Entry() { Placeholder = "I am an entry", BackgroundColor = Color.White };

            Content = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                BackgroundColor = Color.FromHex("#12A5F4"),
                                WidthRequest = 100,
                                HeightRequest = 100,
                                Children = { Title, Entry }
                            };
        }

        async void OnNextPageButtonClicked(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new MyProfilePage());
        }
    }
}
