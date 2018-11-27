namespace SatlockApp.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Views;

    public class MapViewModel : BaseViewModel
    {

        private string latitude;
        private string longitude;
        private bool isLoading;
        private bool eventsEnabled;
        private bool closeEnabled;
        private bool tripEnabled;
        private bool installEnabled;
        private bool isMap;

        #region Propperties
        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                SetValue(ref this.latitude, value);
            }
        }

        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                SetValue(ref this.longitude, value);
            }
        }

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                SetValue(ref this.isLoading, value);
            }
        }

        public bool EventsEnabled
        {
            get
            {
                return eventsEnabled;
            }
            set
            {
                SetValue(ref this.eventsEnabled, value);
            }
        }

        public bool TripEnabled
        {
            get
            {
                return tripEnabled;
            }
            set
            {
                SetValue(ref this.tripEnabled, value);
            }
        }

        public bool InstallEnabled
        {
            get
            {
                return installEnabled;
            }
            set
            {
                SetValue(ref this.installEnabled, value);
            }
        }

        public bool CloseEnabled
        {
            get
            {
                return closeEnabled;
            }
            set
            {
                SetValue(ref this.closeEnabled, value);
            }
        }

        public bool IsMap
        {
            get
            {
                return isMap;
            }
            set
            {
                SetValue(ref this.isMap, value);
            }
        }

        public ICommand Close
        {
            get
            {
                return new RelayCommand(CloseMap);
            }

        }

        private async void CloseMap()
        {

            await App.Navigator.PopToRootAsync();

        }

        public ICommand Trip
        {
            get
            {
                return new RelayCommand(OpenTrip);
            }

        }

        private async void OpenTrip()
        {

            //await App.Navigator.PushAsync(new );

        }

        public ICommand Events
        {
            get
            {
                return new RelayCommand(OpenEvents);
            }

        }

        private async void OpenEvents()
        {

            //await App.Navigator.PushAsync(new );

        }

        public ICommand Installation
        {
            get
            {
                return new RelayCommand(OpenInstallation);
            }

        }

        private async void OpenInstallation()
        {

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Installation = new InstallationViewModel();

            await App.Navigator.PushAsync(new InstallationPage());

        }


        #endregion

        #region Constructors
        public MapViewModel()
        {

            this.IsLoading = true;
            this.EventsEnabled = false;
            this.CloseEnabled = false;
            this.TripEnabled = false;
            this.InstallEnabled = false;
            this.IsMap = false;
            
        }
        #endregion
    }
}
