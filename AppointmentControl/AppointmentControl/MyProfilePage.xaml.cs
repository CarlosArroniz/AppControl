// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyProfilePage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;
    using System.Collections.Generic;

    using Xamarin.Forms;

    /// <summary>
    /// The my profile page.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class MyProfilePage : ContentPage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProfilePage"/> class.
        /// </summary>
        public MyProfilePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The edit profile.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        async void EditProfile(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new EditProfile());
        }

        /// <summary>
        /// The navigate to.
        /// </summary>
        /// <param name="menu">
        /// The menu.
        /// </param>
        
        #endregion
    }
}
