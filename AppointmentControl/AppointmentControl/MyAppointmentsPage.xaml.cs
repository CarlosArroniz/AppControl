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
        public List<Appointment> appointments;

        /// <summary>
        /// The apps list.
        /// </summary>
        public ObservableCollection<Appointment> appsList;

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
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();

            appointments = new List<Appointment>();

            appointManager = new AppointmentManager();

            this.BackgroundColor = Color.FromHex("#FFF");

            header = new Label
            {
                FontAttributes = FontAttributes.Bold,
                Text = "Appointments for today:",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("#12A5F4"),
                VerticalOptions = LayoutOptions.Center
            };

            appointsList = new ListView
            {
                SeparatorVisibility = SeparatorVisibility.Default,
                SeparatorColor = Color.Red,
                RowHeight = 100,
                WidthRequest = 150,

                ItemTemplate = new DataTemplate(() =>
                {
                    nameLabel = new Label
                    {
                        FontSize = 10,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF"),
                    };
                    nameLabel.SetBinding(Label.TextProperty, "PatientId");

                    dateLabel = new Label
                    {
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromHex("#FFF"),
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

            content = new StackLayout
            {
                Padding = 25,
                Children =
                {
                    header, appointsList
                }
            };

            Content = new ScrollView
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

            var user = ((User)Application.Current.Properties[Constants.UserPropertyName]);

            if (user.isdoctor)
            {
                appointsList.ItemsSource = await appointManager.GetAppointmentsOfDoctorAsync(user.Id);
            }
            else
            {
                appointsList.ItemsSource = await appointManager.GetAppointmentsOfPatientAsync(user.Id);
            }
        }
        #endregion
    }
}