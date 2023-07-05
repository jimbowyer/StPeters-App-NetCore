
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;

namespace StPeters
{
    public class pageMassReadings : TabbedPage
    {
        const string cNOVALIS = "https://readings.livingwithchrist.ca/";
        const string cREAD1 = "daily-texts/reading/";
        const string cPSALM = "daily-texts/psalm/";
        const string cREAD2 = "daily-texts/reading2/";
        const string cGOSPEL = "daily-texts/gospel/";

        string sDate = DateTime.Now.ToString("yyyy-MM-dd");
        string sDateFormatted = DateTime.Now.ToString("m");
        string[] sReadings = new string[4];
        HttpClient hcShared = new HttpClient(); //declare here and ref for reuse to reduce instance cost

        public pageMassReadings()
        {
            this.Title = "Mass Readings for " + sDateFormatted;
            this.Children.Add(new tabReading(cNOVALIS + cREAD1 + sDate, "1st Reading", ref hcShared) { });
            this.Children.Add(new tabReading(cNOVALIS + cPSALM + sDate, "Psalm", ref hcShared) { });
            this.Children.Add(new tabReading(cNOVALIS + cREAD2 + sDate, "2nd Reading", ref hcShared) { });
            this.Children.Add(new tabReading(cNOVALIS + cGOSPEL + sDate, "Gospel", ref hcShared) { });

        } //ctor

    } //class PageReadingsMass

    public class tabReading : ContentPage
    {
        Uri uri2Get;
        string sRead2Show = string.Empty;
        HttpClient hClient;

        public tabReading(string argReadUrl, string argTitle, ref HttpClient argHClient)
        {
            this.Title = argTitle;
            uri2Get = new Uri(argReadUrl);
            hClient = argHClient;
        }

        override protected void OnAppearing()
        {
            base.OnAppearing();
            if (sRead2Show == "") sRead2Show = GetReading(uri2Get); //reduce reloading here

            Content = new WebView
            {
                Source = new HtmlWebViewSource
                {
                    Html = sRead2Show
                },
                HeightRequest = Application.Current.MainPage.Height,
                WidthRequest = Application.Current.MainPage.Width,
                VerticalOptions = LayoutOptions.Center
            };
        }

        private string GetReading(Uri argUri2Get)
        {
            try
            {
                if (hClient == null) { hClient = new HttpClient(); }
                HttpResponseMessage response;
                HttpContent responseContent;
                
                response = Task.Run(() => hClient.GetAsync(argUri2Get)).Result;
                responseContent = response.Content;
                return Task.Run(() => responseContent.ReadAsStringAsync()).Result;
            }
            catch (Exception ex)
            {
                return "error loading reading: " + ex.Message;
            }
        }

    } //class tabReading
} //ns
