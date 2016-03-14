// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The appointment control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using Xamarin.Forms;

    /// <summary>
    /// The appointment control.
    /// </summary>
    public partial class AppointmentControl : Application
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentControl"/> class.
        /// </summary>
        public AppointmentControl()
        {
            MainPage = new Login();
        }

        #endregion
    }
}