// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyProfilePage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// The my profile page.
    /// </summary>
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
            await Navigation.PushModalAsync(new EditProfile());
        }

        #endregion
    }
}
