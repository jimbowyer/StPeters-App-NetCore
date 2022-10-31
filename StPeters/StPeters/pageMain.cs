using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StPeters
{
    class pageMain : ContentPage
    {
        Color mcolorBack = new Xamarin.Forms.Color();
        Color mcolorText = new Xamarin.Forms.Color();
        string mstrSeason;
        string mstrYearCycle;
        DayOfWeek mDoW;

        public pageMain()
        {
            const string cWEB = "www.st-peters.ca";
            const string cSEARCH = "www.google.com/maps/search/catholic/@";
            const string cDEFQUE = "51.114515,-114.2201127,11z?hl=en-CA";
            GetSeasonVars(ref mstrSeason, ref mcolorBack, ref mcolorText, ref mstrYearCycle);
            mDoW = WhatDay();         

            Button btnReading = new Button
            {
                Text = "Reflections - " + DateTime.Now.ToString("m"), 
                BackgroundColor = mcolorBack,
                TextColor = mcolorText,
                BorderWidth = 0,
                BorderColor = mcolorBack,
                WidthRequest = 290
            };

            btnReading.Clicked += (sender, args) =>
            {
                //open readings page:
                Navigation.PushAsync(new pageReadings());
            };

            Button btnMassReading = new Button
            {
                Text = "Mass Readings - " + " " + mstrSeason + " (" + mstrYearCycle + ")",
                BackgroundColor = mcolorBack,
                TextColor = mcolorText,
                BorderWidth = 0,
                BorderColor = mcolorBack
            };

            btnMassReading.Clicked += (sender, args) =>
            {
                //open readings page:
                Navigation.PushAsync(new pageMassReadings());
            };

            Button btnChurchSearch = new Button
            {
                Text = "Find Mass/Reconciliation",
                BackgroundColor = mcolorBack,
                TextColor = mcolorText,
                BorderWidth = 0,
                BorderColor = mcolorBack
            };

            btnChurchSearch.Clicked += async (sender, args) =>
            {
                Uri uri;
                Location location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    string sLong = location.Longitude.ToString();
                    string sLat = location.Latitude.ToString();
                    uri = new Uri("https://" + cSEARCH + sLat + "," + sLong + ",13z");
                }
                else
                {
                    //use default position
                    uri = new Uri("https://" + cSEARCH + cDEFQUE);
                }

                //open church search in browser:                
                await Launcher.OpenAsync(uri);

            };

            Button btnBulletins = new Button
            {
                Text = "Parish Web/Bulletins",
                BackgroundColor = mcolorBack,
                TextColor = mcolorText,
                BorderWidth = 0,
                BorderColor = mcolorBack
            };

            btnBulletins.Clicked += (sender, args) =>
            {
                //open parish bulletins page in browser:                
                Uri uri = new Uri("http://" + cWEB);
                Launcher.OpenAsync(uri);
            };

            Button btnMysteries = new Button
            {
                Text = TodaysMysteries(mDoW),
                BackgroundColor = mcolorBack,
                TextColor = mcolorText,
                BorderWidth = 0,
                BorderColor = mcolorBack
            };
            btnMysteries.Clicked += (sender, args) =>
            {
                //open rosary mysteries page:                
                Navigation.PushAsync(new pageMysteries(mDoW));
            };

            Image imgBack = new Image
            {
                Source = "stPete.png",
                IsVisible = true,
                Aspect = Aspect.AspectFill
            };

            ToolbarItem tbMenu = new ToolbarItem
            {
                Text = "about",
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
                            Navigation.PushAsync(new pageAbout());
                        };
                        break;
                    }
                case (Device.Android):
                    {

                        ToolbarItems.Add(new ToolbarItem("About", "info.png", () =>
                        {
                            Navigation.PushAsync(new pageAbout());
                        }));
                        break;
                    }
                case (Device.UWP):
                    {
                        tbMenu.IconImageSource = "info.png";
                        tbMenu.Order = ToolbarItemOrder.Default;
                        ToolbarItems.Add(new ToolbarItem("About", "info.png", () =>
                        {
                            Navigation.PushAsync(new pageAbout());
                        }));
                        break;
                    }

            } //swtich end

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.End,
                Padding = new Thickness(5),
                Spacing = 10,
                Children =
                    {
                     btnReading, btnMassReading, btnChurchSearch, btnBulletins, btnMysteries
                    }
            };

            BackgroundImageSource = "stPete.png";

        } //constructor - pageMain


        private void GetSeasonVars(ref string pSeason, ref Color pcolorBack, ref Color pcolorText, ref string pCycle)
        {
            RomCal cal = new RomCal();
            Season seaNow;
            clsChurchYear litCal = new clsChurchYear();

            try
            {
                seaNow = cal.SeasonOf(DateTime.Now);
                pSeason = seaNow.SeasonName;
                pCycle = litCal.GetChurchYear(DateTime.Now).ToString();

                switch (seaNow.SeasonColor)
                {
                    case Season.Colors.Green:
                        pcolorBack = Color.Green;
                        pcolorText = Color.White;
                        break;
                    case Season.Colors.Purple:
                        pcolorBack = Color.Purple;
                        pcolorText = Color.White;
                        break;
                    case Season.Colors.Red:
                        pcolorBack = Color.Red;
                        pcolorText = Color.White;
                        break;
                    case Season.Colors.White:
                        pcolorBack = Color.White;
                        pcolorText = Color.Silver;
                        break;
                    default:
                        pcolorBack = Color.Green;
                        pcolorText = Color.White;
                        break;
                }
            }
            catch (Exception)
            {
                //we errored - log later - rtn safe
                pSeason = "Undefined Season";
                pcolorBack = Color.Blue;
                pcolorText = Color.White;
            }

        } //GetSeasonVars

        private DayOfWeek WhatDay()
        {
            DateTime dteNow = DateTime.Now;
            return dteNow.DayOfWeek;
        }

        private string TodaysMysteries(DayOfWeek DoW)
        {
            try
            {
                string sReturn = "";

                switch (DoW)
                {
                    case DayOfWeek.Sunday:
                        sReturn = "Today - Glorious Mysteries";
                        break;
                    case DayOfWeek.Monday:
                        sReturn = "Today - Joyful Mysteries";
                        break;
                    case DayOfWeek.Tuesday:
                        sReturn = "Today - Sorrowful  Mysteries";
                        break;
                    case DayOfWeek.Wednesday:
                        sReturn = "Today - Glorious  Mysteries";
                        break;
                    case DayOfWeek.Thursday:
                        sReturn = "Today - Luminous  Mysteries";
                        break;
                    case DayOfWeek.Friday:
                        sReturn = "Today - Sorrowful  Mysteries";
                        break;
                    case DayOfWeek.Saturday:
                        sReturn = "Today - Joyful  Mysteries";
                        break;
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } //TodaysMysteries
    } //class pageMain
} //ns
