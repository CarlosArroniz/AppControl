// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppointmentsPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppointmentControl.Data;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{
    using System.Threading.Tasks;

    /// <summary>
    /// The my appointments page.
    /// </summary>
    public partial class MyAppointmentsPage : ContentPage
    {
        #region Fields

        private readonly AppointmentManager appointManager;
        private readonly UserManager userManager;

        public ObservableCollection<Appointment> appsList;

        public ObservableCollection<AppointmentUser> appsUserList;

        private User user;

        private Label header;

        private ListView appointsList;

        private Label nameLabel;

        private Label dateLabel;

        private Label reasonLabel;

        private BoxView boxView;

        private StackLayout content;

        private ActivityIndicator activityIndicator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();

            user = (User)Application.Current.Properties[Constants.UserPropertyName];
            appointManager = AppointmentManager.Instance;
            userManager = UserManager.Instance;
            appsUserList = FillAppointmentsList().Result;

            BackgroundColor = Color.FromHex("#FFF");

            appointsList = new ListView
            {
                IsPullToRefreshEnabled = true,
                RowHeight = 150,
                WidthRequest = 200,
                ItemsSource = appsUserList,
                ItemTemplate = new DataTemplate(() =>
                {
                    var statusLabel = new Label()
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };
                    statusLabel.SetBinding(Label.TextProperty, "status");

                    var grid = new Grid { Padding = 5, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center };

                    var btnAceptar = new Button { BackgroundColor = Color.FromHex("#2C903D"), FontSize = 12, HeightRequest = 20, Text = "Accept", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
                    var btnRechazar = new Button { BackgroundColor = Color.FromHex("#FF5808"), FontSize = 12, HeightRequest = 20, Text = "Decline", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
                    var btnCancelar = new Button { BackgroundColor = Color.FromHex("#FF0000"), FontSize = 12, HeightRequest = 20, Text = "Cancel", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };

                    Debug.WriteLine("statusLabel: {0}", statusLabel.Text);
                    Debug.WriteLine("user: {0}", user);
                    //btnAceptar.IsVisible = Appointment.REQUESTED.Equals(statusLabel.Text) && user.isdoctor;
                    //btnRechazar.IsVisible = Appointment.REQUESTED.Equals(statusLabel.Text) && user.isdoctor;
                    //btnCancelar.IsVisible = Appointment.ACCEPTED.Equals(statusLabel.Text) || (!user.isdoctor && Appointment.REQUESTED.Equals(statusLabel.Text));

                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });

                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });

                    grid.Children.Add(btnAceptar, 0, 0);
                    grid.Children.Add(btnRechazar, 1, 0);
                    grid.Children.Add(btnCancelar, 2, 0);

                    nameLabel = new Label
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };

                    var userType = ((User)Application.Current.Properties[Constants.UserPropertyName]);

                    nameLabel.SetBinding(Label.TextProperty, userType.isdoctor ? "PatientName" : "DoctorName");

                    dateLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };

                    dateLabel.SetBinding(Label.TextProperty, new Binding("StartDate", BindingMode.OneWay, null, null, "{0:d}"));

                    reasonLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Italic,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };
                    reasonLabel.SetBinding(Label.TextProperty, "Reason");

                    boxView = new BoxView { BackgroundColor = Color.FromHex("#666666") };

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(5, 10, 10, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                boxView, new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    BackgroundColor = Color.FromHex("#12A5F4"),
                                    WidthRequest = 300,
                                    HeightRequest = 230,
                                    Children =
                                    {
                                        nameLabel,
                                        dateLabel,
                                        reasonLabel,
                                        statusLabel,
                                        grid
                                    }
                                }
                            }
                        }
                    };
                })
            };

            appointsList.RefreshCommand = new Command(() =>
            {
                RefreshAppointments();
                appointsList.IsRefreshing = false;
            });

            content = new StackLayout
            {
                Padding = 25,
                Children =
                {
                    appointsList
                }
            };

            activityIndicator = Util.CreateLoadingIndicator();
            var layout = Util.CreateAbsoluteLayout(content, activityIndicator);

            Content = layout;
        }

        private async void RefreshAppointments()
        {
            appointsList.ItemsSource = await FillAppointmentsList();
        }

        private async Task<ObservableCollection<AppointmentUser>> FillAppointmentsList()
        {
            ObservableCollection<AppointmentUser> appountmentUserList;
            
            //activityIndicator.IsRunning = activityIndicator.IsVisible = true;

            appsList = await appointManager.GetAppointmentsOfDoctorAsync(user.Id);

            if (user.isdoctor)
            {
                appsList = await appointManager.GetAppointmentsOfDoctorAsync(user.Id);

            }
            else
            {
                appsList = await appointManager.GetAppointmentsOfPatientAsync(user.Id);

            }
            //activityIndicator.IsRunning = activityIndicator.IsVisible = false;

            appountmentUserList = await FillAppointmentUserList(appsList);
            appointsList.ItemsSource = appountmentUserList;
            return appountmentUserList;

        }

        #endregion

        #region Methods

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            RefreshAppointments();
        }

        private async Task<Dictionary<string, User>> GetUsersDictionary(ObservableCollection<Appointment> appointmentList)
        {
            var loggedUser = ((User)Application.Current.Properties[Constants.UserPropertyName]);
            var userDictionary = new Dictionary<string, User>();
            

            if (!userDictionary.ContainsKey(loggedUser.Id))
            {
                userDictionary.Add(loggedUser.Id, loggedUser);
                Debug.WriteLine("GetUsersDictionary() user added: {0} {1}", loggedUser.Id, loggedUser.Name);
            }
                
            if (loggedUser.isdoctor)
            {
                foreach (
                    var appointment in
                        appointmentList.Where(appointment => !userDictionary.ContainsKey(appointment.PatientId)))
                {
                    var userFound = await userManager.GetUsersAsync(appointment.PatientId);
                    userDictionary.Add(appointment.PatientId, userFound.First());
                    Debug.WriteLine("GetUsersDictionary() user added: {0} {1}", userFound.First().Id, userFound.First().Name);
        }
            }
            else
            {
                foreach (
                    var appointment in
                        appointmentList.Where(appointment => !userDictionary.ContainsKey(appointment.DoctorId)))
                {
                    var userFound = await userManager.GetUsersAsync(appointment.DoctorId);
                    userDictionary.Add(appointment.DoctorId, userFound.First());
                    Debug.WriteLine("GetUsersDictionary() user added: {0} {1}", userFound.First().Id, userFound.First().Name);
                }
            }

            return userDictionary;
        }

        private async Task<ObservableCollection<AppointmentUser>> FillAppointmentUserList(ObservableCollection<Appointment> appointmentList)
        {
            var appointmentUserList = new ObservableCollection<AppointmentUser>();
            Dictionary<string, User> userDictionary = await GetUsersDictionary(appointmentList);

            foreach (var appointment in appointmentList)
            {
                var appointmentUser = new AppointmentUser
                {
                    Id = appointment.Id,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    Reason = appointment.Reason,
                    status = appointment.status,
                    DoctorName = userDictionary[appointment.DoctorId].Name,
                    PatientName = userDictionary[appointment.PatientId].Name
                };
                appointmentUserList.Add(appointmentUser);
                Debug.WriteLine("FillAppointmentUserList() appointmentUser added: {0}", appointmentUser);
            }

            return appointmentUserList;
        }
        #endregion
    }
}