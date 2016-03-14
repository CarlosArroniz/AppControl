using Xamarin.Forms;
namespace AppointmentControl
{
    public partial class PrincipalPage : TabbedPage
    {
        readonly Page tab1Page;
        readonly Page tab2Page;
        readonly Page tab3Page;
        public PrincipalPage()
        {
            tab1Page = new MyAppointmentsPage() { Title = "My Appointments" };
            tab2Page = new MyProfilePage() { Title = "My Profile" };

            Children.Add(tab1Page);
            Children.Add(tab2Page);
            Children.Add(tab2Page);
        }
    }
}
