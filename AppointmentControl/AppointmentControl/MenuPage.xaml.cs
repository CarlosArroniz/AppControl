// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuPage.xaml.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl
{
    using System;
    using System.Collections.Generic;

    using Xamarin.Forms;

    using XLabs.Platform.Services.Geolocation;

    /// <summary>
    /// The menu page.
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class MenuPage : ContentPage
    {
        #region Constructors and Destructors

        public ListView Menu;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();

            this.Title = "Menu";
            this.BackgroundColor = Color.FromHex("#FFF");

            this.Opacity = Opacity.RadiansToDegrees();

            Menu = new MenuListView();

            var label1 = new Label
            {
                TextColor = Color.FromHex("#12A5F4"),
                Text = "Edit Profile",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15
            };

            var label2 = new Label
            {
                TextColor = Color.FromHex("#12A5F4"),
                Text = "Log Out",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal
            };

            layout.Children.Add(Menu);

            this.Content = layout;
        }

        #endregion
        /// <summary>
        /// The master behavior.
        /// </summary>
        public enum MasterBehavior
        {
            Default,

            Popover,

            Split,

            SplitLandscape,

            SplitOnPortrait
        }

        public class MenuItem
        {
            public string Title { get; set; }

            public string IconSource { get; set; }

            public Type TargetType { get; set; }
        }

        /// <summary>
        /// The menu list view.
        /// </summary>
        public class MenuListView : ListView
        {
            public MenuListView()
            {
                List<MenuItem> data = new MenuListData();

                this.ItemsSource = data;
                this.VerticalOptions = LayoutOptions.FillAndExpand;
                this.BackgroundColor = Color.Transparent;
                this.WidthRequest = 120;

                var cell = new DataTemplate(typeof(ImageCell));
                cell.SetBinding(TextCell.TextColorProperty, "Title");
                cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

                this.ItemTemplate = cell;
            }
        }

        public class MenuListData : List<MenuItem>
        {
            public MenuListData()
            {
                Add(new MenuItem() { Title = "Edit Profile", IconSource = "edit.png", TargetType = typeof(EditProfile) });
                Add(new MenuItem() { Title = "LogOut", IconSource = "logout.png", TargetType = typeof(Login) });
            }
        }
    }
}