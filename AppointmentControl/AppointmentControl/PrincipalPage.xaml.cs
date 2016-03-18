// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrincipalPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
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

            menu.Menu.ItemSelected += (sender, args) => this.NavigateTo(args.SelectedItem as MenuPage.MenuItem);

            this.Master = menu;

            this.Detail = new MyTabs();
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

        /// <summary>
        /// The navigate to.
        /// </summary>
        /// <param name="menu">
        /// The menu.
        /// </param>
        public void NavigateTo(MenuPage.MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            this.Detail = new NavigationPage(displayPage);

            this.IsPresented = false;
        }

        #endregion
    }
}