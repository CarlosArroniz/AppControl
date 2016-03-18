// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrincipalPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl.MainNavigation
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// The principal page.
    /// </summary>

    // ReSharper disable once PartialTypeWithSinglePart
    public partial class PrincipalPage : MasterDetailPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrincipalPage"/> class.
        /// </summary>
        public PrincipalPage()
        {
            var menu = new MenuPage();

            menu.Menu.ItemSelected += async (sender, e) => this.NavigateTo(e.SelectedItem as MenuItem);

            this.Master = menu;

            this.Detail = new MenuPage();

        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create appointment.
        /// </summary>
        public void CreateAppointment()
        {
            Application.Current.MainPage = new CreateAppointment();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The navigate to.
        /// </summary>
        /// <param name="menu">
        /// The menu.
        /// </param>
        void NavigateTo(MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.GetType());

            this.Detail = new NavigationPage(displayPage);

            this.IsPresented = false;
        }

        #endregion
    }
}