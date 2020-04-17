using System;
using Xamarin.Forms;

namespace StPeters
{
    //Page to display daily reflection on gospel reading
    class pageReadings : ContentPage
    {
        public pageReadings()
        {
            const string cSCRIPTUREWEB = "https://dailyscripture.servantsoftheword.org/";

            string sYear = DateTime.Now.Year.ToString();
            string sMon = DateTime.Now.ToString("MMM").ToLower();
            string sDay = DateTime.Now.Day.ToString();
            string sUrl = cSCRIPTUREWEB + "readings/" + sYear + "/" + sMon + sDay + ".htm";

            ToolbarItem tbMenu = new ToolbarItem
            {
                Text = "| Mass Readings",
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
                            Navigation.PushAsync(new pageMassReadings());
                        };
                        break;
                    }
                case (Device.Android):
                    {
                        ToolbarItems.Add(new ToolbarItem("| Mass Readings", "", () =>
                        {
                            Navigation.PushAsync(new pageMassReadings());
                        }));
                        break;
                    }
                case (Device.UWP):
                    {
                        tbMenu.IconImageSource = "info.png";
                        tbMenu.Order = ToolbarItemOrder.Default;
                        ToolbarItems.Add(new ToolbarItem("| Mass Readings", "info.png", () =>
                        {
                            //Navigation.PushAsync(new PageReadingsMass());
                        }));
                        break;
                    }

            } //swtich end

            WebView browser = new WebView
            {
                Source = sUrl,
                MinimumWidthRequest = Application.Current.MainPage.Width
            };

            Content = browser;
            
        }
    } //class pageReadings


}
