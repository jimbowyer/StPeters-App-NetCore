using System;
using Xamarin.Forms;


namespace StPeters
{
    class pageWebSite : ContentPage
    {
        Button btnBulletinLast = new Button
        {
            Text = "Last Week's",
            BorderWidth = 0,
        };

        public pageWebSite()
        {

            btnBulletinLast.Clicked += (sender, args) =>
            {
                try
                {
                    //Uri uBull = new Uri(GetBulletinUrl(DateTime.Now.Subtract(TimeSpan.FromDays(7))));

                    //if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.UWP)
                    //{
                    //    Device.OpenUri(uBull);
                    //}
                    //else if (Device.RuntimePlatform == Device.Android)
                    //{
                    //    LoadPDF(this.btnBulletinLast, uBull);
                    //}
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error!", "Sorry, error loading bulletin: " + ex.Message, "cancel");
                }
            }; // btnBulletinLast.Clicked

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                        //_lblHeader,
                        //_lvHomilies,
                        //lblBulletins,
                        new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = { btnBulletinLast }
                    }
                }
            };
        } //pageWebSite costructor

    } //class pageWebSite
} //ns
