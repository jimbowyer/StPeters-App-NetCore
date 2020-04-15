using System;
using Xamarin.Forms;

namespace StPeters
{
    public class pageMassReadings : TabbedPage
    {
        const string cNOVALIS = "http://ec2-34-245-7-114.eu-west-1.compute.amazonaws.com/";
        const string cREAD1 = "daily-texts/reading/";
        const string cPSALM = "daily-texts/psalm/";
        const string cREAD2 = "daily-texts/reading2/";
        const string cGOSPEL = "daily-texts/gospel/";

        string sDate = DateTime.Now.ToString("yyyy-MM-dd");
        string sDateFormatted = DateTime.Now.ToString("m");

        public pageMassReadings()
        {
            this.Title = "Mass Readings for " + sDateFormatted;
            this.Children.Add(new ContentPage
            {
                Title = "1st Reading",
                Content = new WebView
                {
                    Source = cNOVALIS + cREAD1 + sDate,
                    HeightRequest = Application.Current.MainPage.Height,
                    WidthRequest = Application.Current.MainPage.Width,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                },
            }
            );
            this.Children.Add(new ContentPage
            {
                Title = "Psalm",
                Content = new WebView
                {
                    Source = cNOVALIS + cPSALM + sDate,
                    HeightRequest = Application.Current.MainPage.Height,
                    WidthRequest = Application.Current.MainPage.Width,
                    VerticalOptions = LayoutOptions.Center
                },
            }
            );
            this.Children.Add(new ContentPage
            {
                Title = "2nd Reading",
                Content = new WebView
                {
                    Source = cNOVALIS + cREAD2 + sDate,
                    HeightRequest = Application.Current.MainPage.Height,
                    WidthRequest = Application.Current.MainPage.Width,
                    VerticalOptions = LayoutOptions.Center
                },
            }
            );
            this.Children.Add(new ContentPage
            {
                Title = "Gospel",
                Content = new WebView
                {
                    Source = cNOVALIS + cGOSPEL + sDate,
                    HeightRequest = Application.Current.MainPage.Height,
                    WidthRequest = Application.Current.MainPage.Width,
                    VerticalOptions = LayoutOptions.Center
                },
            });
        }
    } //class PageReadingsMass
}
