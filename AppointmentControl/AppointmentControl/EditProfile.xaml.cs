using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    using System.Collections.Generic;
    using System.Threading;

    public partial class EditProfile : ContentPage
    {
        public ListView Menu { set; get; }

        public EditProfile()
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromHex("#FFF");

            //<StackLayout 2>
            var title = new Label
            {
                Text = "Edit Profile",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            };
            //</StackLayout 2>

            //<Stacklayout3>
            var nombreLbl = new Label
            {
                Text = "Name",
                FontSize = 20,

                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var nameEtry = new Entry
            {
                Placeholder = "Your name!",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666666")
            };

            var especialidadLbl = new Label
            {
                Text = "Speciality",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var specialityEntry = new Entry
            {
                Placeholder = "Your name!",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666666")
            };

            var phoneLabe = new Label
            {
                Text = "Phone",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var phoneEntry = new Entry
            {
                Placeholder = "Your Phone!",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666666"),
                Keyboard = Keyboard.Telephone
            };

            var addressLabel = new Label
            {
                Text = "Address",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var adressEntry = new Entry
            {
                Placeholder = "Address!",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666666")
            };

            var countryLabel = new Label
            {
                Text = "Address",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var countryPicker = new Picker
            {
                BackgroundColor = Color.FromHex("#FFF"),
                Items = { "Mexico", "Canada", "España" }
            };

            var provinceLabel = new Label
            {
                Text = "Province",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var provincePicker = new Picker
            {
                BackgroundColor = Color.FromHex("#FFF"),
                Items = { "Michoacán", "Hidalgo", "Veracruz" }
            };

            var cityLabel = new Label
            {
                Text = "City",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var cityPicker = new Picker
            {
                BackgroundColor = Color.FromHex("#FFF"),
                Items = { "Morelia", "Zamora", "Quiroga" }
            };

            var zipCodeLabel = new Label
            {
                Text = "Zip Code",
                FontSize = 20,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Start,
                FontAttributes = FontAttributes.Bold
            };

            var zipCodeEntry = new Entry
            {
                Placeholder = "Zip Code!",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666666")
            };

            //</Stacklayout3>
        }

        private void Save(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PrincipalPage();
        }
    }
}
