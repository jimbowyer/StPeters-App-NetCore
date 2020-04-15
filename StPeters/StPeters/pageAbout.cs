using Xamarin.Forms;
using System.Reflection;
using System;
using Xamarin.Essentials;

namespace StPeters
{
    public class pageAbout : ContentPage
    {
        public pageAbout()
        {
            const string cSTR_ABOUT = "Produced by the grace of God & with the prayers and support of St Peters RC Parish, Calgary. Content copyright of originators & shared with their consent.";
            const string cWEB = "www.st-peters.ca";
            var assembly = typeof(StPeters.App).GetTypeInfo().Assembly;
            var assemName = new AssemblyName(assembly.FullName);
            string sVersion = "Version: " + assemName.Version.ToString();

            BackgroundColor = Color.White;

            Button btnLink = new Button() { Text = cWEB, TextColor = Color.Blue };
            btnLink.Clicked += (sender, args) =>
            {
                Uri uri = new Uri("http://" + cWEB);
                //Device.OpenUri(uri);
                Launcher.OpenAsync(uri);
            };

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Padding = new Thickness(5),
                Spacing = 30,
                Children = {
                    new Image
                    {
                        Source = "about.jpg",
                        IsVisible = true,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Label { Text = sVersion, TextColor= Color.Black, HorizontalTextAlignment= TextAlignment.Center},
                    new Label { Text = cSTR_ABOUT, TextColor= Color.Black, HorizontalTextAlignment= TextAlignment.Center},
                    //new Label { Text = cWEB, TextColor= Color.Blue, HorizontalTextAlignment= TextAlignment.Center},
                    btnLink
                }
            };

        } //pageAbout
    }
}
