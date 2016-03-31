using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AppointmentControl.Data;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class EditProfile : NavigationPage
    {
        private readonly UserManager userManager = UserManager.Instance;
        private User user;

        public EditProfile()
        {
            InitializeComponent();

            user = Application.Current.Properties[Constants.UserPropertyName] as User;

            FillFieldsWithLoggedUserData();
            EnableFieldsByUserType();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();

            Navigation.PushModalAsync(new MyProfilePage());
        }

        private void EnableFieldsByUserType()
        {
            if (user != null && !user.isdoctor)
            {
                SpecialityLabel.IsEnabled = SpecialityLabel.IsVisible = false;
                Speciality.IsEnabled = Speciality.IsVisible = false;
            }
        }

        private void FillFieldsWithLoggedUserData()
        {
            Name.Text = user.Name;
            Speciality.Text = user.Speciality;
            Phone.Text = user.Phone;
            Address.Text = user.Address;
            City.Text = user.City;
            State.Text = user.State;
            Country.Text = user.Country;
            ZipCode.Text = user.Zip;
            Username.Text = user.Username;
            Password.Text = user.Password;
            Email.Text = user.Email;
        }

        private async void Save(object sender, EventArgs e)
        {
            if (!await ValidateFields()) return;

            ActIndicator.IsRunning = ActIndicator.IsVisible = true;
            user = FillUserDataWithFormFields();
            await userManager.SaveTaskAsync(user);
            Application.Current.Properties[Constants.UserPropertyName] = user;

            Application.Current.MainPage = new PrincipalPage();
        }

        private User FillUserDataWithFormFields()
        {
            User user = Application.Current.Properties[Constants.UserPropertyName] as User;
            user.Name = Name.Text;
            user.Speciality = Speciality.Text;
            user.Phone = Phone.Text;
            user.Address = Address.Text;
            user.City = City.Text;
            user.State = State.Text;
            user.Country = Country.Text;
            user.Zip = ZipCode.Text;
            user.Username = Username.Text;
            user.Password = Password.Text;
            user.Email = Email.Text;
            return user;
        }

        private async Task<bool> ValidateFields()
        {
            if (user.Password != Password.Text)
            {
                if (!await DisplayAlert(null, "Are you sure do you want to change your current password?", "Ok", "Cancel"))
                {
                    Password.Text = user.Password;
                    return false;
                }

                if (string.IsNullOrEmpty(Password.Text))
                {
                    await DisplayAlert("Invalid password.",
                        "Please enter a correct password.", "Ok");
                    Password.Text = null;
                    Password.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(user.Phone) && user.Phone != Phone.Text)
            {
                if (!await DisplayAlert(null, "Are you sure do you want to change your current phone number?", "Ok", "Cancel"))
                {
                    Phone.Text = user.Phone;
                    return false;
                }

                if (Phone.Text.Length != 10)
                {
                    await DisplayAlert("Invalid phone number.",
                        "Please enter a correct phone number.", "Ok");
                    Phone.Text = null;
                    Phone.Focus();
                    return false;
                }
            }

            if (Speciality.IsEnabled && string.IsNullOrEmpty(Speciality.Text))
            {
                await DisplayAlert("Invalid Speciality.",
                    "Please insert your medical speciality.", "Ok");
                Speciality.Text = null;
                Speciality.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(user.Email) && user.Email != Email.Text)
            {
                if (!await DisplayAlert(null, "Are you sure do you want to change your current email?", "Ok", "Cancel"))
                {
                    Email.Text = user.Email;
                    return false;
                }
                if (!Regex.Match(Email.Text, Constants.EmailRegexPattern).Success)
                {
                    await DisplayAlert("Invalid Email.",
                    "Please insert a valid email address.", "Ok");
                    Email.Text = null;
                    Email.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
