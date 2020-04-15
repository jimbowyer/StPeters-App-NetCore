using System;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace StPeters
{
    public class pagePrayers : ContentPage
    {
        public pagePrayers()
        {
            HtmlWebViewSource hSource = new HtmlWebViewSource();
            hSource.Html = LoadPrayerText();

            WebView browser = new WebView
            {
                Source = hSource,
                HeightRequest = 500,
                WidthRequest = 450,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Content = browser;
        }
        private string LoadPrayerText()
        {
            try
            {
                string sRtn = "";
                Assembly assem = typeof(pagePrayers).GetTypeInfo().Assembly;
                Stream streamMystery = assem.GetManifestResourceStream("StPeters.Resources.prayers.htm");
                if (streamMystery != null)
                {
                    using (StreamReader reader = new StreamReader(streamMystery))
                    {
                        sRtn = reader.ReadToEnd();
                    }
                }
                return sRtn;
            }
            catch (Exception ex)
            {
                return ("Failed loading prayers: " + ex.Message);
            }

        }
    }
}
