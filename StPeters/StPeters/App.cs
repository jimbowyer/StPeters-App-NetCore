using System;
using Xamarin.Forms;
using System.Security;

namespace StPeters
{
    public partial class App : Application
    {
        [SecuritySafeCriticalAttribute]
        public App()
        {
            try
            {
                // The root page of application
                MainPage = new NavigationPage(new pageMain() { Title = " St. Peters" })
                {                                       
                    BarTextColor = Color.Silver,
                    BarBackgroundColor = Color.Black
                };
                NavigationPage.SetHasNavigationBar(this, true);            
            }
            catch (Exception ex)
            {
                MainPage.DisplayAlert("app error", ex.Message, "OK");
            }
        } //App

        protected override void OnStart()
        {
            // Handle when app starts... 

            //----REM FOR NOW - come back to later if use App Center telemetry----//
            // #1. AppCenter Analytics:
            //clsConfig cfg = new clsConfig();
            //AppCenter.Start("android=" + cfg.AppCenterAndroidKey +";" +
            //      "uwp={Your UWP App secret here};" +
            //      "ios=" + cfg.AppCenterIosKey + ";",
            //      typeof(Analytics), typeof(Crashes));

            //#if DEBUG
            //    AppCenter.LogLevel = LogLevel.Verbose;
            //#endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    } //Class App
} //ns
