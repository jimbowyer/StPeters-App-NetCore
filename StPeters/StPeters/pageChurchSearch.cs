using Xamarin.Forms;

namespace StPeters
{
    public class pageChurchSearch : ContentPage
    {
        public pageChurchSearch()
        {
            var browser = new WebView
            {

                Source = "http://rcsearch.azurewebsites.net/default.aspx?rdoT=sun&lon=-114.193291&lat=51.112827"

            };

            Content = browser;
        }
    }
} //ns

