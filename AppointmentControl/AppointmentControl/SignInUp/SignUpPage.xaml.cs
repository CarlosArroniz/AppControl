// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignUpPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl.SignInUp
{
    using System;

    using Xamarin.Forms;

    using XLabs.Forms.Controls;

    /// <summary>
    /// The sign up page.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class SignUpPage : ContentPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage"/> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
            

            var signUpLabel = new Label
            {
                Text = "Sign Up", 
                TextColor = Color.FromHex("#FFF"), 
                BackgroundColor = Color.FromHex("#12A5F4"), 
                HorizontalTextAlignment = TextAlignment.Center, 
                VerticalTextAlignment = TextAlignment.Center, 
                FontAttributes = FontAttributes.Bold, 
                FontSize = 20
            };

            var userLabel = new Label
            {
                Text = "Username", 
                FontSize = 20, 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromHex("#FFF")
            };
            var userName = new Entry
            {
                Placeholder = "Username", 
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#12A5F4"), 
                PlaceholderColor = Color.FromHex("#026CA5")
            };

            var passLabel = new Label
            {
                Text = "Password", 
                FontSize = 20, 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromHex("#FFF")
            };

            var pass = new Entry
            {
                Placeholder = "Password", 
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#12A5F4"), 
                PlaceholderColor = Color.FromHex("#026CA5")
            };
            

            #region User Localization
            var countryLabel = new Label
            {
                Text = "Country", 
                FontSize = 20, 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromHex("#FFF")
            };

            var countryPicker = new Picker
            {
                Items = {
                           "México", "EUA", "Canada" 
                        }, 
                Title = "Choose your country"
            };

            var phoneLabel = new Label
            {
                Text = "Cellphone", 
                FontSize = 20, 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromHex("#FFF")
            };

            var phone = new Entry
            {
                Placeholder = "999 999 9999", 
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#12A5F4"), 
                PlaceholderColor = Color.FromHex("#026CA5"), 
                Keyboard = Keyboard.Telephone
            };
            #endregion

            #region UserData

            var name = new Label
            {
                Text = "Name", 
                FontSize = 20, 
                BackgroundColor = Color.FromHex("#12A5F4"), 
                TextColor = Color.FromHex("#FFF"), 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
            };

            var nameEntry = new Entry
            {
                Placeholder = "Juan Perez Lopez", 
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#12A5F4"), 
                PlaceholderColor = Color.FromHex("#026CA5"), 
                Keyboard = Keyboard.Text
            };


            var addressLabel = new Label
            {
                Text = "Address", 
                FontSize = 20, 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
                TextColor = Color.FromHex("#FFF")
            };

            var address = new Entry
            {
                Placeholder = "Madero Av. 123 Centro", 
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#12A5F4"), 
                PlaceholderColor = Color.FromHex("#026CA5"), 
                Keyboard = Keyboard.Text
            };

            var zipCodeLabel = new Label
            {
                Text = "Zip Code", 
                FontSize = 20, 
                BackgroundColor = Color.FromHex("#12A5F4"), 
                TextColor = Color.FromHex("#FFF"), 
                HorizontalTextAlignment = TextAlignment.Start, 
                VerticalTextAlignment = TextAlignment.End, 
                FontAttributes = FontAttributes.Bold, 
            };

            var zipCode = new Entry
            {
                Placeholder = "58000", 
                BackgroundColor = Color.FromHex("#FFF"), 
                TextColor = Color.FromHex("#000"), 
                PlaceholderColor = Color.FromHex("#666"), 
                Keyboard = Keyboard.Numeric
            };

            var save = new Button
            {
                Text = "Save", 
                BackgroundColor = Color.FromHex("#2C903D"), 
                TextColor = Color.FromHex("#FFF"), 
            };

            var radios = new BindableRadioGroup
            {
                TextColor = Color.FromHex("#FFF"), 
                FontSize = 20, 
                VerticalOptions = LayoutOptions.CenterAndExpand, 
                HorizontalOptions = LayoutOptions.CenterAndExpand, 
                Orientation = StackOrientation.Horizontal
            };

            radios.ItemsSource = new[] { "Paciente", "Medico", "Clinica" };

            #endregion

            var stack1 = new StackLayout
            {   
               Orientation = StackOrientation.Vertical, 
               BackgroundColor = Color.FromHex("#12A5F4"), 
               Padding = 15, 
               VerticalOptions = LayoutOptions.Center, 
               Spacing = 10, 
               Children =
                   {
                       signUpLabel, userLabel, userName, 
                       passLabel, pass, 
                       radios, 
                       countryLabel, countryPicker, 
                       phoneLabel, phone, 
                       name, nameEntry, 
                       addressLabel, address, 
                       zipCodeLabel, zipCode, 
                       save
                   }
            };

            var scroll = new ScrollView { Content = stack1 };

            this.Content = scroll;

            save.Clicked += async (sender, args) => await this.Navigation.PushModalAsync(new Login());
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void Save(object sender, EventArgs e)
        {
            // Application.Current.MainPage = new NavigationPage(new Page1()); 
            Application.Current.MainPage = new Login();
        }

        #endregion
    }
}