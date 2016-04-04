// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignUpPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppointmentControl.Data;
using AppointmentControl.Models;

namespace AppointmentControl
{
    using Xamarin.Forms;

    using XLabs.Forms;
    using XLabs.Forms.Controls;
    using XLabs.Forms.Converter;

    /// <summary>
    /// The sign up page.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class SignUpPage : ContentPage
    {
        private readonly UserManager userManager;

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
        private Label specialityLabel;
        private Entry speciality;
        private Label emailLabel;
        private Entry email;
        private Label cityLabel;
        private Entry city;
        private Label stateLabel;
        private Entry state;
        private BindableRadioGroup radios;
        private ActivityIndicator activityIndicator;
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
            userManager = UserManager.Instance;
        }

        private void InitializeComponents()
        {
            signUpLabel = new Label
            {
                Text = "Sign Up",
                TextColor = Color.FromHex("#12A5F4"),
                BackgroundColor = Color.FromHex("#FFF"),
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
                TextColor = Color.FromHex("#12A5F4")
            };
            userName = new Entry
            {
                Placeholder = "Username",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666")
            };

            passLabel = new Label
            {
                Text = "Password",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            pass = new Entry
            {
                Placeholder = "Password",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                IsPassword = true
            };

            countryLabel = new Label
            {
                Text = "Country",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            countryPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.Center,
                Items = { "México", "EUA", "Canada" },
                Title = "Choose your country"
            };

            stateLabel = new Label
            {
                Text = "State",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            state = new Entry
            {
                Placeholder = "Los Angeles",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Text
            };

            cityLabel = new Label
            {
                Text = "City",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            city = new Entry
            {
                Placeholder = "California",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Text
            };

            phoneLabel = new Label
            {
                Text = "Cellphone",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            phone = new Entry
            {
                Placeholder = "999 999 9999",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Telephone
            };

            emailLabel = new Label
            {
                Text = "Email",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            email = new Entry
            {
                Placeholder = "somebody@example.com.mx",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Email
            };

            name = new Label
            {
                Text = "Name",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
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
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Text
            };

            addressLabel = new Label
            {
                Text = "Address",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#12A5F4")
            };

            address = new Entry
            {
                Placeholder = "Madero Av. 123 Centro",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Text
            };

            zipCodeLabel = new Label
            {
                Text = "Zip Code",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold
            };

            zipCode = new Entry
            {
                Placeholder = "58000",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center
            };

            save = new Button
            {
                Text = "Save",
                BackgroundColor = Color.FromHex("#2C903D"),
                TextColor = Color.FromHex("#FFF")
            };

            radios = new BindableRadioGroup
            {
                TextColor = Color.FromHex("#12A5F4"),
                FontSize = 20,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal
            };

            radios.ItemsSource = new[] { "Patient", "Medic" };

            specialityLabel = new Label
            {
                Text = "Speciality",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                IsVisible = false
            };

            speciality = new Entry
            {
                Placeholder = "Ophthalmologist",
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666"),
                Keyboard = Keyboard.Text,
                IsVisible = false
            };

            var stack1 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#FFF"),
                Padding = 15,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 10,
                Children =
                   {
                       signUpLabel, userLabel, userName,
                       passLabel, pass,
                       radios,
                       specialityLabel, speciality,
                       countryLabel, countryPicker,
                       stateLabel,state,
                       cityLabel, city,
                       phoneLabel, phone,
                       emailLabel,email,
                       name, nameEntry,
                       addressLabel, address,
                       zipCodeLabel, zipCode,
                       save
                   }
            };

            var scroll = new ScrollView { Content = stack1 };

            radios.CheckedChanged += (sender, i) =>
                {
                    if (this.radios.SelectedIndex.Equals(DOCTOR))
                    {
                        specialityLabel.IsVisible = true;
                        speciality.IsVisible = true;
                    }
                    else
                    {
                        specialityLabel.IsVisible = false;
                        speciality.IsVisible = false;
                    }
                };

            activityIndicator = Util.CreateLoadingIndicator();

            this.activityIndicator = new ActivityIndicator
                                         {
                                             HorizontalOptions = LayoutOptions.Center,
                                             VerticalOptions = LayoutOptions.Center,
                                             HeightRequest = 5,
                                             WidthRequest = 5,
                                             Color = Color.FromHex("#045309")
                                         };

            var layout = Util.CreateAbsoluteLayout(scroll, activityIndicator);
            Content = layout;

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
            if (!await ValidateFields())
            {
                return;
            }

            bool isDoc = radios.SelectedIndex == DOCTOR;

            var country = countryPicker.SelectedIndex;

            var user = new User()
            {
                Username = userName.Text,
                Password = pass.Text,
                Phone = phone.Text,
                Email = email.Text,
                City = city.Text,
                State = state.Text,
                Speciality = speciality.Text,
                isdoctor = isDoc,
                Address = address.Text,
                Zip = zipCode.Text,
                Name = nameEntry.Text,
                Country = countryPicker.Items.ElementAt(country)
            };

            activityIndicator.IsRunning = activityIndicator.IsVisible = true;
            await userManager.SaveTaskAsync(user);

            user = await userManager.FindUser(user);

            await Navigation.PushModalAsync(new Login());
        }

        private async Task<bool> ValidateFields()
        {
            if (!await ValidateUsername())
            {
                return false;
            }

            if (pass.Text == null)
            {
                await DisplayAlert("Contraseña vacía.",
                    "Favor de ingresar su contraseña.", "Ok");
                pass.Text = null;
                pass.Focus();
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUsername()
        {
            if (userName.Text == null || userName.Text.Length <= 0)
            {
                await DisplayAlert("Nombre de usuario inválido.",
                    "Favor de ingresar su nombre de usuario.", "Ok");
                userName.Text = null;
                userName.Focus();
                return false;
            }

            activityIndicator.IsRunning = activityIndicator.IsVisible = true;
            if (await userManager.FindUser(userName.Text) != null)
            {
                await DisplayAlert("Nombre de usuario ya existe.",
                    "Favor de seleccionar otro nombre de usuario.", "Ok");
                userName.Text = null;
                userName.Focus();
                activityIndicator.IsRunning = activityIndicator.IsVisible = false;
                return false;
            }
            activityIndicator.IsRunning = activityIndicator.IsVisible = false;
            return true;
        }

        #endregion
    }
}