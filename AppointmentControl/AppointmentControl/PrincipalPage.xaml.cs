using Xamarin.Forms;
namespace AppointmentControl
{
    public partial class PrincipalPage : TabbedPage
    {
        public PrincipalPage()
        {
            Children.Add(new MyAppointmentsPage { Title = "My Appointments" });
            Children.Add(new CreateAppointment { Title = "Create Appointment" });
            Children.Add(new MyProfilePage { Title = "My Profile" });
        }
        public void CreateAppointment()
        {
            Application.Current.MainPage = new CreateAppointment();
        }
    }
}