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
            this.MainPage = new Login();
        }

        #endregion
    }
}