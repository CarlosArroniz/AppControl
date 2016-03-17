// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The login.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;
    using System.Runtime.InteropServices;

    using Xamarin.Forms;

    using XLabs.Forms.Controls.MonoDroid.TimesSquare;

    /// <summary>
    /// The login.
    /// </summary>
    public partial class Login : ContentPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromHex("#12A5F4");

            //<StackLayout 1>
            var Title = new Label
            {
                Text = "Appointment Control",
                FontSize = 40,
                BackgroundColor = Color.FromHex("#12A5F4"),
                HeightRequest = 55,
                TextColor = Color.FromHex("#FFF"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            //</StackLayout 1>

            //<StackLayout 2>
            var userName = new Entry
            {
                Placeholder = "Username",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666666"),
                HeightRequest = 45
            };

            var password = new Entry
            {
                Placeholder = "Password",
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromHex("#FFF"),
                TextColor = Color.FromHex("#12A5F4"),
                PlaceholderColor = Color.FromHex("#666666"),
                HeightRequest = 45
            };

            var Login = new Button
            {
                Text = "LogIn",
                BackgroundColor = Color.FromHex("#2C903D"),
                TextColor = Color.FromHex("#FFF"),
            };
            //</StackLayout 2>

            //<StackLayout 3>
            var noAccount = new Label
            {
                Text = "No account?",
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("#FFF"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 20
            };

            var signUp = new Button
            {
                Text = "SignUp",
                BackgroundColor = Color.FromHex("#026CA5"),
                TextColor = Color.FromHex("#FFF"),
            };
            //</StackLayout 3>

            //<StackLayout 4>
            var forgotPass = new Button
            {
                Text = "Forgot your password?",
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("#FFF")
            };
            //</StackLayout 4>

            var stack3 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children = { noAccount, signUp }
            };

            var stack1 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#12A5F4"),
                Children = { Title },
                HeightRequest = 110
            };

            var stack2 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20, 50, 20, 20),
                Spacing = 25,
                Children = { userName, password, Login, stack3 }
            };
            var stack4 = new StackLayout
            {
                Children = { forgotPass }
            };

            var mainStack = new StackLayout { Children = { stack1, stack2, stack4 } };

            Content = mainStack;

            Login.Clicked += (e, sender) =>
                {
                    if ((userName != null && password != null) && (userName.Equals("app") && password.Equals("pass")))
                    {
                        Navigation.PushModalAsync(new PrincipalPage());
                    }
                };

            signUp.Clicked += (e, sender) => Navigation.PushModalAsync(new SignUpPage());
        }

        async void SignIn(object sender, EventArgs eventArgs)
        {
            var page = new NavigationPage(new Page());
            page.BarBackgroundColor = Color.FromHex("#004D40");
            page.BarTextColor = Color.White;

            Application.Current.MainPage = page;
        }

        //async void SignUp(object sender, EventArgs eventArgs)
        //{
        //    Application.Current.MainPage = new NavigationPage(new SignUpPage());

        //}
        #endregion
    }
}