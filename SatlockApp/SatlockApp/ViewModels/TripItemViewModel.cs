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
            MainViewModel.GetInstance().Map = new MapViewModel(this);
            await App.Navigator.PushAsync(new MapPage());
        }

        #endregion

    }
}
