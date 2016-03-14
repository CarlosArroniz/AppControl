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