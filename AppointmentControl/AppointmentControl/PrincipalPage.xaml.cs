using Xamarin.Forms;
namespace AppointmentControl
{
    public partial class PrincipalPage : TabbedPage
    {
        public PrincipalPage()
        {
            Children.Add(new MyAppointmentsPage());
            Children.Add(new Login());
        }
    }
}
