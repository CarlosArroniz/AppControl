// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppointmentsPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
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

    /// <summary>
    /// The my appointments page.
    /// </summary>
    public partial class MyAppointmentsPage : ContentPage
    {
        #region Fields

        /// <summary>
        /// The appoint manager.
        /// </summary>
        public readonly AppointmentManager appointManager;

        /// <summary>
        /// The appointments.
        /// </summary>
        public List<Appointment> appointments;

        /// <summary>
        /// The apps list.
        /// </summary>
        public ObservableCollection<Appointment> appsList;

        public ObservableCollection<AppointmentUser> appsUserList;

        public ObservableCollection<User> user;

        /// <summary>
        /// The single appoint.
        /// </summary>
        private Label header;

        private ListView appointsList;

        private Label nameLabel;

        private Label dateLabel;

        private Label reasonLabel;

        private BoxView boxView;

        private StackLayout content;

        private ActivityIndicator activityIndicator;

        private Button btnAceptar;

        private Button btnRechazar;

        private Button btnCancelar;

        private Grid grid;


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();

            grid = new Grid { Padding = 5, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center};

            appsUserList = new ObservableCollection<AppointmentUser>();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });

            btnAceptar = new Button { BackgroundColor = Color.FromHex("#2C903D"), FontSize = 12, HeightRequest = 20, Text = "Accept", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
            btnRechazar = new Button { BackgroundColor = Color.FromHex("#FF5808"), FontSize = 12, HeightRequest = 20, Text = "Decline", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
            btnCancelar = new Button { BackgroundColor = Color.FromHex("#FF0000"), FontSize = 12, HeightRequest = 20, Text = "Cancel", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });

            grid.Children.Add(this.btnAceptar, 0, 0);
            grid.Children.Add(this.btnRechazar, 1, 0);
            grid.Children.Add(this.btnCancelar, 2, 0);

            appointments = new List<Appointment>();

            appointManager = AppointmentManager.Instance;

            BackgroundColor = Color.FromHex("#FFF");

            appointsList = new ListView
            {
                IsPullToRefreshEnabled = true,
                RowHeight = 150,
                WidthRequest = 200,

                ItemTemplate = new DataTemplate(() =>
                {
                    nameLabel = new Label
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };

                    var userType = ((User)Application.Current.Properties[Constants.UserPropertyName]);

                    this.nameLabel.SetBinding(Label.TextProperty, userType.isdoctor ? "PatientName" : "DoctorName");

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

        private void RefreshAppointments()
        {
            FillAppointmentsList();
        }

        private async void FillAppointmentsList()
        {
            var user = ((User)Application.Current.Properties[Constants.UserPropertyName]);

            activityIndicator.IsRunning = activityIndicator.IsVisible = true;

            appsList = await this.appointManager.GetAppointmentsOfDoctorAsync(user.Id);

            if (user.isdoctor)
            {
                foreach (var app in appsList)
                {
                    if (app.status.Equals(Appointment.ACCEPTED))
                    {
                        this.btnAceptar.IsVisible = false;
                        this.btnRechazar.IsVisible = false;
                        this.btnCancelar.IsVisible = false;
                    }

                    if (app.status.Equals(Appointment.REQUESTED))
                    {
                        this.btnAceptar.IsVisible = true;
                        this.btnRechazar.IsVisible = true;
                        this.btnCancelar.IsVisible = false;
                    }
                }

                appsList = await this.appointManager.GetAppointmentsOfDoctorAsync(user.Id);

                appointsList.ItemsSource = await FillAppointmentUserList(this.appsList);
            }
            else
            {
                foreach (var app in appsList)
                {
                    if (app.status.Equals(Appointment.ACCEPTED))
                    {
                        this.btnAceptar.IsVisible = false;
                        this.btnRechazar.IsVisible = false;
                        this.btnCancelar.IsVisible = false;
                    }

                    if (app.status.Equals(Appointment.REQUESTED))
                    {
                        this.btnAceptar.IsVisible = true;
                        this.btnRechazar.IsVisible = true;
                        this.btnCancelar.IsVisible = false;
                    }
                }

                appsList = await this.appointManager.GetAppointmentsOfPatientAsync(user.Id);

                appointsList.ItemsSource = await FillAppointmentUserList(this.appsList);
            }
            
            activityIndicator.IsRunning = activityIndicator.IsVisible = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            FillAppointmentsList();
        }

        private async Task<Dictionary<string, User>> GetUsersDictionary(ObservableCollection<Appointment> appointmentList)
        {
            var loggedUser = ((User)Application.Current.Properties[Constants.UserPropertyName]);
            var userDictionary = new Dictionary<string, User>();
            var userManager = UserManager.Instance;

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
                    var user = await userManager.GetUsersAsync(appointment.PatientId);
                    userDictionary.Add(appointment.PatientId, user.First());
                    Debug.WriteLine("GetUsersDictionary() user added: {0} {1}", user.First().Id, user.First().Name);
                }
            }
            else
            {
                foreach (
                    var appointment in
                        appointmentList.Where(appointment => !userDictionary.ContainsKey(appointment.DoctorId)))
                {
                    var user = await userManager.GetUsersAsync(appointment.DoctorId);
                    userDictionary.Add(appointment.DoctorId, user.First());
                    Debug.WriteLine("GetUsersDictionary() user added: {0} {1}", user.First().Id, user.First().Name);
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
                var appointmentUser = new AppointmentUser()
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