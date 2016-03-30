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

    using XLabs.Forms;
    using XLabs.Forms.Controls;

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

        private Button btnAceptar;

        private Button btnRechazar;

        private Button btnCancelar;
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAppointmentsPage"/> class.
        /// </summary>
        public MyAppointmentsPage()
        {
            InitializeComponent();

            var grid = new Grid { Padding = 2 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Star) });

            appointments = new List<Appointment>();

            appointManager = AppointmentManager.Instance;

            this.BackgroundColor = Color.FromHex("#FFF");

            btnAceptar = new Button { TextColor = Color.FromHex("#FFF"), BackgroundColor = Color.FromHex("#2C903D"), Text = "Accept", WidthRequest = 10, FontSize = 10, HeightRequest = 70 };
            btnRechazar = new Button { TextColor = Color.FromHex("#FFF"), BackgroundColor = Color.FromHex("#FF5808"), Text = "Deny", WidthRequest = 10, FontSize = 10, HeightRequest = 70 };
            btnCancelar = new Button { TextColor = Color.FromHex("#FFF"), BackgroundColor = Color.FromHex("#FF0000"), Text = "Cancel", WidthRequest = 10, FontSize = 10, HeightRequest = 70 };

            grid.Children.Add(btnAceptar, 0, 0);
            grid.Children.Add(btnRechazar, 1, 0);
            grid.Children.Add(btnCancelar, 2, 0);

            appointsList = new ListView
            {
                SeparatorVisibility = SeparatorVisibility.Default,
                SeparatorColor = Color.Gray,
                RowHeight = 150,
                WidthRequest = 200,
                ItemTemplate = new DataTemplate(() =>
                {
                    nameLabel = new Label
                    {
                        FontSize = 15,
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

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = 8,
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new StackLayout
                                {
                                    Padding = new Thickness(1, 5, 1, 1), 
                                    HorizontalOptions = LayoutOptions.Center, 
                                    BackgroundColor = Color.FromHex("#12A5F4"), 
                                    WidthRequest = 300, 
                                    HeightRequest = 150, 
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

            content = new StackLayout
            {
                Padding = 0,
                Children =
                {
                    appointsList
                }
            };

            var framious = new Frame() { HasShadow = true, Content = content };

            Content = framious;
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
                btnCancelar.IsVisible = false;
                appointsList.ItemsSource = await appointManager.GetAppointmentsOfDoctorAsync(user.Id);
            }
            else
            {
                appointsList.ItemsSource = await appointManager.GetAppointmentsOfPatientAsync(user.Id);
                btnAceptar.IsVisible = false;
                btnRechazar.IsVisible = false;
                btnCancelar.IsVisible = true;
            }
        }
        #endregion
    }
}