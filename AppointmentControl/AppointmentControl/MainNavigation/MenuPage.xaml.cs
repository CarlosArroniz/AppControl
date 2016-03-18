using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppointmentControl.MainNavigation
{
    using global::AppointmentControl.SignInUp;

    public partial class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }
        public MenuPage()
        {
            InitializeComponent();

            Icon = "medical.png";
            Title = "Menu";
            BackgroundColor = Color.FromHex("#12A5F4");

            this.Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content =
                    new Label
                    {
                        TextColor = Color.FromHex("#AAAAAA"),
                        Text = "Menu"
                    }
            };

            var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
        
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
                this.Add(new MenuItem()
                {
                    Title = "Account",
                    IconSource = "medical.png",
                    TargetType = typeof(Login)// Add MeuOptions pages Dumbass
                });
            }
        }

    }
}
