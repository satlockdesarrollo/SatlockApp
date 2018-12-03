using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatlockApp.ViewModels;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.Abstractions.Enumerations;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SatlockApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        private MapViewModel Map;
        private int option;

		public MapPage ()
		{
			InitializeComponent ();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Webview.AddLocalCallback("cslog", HandleLog);
            Webview.AddLocalCallback("csfakecode", PerformFakeAction);

        }

        private void UpdateLocation(string latitud, string longitud)
        {

            string method = "updateMarker(" + latitud + ", "+longitud+")";

            Webview.InjectJavascriptAsync(method);

        }

        private void PerformFakeAction(string obj)
        {
            Webview.InjectJavascriptAsync("returnValue = \"NewReturnValue\";").ConfigureAwait(false);
                       
        }

        private void HandleLog(string obj)
        {
            System.Diagnostics.Debug.WriteLine($"Got from JS: {obj}");
        }

        private void OnNavigationStarted(object sender, Xam.Plugin.WebView.Abstractions.Delegates.DecisionHandlerDelegate e)
        {
            System.Diagnostics.Debug.WriteLine("Navigation has started");
       
         
        }

        private void OnNavigationCompleted(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigation has completed");
        }

        private void OnContentLoaded(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Content has loaded");

            var mainViewModel = MainViewModel.GetInstance();
            this.Map = mainViewModel.Map;
            this.option = mainViewModel.OptionMap;

            switch (this.option)
            {
                case 0:
                    this.Map.Latitude = mainViewModel.EventsMobile.LatitudUnidad;
                    this.Map.Longitude = mainViewModel.EventsMobile.LongitudUnidad;

                    this.Map.EventsEnabled = true;
                    this.Map.CloseEnabled = true;

                    break;
                case 1:
                    this.Map.Latitude = mainViewModel.EventsMobile.LatitudUnidad;
                    this.Map.Longitude = mainViewModel.EventsMobile.LongitudUnidad;

                    this.Map.InstallEnabled = true;
                    this.Map.CloseEnabled = true;

                    break;

                case 2:
                    this.Map.Latitude = mainViewModel.TripItem.Latitud;
                    this.Map.Longitude = mainViewModel.TripItem.Longitud;

                    this.Map.TripEnabled = true;
                    this.Map.CloseEnabled = true;

                    break;

            }
            
            this.Map.IsLoading = false;
            this.Map.IsMap = true;
           

            this.UpdateLocation(this.Map.Latitude, this.Map.Longitude);

        }

        private void OnNavigationError(object sender, int e)
        {
            System.Diagnostics.Debug.WriteLine($"An error was thrown with code: {e}");
        }
    }
}