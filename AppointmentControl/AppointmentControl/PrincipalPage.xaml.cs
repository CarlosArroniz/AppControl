// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrincipalPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using Xamarin.Forms;

    /// <summary>
    /// The principal page.
    /// </summary>
    public partial class PrincipalPage : TabbedPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrincipalPage"/> class.
        /// </summary>
        public PrincipalPage()
        {
            Padding = new Thickness(10,15,15,15);

            this.Children.Add(new MyAppointmentsPage { Title = "My Appointments"});
            this.Children.Add(new CreateAppointment { Title = "New Appointment" });
            this.Children.Add(new MyProfilePage { Title = "My Profile" });
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create appointment.
        /// </summary>
        public void CreateAppointment()
        {
            Application.Current.MainPage = new CreateAppointment();
        }
        #endregion
    }
}