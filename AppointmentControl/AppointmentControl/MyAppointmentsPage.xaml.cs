// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppointmentsPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using global::AppointmentControl.Data;

    using global::AppointmentControl.Models;

    using Newtonsoft.Json;

    using Xamarin.Forms;

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
        public List<Appointments> appointments;

        /// <summary>
        /// The apps list.
        /// </summary>
        public ObservableCollection<Appointment> appsList;

        /// <summary>
        /// The single appoint.
        /// </summary>
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();
            appointments = new List<Appointments>();

            appointManager = new AppointmentManager();

            this.BackgroundColor = Color.FromHex("#FFF");

            var header = new Label
            {
                FontAttributes = FontAttributes.Bold,
                Text = "Appointments for today:",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.Center
            };

            var appointsList = new ListView
            {
                SeparatorVisibility = SeparatorVisibility.Default,
                SeparatorColor = Color.FromHex("#FFF"),
                ItemsSource = this.appointments,
                RowHeight = 100,
                WidthRequest = 150,

                ItemTemplate = new DataTemplate(() =>
                {
                    var nameLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF"),
                    };
                    nameLabel.SetBinding(Label.TextProperty, "PatientId");

                    var dateLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF"),
                    };

                    dateLabel.SetBinding(Label.TextProperty, new Binding("StartDate", BindingMode.OneWay, null, null, "Appointment Date {0:d}"));

                    var reasonLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Italic,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF")
                    };
                    reasonLabel.SetBinding(Label.TextProperty, "Reason");

                    var boxView = new BoxView { BackgroundColor = Color.FromHex("#666666") };

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                boxView, new StackLayout
                                {
                                    HorizontalOptions = LayoutOptions.CenterAndExpand, 
                                    BackgroundColor = Color.FromHex("#808080"), 
                                    WidthRequest = 300, 
                                    HeightRequest = 150, 
                                    Children =
                                    {
                                        nameLabel, 
                                        dateLabel, 
                                        reasonLabel
                                    }
                                }
                            }
                        }
                    };
                })
            };

            var content = new StackLayout
            {
                Padding = 25,
                Children =
                {
                    header, appointsList
                }
            };

            this.Content = new ScrollView
            {
                Content = content,
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userId = ((User)Application.Current.Properties[Constants.UserPropertyName]).Id;

            appsList = await appointManager.GetAppointmentsOfDoctorAsync(userId);

            if (appsList != null)
            {
                foreach (var appointment in appsList)
                {
                    appointments.Add(new Appointments(appointment.Id, appointment.DoctorId, appointment.PatientId, appointment.StartDate, appointment.EndDate, appointment.Reason, appointment.status));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "No hay citas", "Aceptar");
            }
        }

        public class Appointments
        {

            public Appointments(string Id, string DoctorId, string PatientId, string StartDate, string EndDate, string Reason, string status)
            {
                this.Id = Id;
                this.DoctorId = DoctorId;
                this.PatientId = PatientId;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
                this.Reason = Reason;
                this.status = status;
            }
            
            public string Id { get; set; }
            
            public string DoctorId { get; set; }

            public string PatientId { get; set; }

            public string StartDate { get; set; }

            public string EndDate { get; set; }

            public string Reason { get; set; }

            public string status { get; set; }
        }
        #endregion
    }
}