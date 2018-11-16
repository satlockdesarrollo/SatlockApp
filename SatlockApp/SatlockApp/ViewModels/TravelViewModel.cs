namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Models;
    using Services;
    using Renders;
    using Xamarin.Forms;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using System.Linq;

    public class TravelViewModel : BaseViewModel
    {

        private ApiService Api;

        #region Attributes
        private ObservableCollection<TripItemViewModel> trips;
        private bool isRefreshing;
        private string filter;
        private string token;
        private string user;
        #endregion

        #region Propierties
        public ObservableCollection<TripItemViewModel> Trips
        {
            get { return this.trips; }
            set { SetValue(ref this.trips, value); }

        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Methods
        private async void LoadTrips()
        {

            var connection = await this.Api.CheckConnection();
            if (!connection.IsSuccess)
            {

                DependencyService.Get<ToastMessage>().Show(connection.Message);
                return;

            }

            this.IsRefreshing = true;

            var apiUrl = Application.Current.Resources["APIUrl"].ToString();
            var response = await this.Api.getTrips<Response>(
                apiUrl,
                "/Controller",
                "/DatosViajeController.php",
                this.token,
                this.user
            );

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");

                this.IsRefreshing = false;

                return;



            }

            DependencyService.Get<ToastMessage>().Show(response.Message);

            MainViewModel.GetInstance().TripsList = (List<TripsRequest>)response.Data;
            this.Trips = new ObservableCollection<TripItemViewModel>(this.ToTripItemViewModel());
            this.IsRefreshing = false;


        }

        private IEnumerable<TripItemViewModel> ToTripItemViewModel()
        {
            return MainViewModel.GetInstance().TripsList.Select(l => new TripItemViewModel
            {
                Idviaje = l.Idviaje,
                Ultimalocalizacion = l.Ultimalocalizacion,
                Alias = l.Alias,
                Sello = l.Sello,
                Contenedor = l.Contenedor,
                Subgrupo = l.Subgrupo,
                Tipo = l.Tipo,
                Fecha_inicial = l.Fecha_inicial,
                Hora_inicial = l.Hora_inicial,
                Hora_final = l.Hora_final,
                Floc = l.Floc,
                Uloc = l.Uloc,
                Estado = l.Estado,
                Motonave = l.Motonave,
                Destino = l.Destino,
                Longitud = l.Longitud,
                Latitud = l.Latitud,
                Bateria = l.Bateria,
                Tamcontenedor = l.Tamcontenedor,
                Diasmora = l.Diasmora,
                Patio = l.Patio,
                Lineanaviera = l.Lineanaviera,
                Motonaven = l.Motonaven
            });
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadTrips);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Trips = new ObservableCollection<TripItemViewModel>(
                    this.ToTripItemViewModel());
            }
            else
            {
                this.Trips = new ObservableCollection<TripItemViewModel>(
                    this.ToTripItemViewModel().Where(
                        l => l.Contenedor.ToLower().Contains(this.Filter.ToLower()) ||
                           l.Sello.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion

        #region Constructor
        public TravelViewModel()
        {

            this.Api = new ApiService();

            var mainViewModel = MainViewModel.GetInstance();
            this.token = mainViewModel.Token;
            this.user = mainViewModel.User;
            this.LoadTrips();

        }
        #endregion
    }
}
