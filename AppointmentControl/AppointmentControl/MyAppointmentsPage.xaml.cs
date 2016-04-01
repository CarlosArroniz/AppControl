// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppointmentsPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                    nameLabel.SetBinding(Label.TextProperty, "PatientId");

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

            grid = new Grid
                       {
                           Padding = 5,
                           HorizontalOptions = LayoutOptions.Center,
                           VerticalOptions = LayoutOptions.Center
                       };

            this.grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });
            this.grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            this.grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            this.grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) });

            this.grid.Children.Add(btnAceptar, 0, 0);
            this.grid.Children.Add(btnRechazar, 1, 0);
            this.grid.Children.Add(btnCancelar, 1, 1);

            btnAceptar = new Button { BackgroundColor = Color.FromHex("#2C903D"), FontSize = 12, HeightRequest = 20, Text = "Accept", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
            btnRechazar = new Button { BackgroundColor = Color.FromHex("#FF5808"), FontSize = 12, HeightRequest = 20, Text = "Decline", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };
            btnCancelar = new Button { BackgroundColor = Color.FromHex("#FF0000"), FontSize = 12, HeightRequest = 20, Text = "Cancel", TextColor = Color.FromHex("#FFF"), FontAttributes = FontAttributes.Bold };

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

            if (user.isdoctor)
            {
                btnCancelar.IsVisible = false;

                appsList = await appointManager.GetAppointmentsOfDoctorAsync(user.Id);

                foreach (var apps in appsList)
                {
                    if (apps.status == Appointment.REQUESTED)
                    {
                        btnAceptar.IsVisible = true;
                        btnRechazar.IsVisible = true;
                    }
                    if (apps.status == Appointment.ACCEPTED)
                    {
                        btnAceptar.IsVisible = false;
                        btnRechazar.IsVisible = false;
                    }
                    appointsList.ItemsSource = appsList;
                }
            }
            else
            {
                appsList = await appointManager.GetAppointmentsOfPatientAsync(user.Id);

                foreach (var apps in appsList)
                {
                    if (apps.status == Appointment.REQUESTED)
                    {
                        btnAceptar.IsVisible = false;
                        btnRechazar.IsVisible = false;
                        this.btnCancelar.IsVisible = true;
                    }
                    if (apps.status == Appointment.ACCEPTED)
                    {
                        btnAceptar.IsVisible = false;
                        btnRechazar.IsVisible = false;
                        btnCancelar.IsVisible = true;
                    }
                    appointsList.ItemsSource = appsList;
                }
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
        #endregion
    }
}