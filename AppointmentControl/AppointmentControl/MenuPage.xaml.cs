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

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();

            var icon = new Image { Source = ImageSource.FromFile("medical.png"), HeightRequest = 40, WidthRequest = 40};
            this.Title = "Menu";
            this.BackgroundColor = Color.FromHex("#12A5F4");
            this.Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5), 
                Content = new Label
                {
                    TextColor = Color.FromHex("#FFF"), 
                    Text = "Menu",
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 15
                }
            };

            var layout = new StackLayout { Spacing = 0, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Horizontal };

            layout.Children.Add(menuLabel);
            layout.Children.Add(this.Menu);

            this.Content = layout;
        }

        #endregion

        #region Enums

        /// <summary>
        /// The master behavior.
        /// </summary>
        public enum MasterBehavior
        {
            /// <summary>
            /// The default.
            /// </summary>
            Default, 

            /// <summary>
            /// The popover.
            /// </summary>
            Popover, 

            /// <summary>
            /// The split.
            /// </summary>
            Split, 

            /// <summary>
            /// The split landscape.
            /// </summary>
            SplitLandscape, 

            /// <summary>
            /// The split on portrait.
            /// </summary>
            SplitOnPortrait
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        public ListView Menu { get; set; }

        #endregion

        /// <summary>
        /// The menu item.
        /// </summary>
        public class MenuItem
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the icon source.
            /// </summary>
            public string IconSource { get; set; }

            /// <summary>
            /// Gets or sets the target type.
            /// </summary>
            public Type TargetType { get; set; }

            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            public string Title { get; set; }

            #endregion
        }

        /// <summary>
        /// The menu list data.
        /// </summary>
        public class MenuListData : List<MenuItem>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="MenuListData"/> class.
            /// </summary>
            public MenuListData()
            {
                this.Add(
                    new MenuItem
                        {
                            Title = "Account", 
                            IconSource = "medical.png", 
                            TargetType = typeof(Login) // Add MeuOptions pages Dumbass
                        });
            }

            #endregion
        }

        /// <summary>
        /// The menu list view.
        /// </summary>
        public class MenuListView : ListView
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="MenuListView"/> class.
            /// </summary>
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

            #endregion
        }
    }
}
