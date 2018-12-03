namespace SatlockApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SatlockApp.Models;
    using SatlockApp.Renders;
    using SatlockApp.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class InstallationViewModel : BaseViewModel
    {
        private ApiService Api;

        private string unidad;
        private string contenedor;
        private int tamano;
        private int control;
        private string embarcacion;
        private string maxDate;
        private string minDate;
        private string eta;
        private string cargue;
        private string despacho;
        private string direccion;
        private string observation;
        private bool isLoading;
        private bool installationEnabled;

        private string token;
        private string user;

        public string Unidad
        {
            get
            {
                return unidad;
            }
            set
            {
                SetValue(ref this.unidad, value);
            }
        }

        public string Contenedor
        {
            get
            {
                return contenedor;
            }
            set
            {
                SetValue(ref this.contenedor, value);
            }
        }

        public int Tamano
        {
            get
            {
                return tamano;
            }
            set
            {
                SetValue(ref this.tamano, value);
            }
        }

        public int Control
        {
            get
            {
                return control;
            }
            set
            {
                SetValue(ref this.control, value);
            }
        }

        public string Embarcacion
        {
            get
            {
                return embarcacion;
            }
            set
            {
                SetValue(ref this.embarcacion, value);
            }
        }

        public string MaxDate
        {

            get
            {
                return maxDate;
            }
            set
            {
                SetValue(ref this.maxDate, value);
            }
        }

        public string MinDate
        {

            get
            {
                return minDate;
            }
            set
            {
                SetValue(ref this.minDate, value);
            }
        }

        public string Eta
        {

            get
            {
                return eta;
            }
            set
            {
                SetValue(ref this.eta, value);
            }
        }

        public string Cargue
        {

            get
            {
                return cargue;
            }
            set
            {
                SetValue(ref this.cargue, value);
            }
        }
        
        public string Despacho
        {

            get
            {
                return despacho;
            }
            set
            {
                SetValue(ref this.despacho, value);
            }
        }

        public string Direccion
        {

            get
            {
                return direccion;
            }
            set
            {
                SetValue(ref this.direccion, value);
            }
        }

        public string Observation
        {

            get
            {
                return observation;
            }
            set
            {
                SetValue(ref this.observation, value);
            }
        }

        public string User
        {

            get
            {
                return user;
            }
            set
            {
                SetValue(ref this.user, value);
            }
        }

        public string Token
        {

            get
            {
                return token;
            }
            set
            {
                SetValue(ref this.token
, value);
            }
        }

        public bool InstallationEnabled
        {

            get
            {
                return installationEnabled;
            }
            set
            {
                SetValue(ref this.installationEnabled, value);
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

        public ICommand Installation
        {
            get
            {
                return new RelayCommand(Create);
            }

        }

        private async void Create()
        {
            if (string.IsNullOrEmpty(this.Contenedor))
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar un número de contenedor");
                return;
            }

            if (this.Tamano == 0)
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar el tamaño del contenedor");
                return;
            }

            if (this.Control == 0)
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar el número de puerto");
                return;
            }

            if (string.IsNullOrEmpty(this.Direccion))
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar una dirección");
                return;
            }

            if (string.IsNullOrEmpty(this.Observation))
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar una observación");
                return;
            }

            this.IsLoading = true;
            this.InstallationEnabled = false;

            var connection = await this.Api.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsLoading = false;
                this.InstallationEnabled = true;

                DependencyService.Get<ToastMessage>().Show(connection.Message);
                return;

            }


            var trip = new InstallationRequest
            {
                Placa = this.Unidad,
                Contenedor = this.Contenedor,
                Idtamcontenedor = this.Tamano,
                Idpuertocontrol = this.Control,
                Motonave = this.Embarcacion,
                Eta = this.Eta,
                FCargue = this.Cargue,
                FechaDespacho = this.Despacho,
                DireccionOrigen = this.Direccion,
                Estado = "",
                Observaciones = this.Observation

            };

            var data_request = new DataRequest<InstallationRequest>(trip);

            var apiUrl = Application.Current.Resources["APIUrlDev"].ToString();

            var response = await this.Api.CreateTrip(
                apiUrl,
                "/Controller",
                "/AsignacionController.php",
                this.Token,
                this.User,
                data_request);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");

                this.IsLoading = false;
                this.InstallationEnabled = true;

                return;


            }


            await Application.Current.MainPage.DisplayAlert(
                  "Exito",
                  response.Message,
                  "Aceptar");

            await App.Navigator.PopToRootAsync();



        }


        public InstallationViewModel()
        {
            this.updateDates();
            this.InstallationEnabled = true;
            this.IsLoading = false;
            this.Tamano = 0;
            this.Control = 0;

            this.Api = new ApiService();

            var mainViewModel = MainViewModel.GetInstance();
            this.Unidad = mainViewModel.MobileImei;
            this.Token = mainViewModel.Token;
            this.User = mainViewModel.User;
        }

        private void updateDates()
        {

            var now = DateTime.Now;
            this.Eta = now.ToString("MM/dd/yyyy");
            this.Cargue = now.ToString("MM/dd/yyyy");
            this.Despacho = now.ToString("MM/dd/yyyy");

            var max = now.AddMonths(6);
            this.MaxDate = max.ToString("MM/dd/yyyy");

            var min = now.AddDays(-1);
            this.MinDate = min.ToString("MM/dd/yyyy");

        }

    }
}
