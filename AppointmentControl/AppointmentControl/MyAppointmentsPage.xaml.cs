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

        class Appointments
        {
            public Appointments(string name, DateTime date, string reason, Color color)
            {
                this.Name = name;
                this.Date = date;
                this.Reason = reason;
                this.Color = color;
            }

            public string Name { private set; get; }

            public DateTime Date { private set; get; }

            public string Reason { private set; get; }
            public Color Color { private set; get; }
        };

        public MyAppointmentsPage()
        {
            Label header = new Label
                               {
                                   Text = "Appointments for today:",
                                   FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                   HorizontalOptions = LayoutOptions.Center
                               };

            List<Appointments> appointments = new List<Appointments>
                                                  {
                                                      new Appointments("Mr. Carlos Arroniz", new DateTime(2016,03,20,10,00,00,00), "Chronic headache", Color.FromHex("#ee9c00")),
                                                      new Appointments("Mrs. Beatriz Paredes", new DateTime(2016,03,20,11,30,00), "Painfull pain in legs xD", Color.FromHex("#f00"))
                                                  };

            ListView appointsList = new ListView
                                        {
                                            ItemsSource = appointments,

                                            ItemTemplate = new DataTemplate(() =>
                                                    {
                                                        Label nameLabel = new Label();
                                                        nameLabel.SetBinding(Label.TextProperty, "Name");

                                                        Label dateLabel = new Label();
                                                        dateLabel.SetBinding(Label.TextProperty, new Binding("Date", BindingMode.OneWay, null, null, "Appointment Date {0:d}"));

                                                        Label reasonLabel = new Label();
                                                        reasonLabel.SetBinding(Label.TextProperty, "Reason");

                                                        BoxView boxView = new BoxView();
                                                        boxView.SetBinding(BoxView.ColorProperty, "Color");

                                                        return new ViewCell
                                                                   {
                                                                       View = new StackLayout
                                                                                  {
                                                                                      Padding = 10,
                                                                                      Orientation = StackOrientation.Horizontal,
                                                                                      Children =
                                                                                          {
                                                                                              boxView, new StackLayout
                                                                                                           {
                                                                                                               BackgroundColor = Color.Green,
                                                                                                               VerticalOptions = LayoutOptions.Center,
                                                                                                               Spacing = 10,
                                                                                                               Children =
                                                                                                                   {
                                                                                                                       nameLabel,
                                                                                                                       dateLabel,
                                                                                                                       reasonLabel
                                                                                                                   }
                                                                                                           }
                                                                                          }
                                                                                  }
                                                                   };
                                                    })
                                        };
            this.Padding = new Thickness(10, Device.OnPlatform(20,0,0), 10, 5);

            this.Content = new StackLayout
                               {
                                   Children =
                                       {
                                           header, appointsList
                                       }
                               };

        }
    }
}
