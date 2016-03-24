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
    public class MyAppointmentsPage : ContentPage
    {
        #region Fields

        /// <summary>
        /// The appoint manager.
        /// </summary>
        private readonly AppointmentManager appointManager;

        /// <summary>
        /// The appointments.
        /// </summary>
        private List<Appointments> appointments;

        /// <summary>
        /// The apps list.
        /// </summary>
        private ObservableCollection<Appointment> appsList;

        /// <summary>
        /// The single appoint.
        /// </summary>
        private Appointment singleAppoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();

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


            this.appsList = await this.appointManager.GetAppointmentsAsync(userId);

            foreach (var appointment in this.appsList)
            {
                this.appointments = new List<Appointments>
                {
                    new Appointments(appointment.Id, appointment.DoctorId, appointment.PatientId, appointment.StartDate, appointment.EndDate, appointment.Reason, appointment.status)
                };
            }
        }

        #endregion

        /// <summary>
        /// The appointments.
        /// </summary>
        public class Appointments
        {
            #region Constants

            /// <summary>
            /// The accepted.
            /// </summary>
            public const string ACCEPTED = "Accepted";

            /// <summary>
            /// The declined.
            /// </summary>
            public const string DECLINED = "Declined";

            /// <summary>
            /// The done.
            /// </summary>
            public const string DONE = "DONE";

            /// <summary>
            /// The rejected.
            /// </summary>
            public const string REJECTED = "Rejected";

            /// <summary>
            /// The requested.
            /// </summary>
            public const string REQUESTED = "Requested";

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Appointments"/> class.
            /// </summary>
            /// <param name="id">
            /// The id.
            /// </param>
            /// <param name="doctorId">
            /// The doctor id.
            /// </param>
            /// <param name="patientId">
            /// The patient id.
            /// </param>
            /// <param name="startDate">
            /// The start date.
            /// </param>
            /// <param name="endDate">
            /// The end date.
            /// </param>
            /// <param name="reason">
            /// The reason.
            /// </param>
            /// <param name="status">
            /// The status.
            /// </param>
            public Appointments(string id, string doctorId, string patientId, string startDate, string endDate, string reason, string status)
            {
                this.Id = id;
                this.DoctorId = doctorId;
                this.PatientId = patientId;
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.Reason = reason;
                this.status = status;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the doctor id.
            /// </summary>
            [JsonProperty(PropertyName = "doctor_id")]
            private string DoctorId { get; set; }

            /// <summary>
            /// Gets or sets the end date.
            /// </summary>
            [JsonProperty(PropertyName = "enddate")]
            private string EndDate { get; set; }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            [JsonProperty(PropertyName = "id")]
            private string Id { get; set; }

            /// <summary>
            /// Gets or sets the patient id.
            /// </summary>
            [JsonProperty(PropertyName = "patient_id")]
            private string PatientId { get; set; }

            /// <summary>
            /// Gets or sets the reason.
            /// </summary>
            [JsonProperty(PropertyName = "Reason")]
            private string Reason { get; set; }

            /// <summary>
            /// Gets or sets the start date.
            /// </summary>
            [JsonProperty(PropertyName = "startdate")]
            private string StartDate { get; set; }

            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            [JsonProperty(PropertyName = "status")]
            private string status { get; set; }

            #endregion
        }
    }
}