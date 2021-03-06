﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;
    using System.Diagnostics;

    using global::AppointmentControl.Data;

    using global::AppointmentControl.Models;

    using Xamarin.Forms;

    /// <summary>
    /// The login.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Login : ContentPage
    {
        #region Fields
        private Entry password;
        private Entry userName;
        private UserManager usman;
        private ActivityIndicator activityIndicator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            this.usman = UserManager.Instance;

            var titleImage = new Image { Source = "medical.png", HeightRequest = 200 };

            // <StackLayout 1>
            var title = new Label
                            {
                                Text = "Appointment Control", 
                                FontSize = 25, 
                                HeightRequest = 35, 
                                TextColor = Color.FromHex("#12A5F4"), 
                                HorizontalTextAlignment = TextAlignment.Center, 
                                VerticalTextAlignment = TextAlignment.Center, 
                                FontAttributes = FontAttributes.Bold
                            };

            // </StackLayout 1>

            // <StackLayout 2>
            this.userName = new Entry
                               {
                                   Placeholder = "Username", 
                                   HorizontalTextAlignment = TextAlignment.Center, 
                                   BackgroundColor = Color.FromHex("#FFF"), 
                                   TextColor = Color.FromHex("#12A5F4"), 
                                   PlaceholderColor = Color.FromHex("#666666"), 
                                   HeightRequest = 45, 
                               };

            this.password = new Entry
                               {
                                   Placeholder = "Password", 
                                   HorizontalTextAlignment = TextAlignment.Center, 
                                   BackgroundColor = Color.FromHex("#FFF"), 
                                   TextColor = Color.FromHex("#12A5F4"), 
                                   PlaceholderColor = Color.FromHex("#666666"), 
                                   HeightRequest = 45, 
                                   IsPassword = true
                               };

            var login = new Button
                            {
                                Text = "LogIn", 
                                BackgroundColor = Color.FromHex("#2C903D"), 
                                TextColor = Color.FromHex("#FFF"), 
                            };

            // </StackLayout 2>

            // <StackLayout 3>
            var noAccount = new Label
                                {
                                    Text = "No account?", 
                                    VerticalOptions = LayoutOptions.Center, 
                                    TextColor = Color.FromHex("#12A5F4"), 
                                    FontAttributes = FontAttributes.Bold, 
                                    FontSize = 20
                                };

            var signUp = new Button
                             {
                                 Text = "SignUp", 
                                 BackgroundColor = Color.FromHex("#026CA5"), 
                                 TextColor = Color.FromHex("#FFF"), 
                             };

            var stack3 = new StackLayout
                             {
                                 Orientation = StackOrientation.Horizontal, 
                                 HorizontalOptions = LayoutOptions.Center, 
                                 Children = {
                                               noAccount, signUp 
                                            }
                             };

            var stack1 = new StackLayout
                             {
                                 Orientation = StackOrientation.Vertical,
                                 Padding = new Thickness(5,10,10,5),
                                 Children = {
                                               titleImage, title
                                            }, 
                                 HeightRequest = 180, 
                                 VerticalOptions = LayoutOptions.Center, 
                                 HorizontalOptions = LayoutOptions.Center
                             };

            var stack2 = new StackLayout
                             {
                                 Orientation = StackOrientation.Vertical, 
                                 Padding = new Thickness(20, 50, 20, 20), 
                                 Spacing = 25, 
                                 Children = {
                                               this.userName, this.password, login, stack3 
                                            }
                             };
            var mainStack = new StackLayout { Children = { stack1, stack2} };

            //this.Content = mainStack;
            activityIndicator = Util.CreateLoadingIndicator();
            var layout = Util.CreateAbsoluteLayout(mainStack, activityIndicator);
            Content = layout;

            // login.Clicked += (e, sender) => { Application.Current.MainPage = new PrincipalPage(); };
            login.Clicked += this.SignIn;

            signUp.Clicked += async (e, sender) => { await this.Navigation.PushModalAsync(new NavigationPage(new SignUpPage())); };
        }

        #endregion

        #region Methods

        /// <summary>
        /// The sign in.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        private async void SignIn(object sender, EventArgs eventArgs)
        {
            var user = this.userName.Text;
            var pass = this.password.Text;
            
            User userPass = null;
            if (user != null && pass != null)
            {
                activityIndicator.IsRunning = activityIndicator.IsVisible = true;
                userPass = await usman.FindUsernameAndPass(user, pass);
                
                activityIndicator.IsRunning = activityIndicator.IsVisible = false;
            }

            if (userPass == null)
            {
                await this.DisplayAlert("Usuario o contraseña incorrectos", "Ingrese sus credenciales o regístrese como nuevo usuario.", "Ok");
                password.Text = null;
                this.userName.Focus();
                return;
            }

            Application.Current.Properties.Add(Constants.UserPropertyName, userPass);
            Application.Current.MainPage = new PrincipalPage();
        }

        #endregion

        // async void SignUp(object sender, EventArgs eventArgs)
        // {
        // Application.Current.MainPage = new NavigationPage(new SignUpPage());

        // }
    }
}