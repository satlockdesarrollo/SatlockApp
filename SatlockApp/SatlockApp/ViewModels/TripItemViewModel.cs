namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Views;
    using Models;

    public class TripItemViewModel : TripsRequest
    {

        #region Commands
        public ICommand SelectTripCommand
        {

            get
            {
                return new RelayCommand(SelectTrip);
            }

        }

        private async void SelectTrip()
        {

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.TripItem = this;
            mainViewModel.OptionMap = 2;
            mainViewModel.Map = new MapViewModel();


            await App.Navigator.PushAsync(new MapPage());

        }

        #endregion

    }
}
