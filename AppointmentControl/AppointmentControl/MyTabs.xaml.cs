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

            BackgroundColor = Color.FromHex("#FFF");
            Padding = 5;
            Children.Add(new MyAppointmentsPage { Title = "My Appointmens", Icon = "appointment.png"});
            Children.Add(new CreateAppointment { Title = "New Appointment" });
            Children.Add(new MyProfilePage { Title = "My Profile" });
        }

        #endregion
    }
}