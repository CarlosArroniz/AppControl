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

    using Xamarin.Forms;

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
        }

        async void SignIn(object sender, EventArgs eventArgs)
        {
            var page = new NavigationPage(new Page());
            page.BarBackgroundColor = Color.FromHex("#004D40");
            page.BarTextColor = Color.White;

            Application.Current.MainPage = page;

        }
        #endregion
    }
}