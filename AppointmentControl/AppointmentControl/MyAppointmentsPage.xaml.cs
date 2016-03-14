using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppointmentControl
{
    public partial class MyAppointmentsPage : ContentPage
    {
        public MyAppointmentsPage()
        {
            Grid grid = new Grid
                            {
                                Padding = new Thickness(0, 1, 1, 1),
                                RowSpacing = 1,
                                ColumnSpacing = 1,
                                BackgroundColor = Color.FromHex("E3E3E3")
                            };

            var layout = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 50,
                Padding = new Thickness(0)
            };

            var cardBackground = new Image()
            {
                Source = ImageSource.FromFile("Icons\\shadow.png"),
                Aspect = Aspect.Fill
            };

            layout.Children.Add(
                cardBackground,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width);
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Height);
                })
            );

            //layout.Children.Add(
            //    grid,
            //    Constraint.Constant(10),
            //    Constraint.Constant(10),
            //    Constraint.RelativeToParent((parent) =>
            //    {
            //        return (parent.Width - 15);
            //    }),
            //    Constraint.RelativeToParent((parent) =>
            //    {
            //        return (parent.Height - 0);
            //    })
            //);

            Content = layout;

        }
    }
}
