// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyTabs.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using Xamarin.Forms;

    /// <summary>
    /// The my tabs.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class MyTabs : TabbedPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTabs"/> class.
        /// </summary>
        public MyTabs()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromHex("#FFF");
            this.Padding = 10;
            this.Children.Add(new MyAppointmentsPage { Title = "My Appointments" });
            this.Children.Add(new CreateAppointment { Title = "New Appointment" });
            this.Children.Add(new MyProfilePage { Title = "My Profile" });
        }

        #endregion
    }
}