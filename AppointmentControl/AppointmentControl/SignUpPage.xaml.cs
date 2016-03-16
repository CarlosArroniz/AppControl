// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignUpPage.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The sign up page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;

    using Xamarin.Forms;

    using XLabs.Forms.Controls;

    /// <summary>
    /// The sign up page.
    /// </summary>
    public partial class SignUpPage : ContentPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage"/> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
            #region User Fields

            Label signUpLabel = new Label
            {
                Text = "Sign Up",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#12A5F4"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };

            Label userLabel = new Label
            {
                Text = "Username",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };
            Entry userName = new Entry
            {
                Placeholder = "Username",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5")
            };

            Label passLabel = new Label
            {
                Text = "Password",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            Entry pass = new Entry
            {
                Placeholder = "Password",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5")
            };
            #endregion

            #region User Localization
            Label countryLabel = new Label
            {
                Text = "Country",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            Picker countryPicker = new Picker
            {
                Items = { "México", "EUA", "Canada" },
                Title = "Choose your country"
            };

            Label phoneLabel = new Label
            {
                Text = "Cellphone",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            Entry phone = new Entry
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

            Label name = new Label
            {
                Text = "Name",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#12A5F4"),
                TextColor = Color.FromHex("#FFF"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };

            Entry nameEntry = new Entry
            {
                Placeholder = "Juan Perez Lopez",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5"),
                Keyboard = Keyboard.Text
            };


            Label addressLabel = new Label
            {
                Text = "Address",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            Entry address = new Entry
            {
                Placeholder = "Madero Av. 123 Centro",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5"),
                Keyboard = Keyboard.Text
            };

            Label zipCodeLabel = new Label
            {
                Text = "Zip Code",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#12A5F4"),
                TextColor = Color.FromHex("#FFF"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };

            Entry zipCode = new Entry
            {
                Placeholder = "58000",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Numeric
            };

            Button save = new Button
            {
                Text = "Save",
                BackgroundColor = Color.FromHex("#2C903D"),
                TextColor = Color.FromHex("#FFF"),
                Command = new Command(() => Navigation.PushAsync(new SignUpPage()))
            };

            CustomRadioButton radioMedico = new CustomRadioButton
            {
                Text = "Medico",
                TextColor = Color.FromHex("#FFF"),
            };
            CustomRadioButton radioPaciente = new CustomRadioButton
            {
                Text = "Paciente",
                TextColor = Color.FromHex("#FFF")
            };

            BindableRadioGroup radios = new BindableRadioGroup
                                            {
                                                Items = { radioPaciente, radioMedico},
                                                HorizontalOptions = LayoutOptions.Center
                                            };
            #endregion

            StackLayout stack1 = new StackLayout
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
            Content = scroll;

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