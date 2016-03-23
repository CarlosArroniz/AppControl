
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class MyTabs : TabbedPage
    {
        public MyTabs()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromHex("#FFF");
            this.Padding = new Thickness(10, 15, 15, 15);
            this.Children.Add(new MyAppointmentsPage { Title = "My Appointments" });
            this.Children.Add(new CreateAppointment { Title = "New Appointment" });
            this.Children.Add(new MyProfilePage { Title = "My Profile" });
        }
    }
}
