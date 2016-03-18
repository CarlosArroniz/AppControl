// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignUpPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using AppointmentControl.Data;
using AppointmentControl.Models;
using XLabs.Forms;

namespace AppointmentControl
{
    using Xamarin.Forms;

    using XLabs.Forms.Controls;

    /// <summary>
    /// The sign up page.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class SignUpPage : ContentPage
    {
        private readonly UserManager userManager;
        private readonly DoctorManager doctorManager;
        private readonly PatientManager patientManager;

        private Label signUpLabel;
        private Label userLabel;
        private Entry userName;
        private Label passLabel;
        private Entry pass;
        private Label countryLabel;
        private Picker countryPicker;
        private Label phoneLabel;
        private Entry phone;
        private Label name;
        private Entry nameEntry;
        private Label addressLabel;
        private Entry address;
        private Label zipCodeLabel;
        private Entry zipCode;
        private Button save;
        private BindableRadioGroup radios;
        private const int PATIENT = 0;
        private const int DOCTOR = 1;
        private const int HOSPITAL = 2;

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage"/> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponents();
            userManager = new UserManager();
            doctorManager = new DoctorManager();
            patientManager = new PatientManager();
        }

        private void InitializeComponents()
        {
            signUpLabel = new Label
            {
                Text = "Sign Up",
                TextColor = Color.FromHex("#FFF"),
                BackgroundColor = Color.FromHex("#12A5F4"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };

            userLabel = new Label
            {
                Text = "Username",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };
            userName = new Entry
            {
                Placeholder = "Username",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5")
            };

            passLabel = new Label
            {
                Text = "Password",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            pass = new Entry
            {
                Placeholder = "Password",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5")
            };

            countryLabel = new Label
            {
                Text = "Country",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            countryPicker = new Picker
            {
                Items = { "México", "EUA", "Canada" },
                Title = "Choose your country"
            };

            phoneLabel = new Label
            {
                Text = "Cellphone",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            phone = new Entry
            {
                Placeholder = "999 999 9999",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5"),
                Keyboard = Keyboard.Telephone
            };

            name = new Label
            {
                Text = "Name",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#12A5F4"),
                TextColor = Color.FromHex("#FFF"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };

            nameEntry = new Entry
            {
                Placeholder = "Juan Perez Lopez",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5"),
                Keyboard = Keyboard.Text
            };

            addressLabel = new Label
            {
                Text = "Address",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#FFF")
            };

            address = new Entry
            {
                Placeholder = "Madero Av. 123 Centro",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#026CA5"),
                Keyboard = Keyboard.Text
            };

            zipCodeLabel = new Label
            {
                Text = "Zip Code",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#12A5F4"),
                TextColor = Color.FromHex("#FFF"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };

            zipCode = new Entry
            {
                Placeholder = "58000",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#000"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Numeric
            };

            save = new Button
            {
                Text = "Save",
                BackgroundColor = Color.FromHex("#2C903D"),
                TextColor = Color.FromHex("#FFF")
            };

            radios = new BindableRadioGroup
            {
                TextColor = Color.FromHex("#FFF"),
                FontSize = 20,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal
            };

            radios.ItemsSource = new[] { "Paciente", "Medico", "Clinica" };

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

            save.Clicked += async (sender, args) => Save();
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
        public async void Save()
        {
            var user = new User()
            {
                Username = userName.Text,
                Password = pass.Text,
                Phone = phone.Text
            };
            await userManager.SaveTaskAsync(user);

            //radios.ItemsSource = new[] { "Paciente", "Medico", "Clinica" };
            switch (radios.SelectedIndex)
            {
                case PATIENT:
                    var patient = new Patient()
                    {
                        Address = address.Text,
                        Country = countryPicker.Items.ToArray()[countryPicker.SelectedIndex],
                        Name = nameEntry.Text,
                        Zip = zipCode.Text
                    };
                    await patientManager.SaveTaskAsync(patient);
                    break;
                case DOCTOR:
                    var doctor = new Doctor()
                    {
                        Address = address.Text,
                        Country = countryPicker.Items.ToArray()[countryPicker.SelectedIndex],
                        Name = nameEntry.Text,
                        Zip = zipCode.Text
                    };
                    await doctorManager.SaveTaskAsync(doctor);
                    break;
                case HOSPITAL:
                default:
                    break;
            }
            
            // Application.Current.MainPage = new NavigationPage(new Page1()); 
            //Application.Current.MainPage = new Login();
            await this.Navigation.PushModalAsync(new Login());
        }

        #endregion
    }
}