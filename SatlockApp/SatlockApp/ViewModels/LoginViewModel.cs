namespace SatlockApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Services;
    using Views;
    using Renders;
    using Models;
    using Helpers;
    using System;

    public class LoginViewModel : BaseViewModel
    {

        private ApiService Api;

        #region Atributte
        private string username;
        private string password;
        private bool loading;
        private bool sesionenabled;
        private bool showPass;
        private string iconSee;
        #endregion

        #region Propierties
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                SetValue(ref this.username, value);
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                SetValue(ref this.password, value);
            }
        }

        public bool Loading
        {
            get
            {
                return this.loading;
            }
            set
            {
                SetValue(ref this.loading, value);
            }
        }

        public bool SesionEnabled
        {
            get
            {
                return this.sesionenabled;
            }
            set
            {
                SetValue(ref this.sesionenabled, value);
            }
        }

        public bool ShowPass
        {
            get
            {
                return this.showPass;
            }
            set
            {
                SetValue(ref this.showPass, value);
            }

        }

        public string IconSee
        {
            get
            {
                return this.iconSee;
            }
            set
            {
                SetValue(ref this.iconSee, value);
            }

        }
        #endregion

        #region Commands
        public ICommand Sesion
        {
            get
            {
                return new RelayCommand(Login);
            }

        }


        private async void Login()
        {

            if (string.IsNullOrEmpty(this.Username))
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar un nombre de usuario");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                DependencyService.Get<ToastMessage>().Show("Debe ingresar una contraseña");
                return;
            }

            this.Loading = true;
            this.SesionEnabled = false;

            var connection = await this.Api.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.Loading = false;
                this.SesionEnabled = true;

                DependencyService.Get<ToastMessage>().Show(connection.Message);
                return;


            }

            var user = new LoginRequest
            {
                User = this.Username,
                Password = this.Password,
                Ip = DependencyService.Get<IpAddress>().GetIPAddress()

            };

            var data_request = new DataRequest<LoginRequest>(user);

            var apiUrl = Application.Current.Resources["APIUrl"].ToString();

            var response = await this.Api.loginUser(
                apiUrl,
                "/Controller",
                "/TokenCreatorController.php",
                data_request);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");

                this.Password = string.Empty;

                this.Loading = false;
                this.SesionEnabled = true;

                return;


            }


            var data_api = response.Data;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = data_api.ToString();
            mainViewModel.User = this.Username;
            mainViewModel.Home = new HomeViewModel();

            Settings.Token = data_api.ToString();
            Settings.Username = this.Username;

            this.Loading = false;
            this.SesionEnabled = true;

            this.Username = string.Empty;
            this.Password = string.Empty;


            Application.Current.MainPage = new MasterPage();

        }

        public ICommand SeePass
        {
            get
            {
                return new RelayCommand(See);
            }

        }

        private void See()
        {

            if(this.ShowPass){

                this.ShowPass = false;
                this.IconSee = "fas-eye-slash";

            }
            else
            {

                this.ShowPass = true;
                this.IconSee = "fas-eye";
            }

        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.Loading = false;
            this.SesionEnabled = true;
            this.ShowPass = true;
            this.IconSee = "fas-eye";

            this.Api = new ApiService();

        }
        #endregion

    }
}
