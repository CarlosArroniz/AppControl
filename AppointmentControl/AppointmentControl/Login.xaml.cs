// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// The login.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Login : ContentPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            var titleImage = new Image { Source = "medical.png", HeightRequest = 200 };

            // <StackLayout 1>
            var Title = new Label
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
            var userName = new Entry
                               {
                                   Placeholder = "Username", 
                                   HorizontalTextAlignment = TextAlignment.Center, 
                                   BackgroundColor = Color.FromHex("#FFF"), 
                                   TextColor = Color.FromHex("#12A5F4"), 
                                   PlaceholderColor = Color.FromHex("#666666"), 
                                   HeightRequest = 45, 
                               };

            var password = new Entry
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

            // </StackLayout 3>

            // <StackLayout 4>
            var forgotPass = new Button
                                 {
                                     Text = "Forgot your password?", 
                                     BackgroundColor = Color.Transparent, 
                                     VerticalOptions = LayoutOptions.Center, 
                                     TextColor = Color.FromHex("#12A5F4")
                                 };

            // </StackLayout 4>
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
                                 Children = {
                                               titleImage, Title 
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
                                               userName, password, login, stack3 
                                            }
                             };
            var stack4 = new StackLayout { Children = { forgotPass } };

            var mainStack = new StackLayout { Children = { stack1, stack2, stack4 } };

            this.Content = mainStack;

            login.Clicked += (e, sender) => { Application.Current.MainPage = new PrincipalPage(); };

            signUp.Clicked += async (e, sender) => { await Navigation.PushModalAsync(new NavigationPage(new SignUpPage())); };
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
        // ReSharper disable once CSharpWarnings::CS1998
        async void SignIn(object sender, EventArgs eventArgs)
        {


            var page = new NavigationPage(new Page());
            page.BarBackgroundColor = Color.FromHex("#004D40");
            page.BarTextColor = Color.White;

            Application.Current.MainPage = page;
        }

        #endregion

        // async void SignUp(object sender, EventArgs eventArgs)
        // {
        // Application.Current.MainPage = new NavigationPage(new SignUpPage());

        // }
    }
}