// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppointmentsPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using AppointmentControl.Data;
using AppointmentControl.Models;
using Xamarin.Forms;

namespace AppointmentControl
{
    using System;
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

        private Label statusLabel;
        private Label appointmentIdLabel;

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
            //appsUserList = FillAppointmentsList().Result;

            BackgroundColor = Color.FromHex("#FFF");

            appointsList = new ListView
            {
                IsPullToRefreshEnabled = true,
                RowHeight = 160,
                WidthRequest = 200,
                ItemsSource = appsUserList,
                ItemTemplate = new DataTemplate(() =>
                {
                    statusLabel = new Label()
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };

                    statusLabel.SetBinding(Label.TextProperty, "status");

                    appointmentIdLabel = new Label()
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF"),
                        IsVisible = false
                    };

                    appointmentIdLabel.SetBinding(Label.TextProperty, "Id");

                    var grid = new Grid { Padding = 5, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center };

                    Debug.WriteLine("statusLabel: {0}", statusLabel.Text);
                    Debug.WriteLine("user: {0}", user);

                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(150, GridUnitType.Star) });

                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Auto) });

                    var btnAceptar = new Button { BackgroundColor = Color.FromHex("#2C903D"), FontSize = 12, HeightRequest = 25, Text = "Accept", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
                    var btnRechazar = new Button { BackgroundColor = Color.FromHex("#FF5808"), FontSize = 12, HeightRequest = 25, Text = "Decline", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
                    var btnCancelar = new Button { BackgroundColor = Color.FromHex("#FF0000"), FontSize = 12, HeightRequest = 25, Text = "Cancel", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };

                    btnAceptar.SetBinding(Button.IsVisibleProperty, "isaccepted");  ///Pay Atention
                    btnRechazar.SetBinding(Button.IsVisibleProperty, "isdeclined");
                    btnCancelar.SetBinding(Button.IsVisibleProperty, "iscanceled");

                    btnAceptar.Clicked += (sender, args) => AcceptAppointment(appointmentIdLabel.Text);
                    btnRechazar.Clicked += (sender, args) => DeclineAppointment(appointmentIdLabel.Text);
                    btnCancelar.Clicked += (sender, args) => CancelAppointment(appointmentIdLabel.Text);

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

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(5, 10, 10, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    BackgroundColor = Color.FromHex("#12A5F4"),
                                    WidthRequest = 300,
                                    HeightRequest = 230,
                                    Children =
                                    {
                                        appointmentIdLabel,
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
                    PatientName = userDictionary[appointment.PatientId].Name,
                    isaccepted = (Appointment.REQUESTED.Equals(appointment.status) && user.isdoctor),
                    isdeclined = (Appointment.REQUESTED.Equals(appointment.status) && user.isdoctor),
                    iscanceled = Appointment.ACCEPTED.Equals(appointment.status) || (Appointment.REQUESTED.Equals(appointment.status) && !user.isdoctor)
                };
                appointmentUserList.Add(appointmentUser);
                Debug.WriteLine("FillAppointmentUserList() appointmentUser added: {0}", appointmentUser);
            }

            return appointmentUserList;
        }
        #endregion

        private async void AcceptAppointment(string appointmentId)
        {
            await ChangeAppointmentStatus(appointmentId, Appointment.ACCEPTED);
        }

        private async void CancelAppointment(string appointmentId)
        {
            await ChangeAppointmentStatus(appointmentId, Appointment.CANCELED);
        }

        private async void DeclineAppointment(string appointmentId)
        {
            await ChangeAppointmentStatus(appointmentId, Appointment.DECLINED);
        }

        private async Task ChangeAppointmentStatus(string appointmentId, string status)
        {
            Debug.WriteLine("ChangeAppointmentStatus() trying to get appointment {0}", appointmentId);
            Appointment appointment = await appointManager.GetTaskAsync(appointmentId);
            Debug.WriteLine("ChangeAppointmentStatus() got appointment {0}", appointment);
            appointment.status = status;
            Debug.WriteLine("ChangeAppointmentStatus() trying to save appointment {0}", appointment);
            await appointManager.SaveTaskAsync(appointment);
            Debug.WriteLine("ChangeAppointmentStatus() appointment {0} saved.", appointment);
            RefreshAppointments();
        }
    }
}