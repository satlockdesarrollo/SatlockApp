using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatlockApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatlockApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();

            /*var mainViewModel = MainViewModel.GetInstance();
            var map = mainViewModel.Map;

            this.Title = map.Title;*/
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

        }

        private void OnNavigationError(object sender, int e)
        {
            System.Diagnostics.Debug.WriteLine($"An error was thrown with code: {e}");
        }
    }
}