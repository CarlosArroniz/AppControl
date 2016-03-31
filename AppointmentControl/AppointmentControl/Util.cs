using System;
using Xamarin.Forms;

namespace AppointmentControl
{
    public abstract class Util
    {
        public static string GetTimestamp(DateTime date, TimeSpan time)
        {
            return date.ToString(Constants.DateFormat) + "T" + time.ToString(Constants.TimeFormat) + "+00:00";
        }

        
        public static AbsoluteLayout CreateAbsoluteLayout(View content, ActivityIndicator actIndicator)
        {
            var absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            var stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };
            stackLayout.Children.Add(content);
            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(0, 0, 1, 1));

            AbsoluteLayout.SetLayoutFlags(actIndicator, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(actIndicator, new Rectangle(0, 0, 1, 1));

            absoluteLayout.Children.Add(stackLayout);
            absoluteLayout.Children.Add(actIndicator);
            return absoluteLayout;
        }

        public static ActivityIndicator CreateLoadingIndicator()
        {
            var loadingIndicator = new ActivityIndicator
            {
                IsRunning = false,
                IsVisible = false,
                Color = Color.Teal,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            return loadingIndicator;
        }
    }
}