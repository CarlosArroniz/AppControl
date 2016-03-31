// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentControl.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using Xamarin.Forms;

    /// <summary>
    ///     The appointment control.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class AppointmentControl : Application
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AppointmentControl" /> class.
        /// </summary>
        public AppointmentControl()
        {
            MainPage = new Login();
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            MainPage = new Login();
        }

        #endregion
    }
}