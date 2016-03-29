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
            this.BackgroundColor = Color.FromHex("#12A5F4");
            Menu = new MenuListView();

            var menuLabel = new ContentView
                                {
                                    Padding = new Thickness(10, 36, 0, 5),
                                    Content =
                                            new Label
                                                {
                                                    TextColor = Color.FromHex("#FFF"),
                                                    Text = "Settings",
                                                    FontAttributes = FontAttributes.Bold,
                                                    FontSize = 15
                                                }
                                };
            var layout = new StackLayout
                             {
                                 Spacing = 0,
                                 HorizontalOptions = LayoutOptions.CenterAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal
                             };

            layout.Children.Add(menuLabel);
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
                this.Add(new MenuItem() { Title = "Account", IconSource = "medical.png", TargetType = typeof(Login) });
                this.Add(new MenuItem() { Title = "Settings", IconSource = "icon.png", TargetType = typeof(Page) });
            }
        }
    }
}