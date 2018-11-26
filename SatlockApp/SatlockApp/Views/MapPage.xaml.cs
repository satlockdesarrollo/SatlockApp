using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatlockApp.ViewModels;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.Abstractions.Enumerations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatlockApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        private MapViewModel Map;

		public MapPage ()
		{
			InitializeComponent ();

            Webview.AddLocalCallback("cslog", HandleLog);
            Webview.AddLocalCallback("csfakecode", PerformFakeAction);

            

        }

        private void PerformFakeAction(string obj)
        {
            Webview.InjectJavascriptAsync("returnValue = \"NewReturnValue\";").ConfigureAwait(false);
                       
        }

        private void HandleLog(string obj)
        {
            System.Diagnostics.Debug.WriteLine($"Got from JS: {obj}");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Webview.InjectJavascriptAsync("doWork(122345, 232123121)");
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
            Loader.IsRunning = false;
            Webview.InjectJavascriptAsync("doWork(122345, 232123121)");

        }

        private void OnNavigationError(object sender, int e)
        {
            System.Diagnostics.Debug.WriteLine($"An error was thrown with code: {e}");
        }
    }
}