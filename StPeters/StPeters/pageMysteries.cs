using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;

namespace StPeters
{
    public class pageMysteries : ContentPage
    {
        public pageMysteries(DayOfWeek pDoW)
        {
            ToolbarItem tbMenu = new ToolbarItem
            {
                Text = "Rosary prayers",
                //custom menu work below for each platform
            };

            switch (Device.RuntimePlatform)
            {
                case (Device.iOS):
                    {
                        tbMenu.Order = ToolbarItemOrder.Primary;
                        ToolbarItems.Add(tbMenu);
                        tbMenu.Clicked += (sender, args) =>
                        {
                            Navigation.PushAsync(new pagePrayers());
                        };
                        break;
                    }
                case (Device.Android):
                    {
                        ToolbarItems.Add(new ToolbarItem("Rosary prayers", "", () =>
                        {
                            Navigation.PushAsync(new pagePrayers());
                        }));
                        break;
                    }
                case (Device.UWP):
                    {
                        tbMenu.IconImageSource = "info.png";
                        tbMenu.Order = ToolbarItemOrder.Default;
                        ToolbarItems.Add(new ToolbarItem("Rosary prayers", "info.png", () =>
                        {
                            Navigation.PushAsync(new StPeters.pagePrayers());
                        }));
                        break;
                    }

            } //swtich end

            string sUrl = GetDaysMysteries(pDoW);
            HtmlWebViewSource hSource = new HtmlWebViewSource();
            hSource.Html = LoadMysteryText(sUrl);

            WebView browser = new WebView
            {
                Source = hSource, //LoadMysteryText(sUrl)               
                MinimumWidthRequest = Application.Current.MainPage.Width,
                MinimumHeightRequest = Application.Current.MainPage.Height
            };

            Content = browser;

        }//pageMysteries constructor

        private string LoadMysteryText(string sMystery)
        {
            string sRtn = "";
            Assembly assem = typeof(pageMysteries).GetTypeInfo().Assembly;
            Stream streamMystery = assem.GetManifestResourceStream("StPeters.Resources." + sMystery);
            if (streamMystery != null)
            {
                using (StreamReader reader = new StreamReader(streamMystery))
                {
                    sRtn = reader.ReadToEnd();
                }
            }
            return sRtn;
        }

        private string GetDaysMysteries(DayOfWeek DoW)
        {
            //TO DO - need added logic for advent/lent
            try
            {
                string sReturn = "";

                switch (DoW)
                {
                    case DayOfWeek.Sunday:
                        sReturn = "glorious.htm";
                        break;
                    case DayOfWeek.Monday:
                        sReturn = "joyful.htm";
                        break;
                    case DayOfWeek.Tuesday:
                        sReturn = "sorrowful.htm";
                        break;
                    case DayOfWeek.Wednesday:
                        sReturn = "glorious.htm";
                        break;
                    case DayOfWeek.Thursday:
                        sReturn = "luminous.htm";
                        break;
                    case DayOfWeek.Friday:
                        sReturn = "sorrowful.htm";
                        break;
                    case DayOfWeek.Saturday:
                        sReturn = "joyful.htm";
                        break;
                }

                return sReturn;
            }
            catch (Exception)
            {
                return "";
            }

        } //GetDaysMysteries 
    } //pageMysteries ContentPage
} //ns stPetes
