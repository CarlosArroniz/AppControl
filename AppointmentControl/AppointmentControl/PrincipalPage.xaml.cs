using Xamarin.Forms;
namespace AppointmentControl
{
    public partial class PrincipalPage : TabbedPage
    {
        public PrincipalPage()
        {
            this.ItemsSource = new NamedColor[]
                                   {
                                       new NamedColor("Today", Color.Green),
                                       new NamedColor("Schedule", Color.Red),
                                       new NamedColor("Settings", Color.Navy)
                                   };
            this.ItemTemplate = new DataTemplate(() => { return new NamedColorPage(); });
            


        }

        class NamedColor
        {
            public NamedColor(string name, Color color)
            {
                this.Name = name;
                this.Color = color;
            }

            public string Name { private set; get; }
            public Color Color { private set; get; }

            public override string ToString()
            {
                return Name;
            }
        }

        class NamedColorPage : ContentPage
        {
            public NamedColorPage()
            {
                this.SetBinding(ContentPage.TitleProperty, "Name");

                BoxView boxView = new BoxView
                                      {
                                          WidthRequest = 100,
                                          HeightRequest = 100,
                                          HorizontalOptions = LayoutOptions.Center
                                      };
                boxView.SetBinding(BoxView.ColorProperty, "Color");

                this.Content = boxView;
            }
        }
    }
}
