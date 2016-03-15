using System;
using System.Collections.Generic;

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

            this.BackgroundColor = Color.FromHex("#FFF");

            Label header = new Label
                               {
                                   FontAttributes = FontAttributes.Italic,
                                   Text = "Appointments for today:",
                                   FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                   HorizontalOptions = LayoutOptions.Center,
                                   TextColor = Color.FromHex("#FFF"),
                                   VerticalOptions = LayoutOptions.Center
                               };

            List<Appointments> appointments = new List<Appointments>
                                                  {
                                                      new Appointments("Mr. Carlos Arroniz", new DateTime(2016,03,20,10,00,00,00), "Chronic headache", Color.FromHex("#ee9c00")),
                                                      new Appointments("Mr. Mauricio Arellano", new DateTime(2016,03,20,11,30,00), "Leg injury", Color.FromHex("#f00")),
                                                      new Appointments("Mr. Jose Nuñez", new DateTime(2016,03,20,11,30,00), "Nose Bleeding", Color.FromHex("#ee9c00")),
                                                      new Appointments("Mr. Bruno Corona", new DateTime(2016,03,20,11,30,00), "Severe Stomachache", Color.FromHex("#f00"))
                                                  };
            ListView appointsList = new ListView
                                        {
                                            SeparatorVisibility = SeparatorVisibility.Default,
                                            SeparatorColor = Color.White,
                                            ItemsSource = appointments,
                                            RowHeight = 100,
                                            WidthRequest = 150,
                                            
                                            ItemTemplate = new DataTemplate(() =>
                                                {
                                                    Label nameLabel = new Label
                                                    {
                                                        FontSize = 15,
                                                        FontAttributes = FontAttributes.Bold,
                                                        HorizontalTextAlignment = TextAlignment.Center,
                                                        TextColor = Color.FromHex("#FFF"),
                                                    };
                                                    nameLabel.SetBinding(Label.TextProperty, "Name");

                                                    Label dateLabel = new Label
                                                    {
                                                        FontSize = 15,
                                                        FontAttributes = FontAttributes.Bold,
                                                        HorizontalTextAlignment = TextAlignment.Center,
                                                        TextColor = Color.FromHex("#FFF"),
                                                    };
                                                    dateLabel.SetBinding(Label.TextProperty, new Binding("Date", BindingMode.OneWay, null, null, "Appointment Date {0:d}"));

                                                    Label reasonLabel = new Label
                                                    {
                                                        FontSize = 15,
                                                        FontAttributes = FontAttributes.Italic,
                                                        HorizontalTextAlignment = TextAlignment.Center,
                                                        TextColor = Color.FromHex("#FFF")
                                                    };
                                                    reasonLabel.SetBinding(Label.TextProperty, "Reason");

                                                    BoxView boxView = new BoxView();

                                                    boxView.SetBinding(BoxView.ColorProperty, "Color");

                                                    return new ViewCell
                                                               {
                                                                   View = new StackLayout
                                                                              {
                                                                                  Orientation = StackOrientation.Horizontal,
                                                                                  Children =
                                                                                          {
                                                                                              boxView, new StackLayout
                                                                                                           {
                                                                                                               BackgroundColor = Color.FromHex("#808080"),
                                                                                                               WidthRequest = 300,
                                                                                                               HeightRequest = 150,
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
            StackLayout content = new StackLayout{
                                   Padding = 25,
                                   Children =
                                       {
                                           header, appointsList
                                       }
                               };

            this.Content = new ScrollView
                               {
                                   Content = content
                               };
        }
    }
}