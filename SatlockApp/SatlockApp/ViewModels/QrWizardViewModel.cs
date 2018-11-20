namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using ZXing.Net.Mobile.Forms;
    using Xamarin.Forms;
    using Services;
    using Views;
    using Renders;
    using Models;
    using Helpers;


    public class QrWizardViewModel : BaseViewModel
    {
        private PermissionValidator Permiso;
        private ApiService Api;
        private EncodeBase64 Codificator;

        private string token;
        private string user;
        private bool loading;
        private bool scanenabled;
        private string initialDate;
        private string finalDate;

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

        public bool ScanEnabled
        {
            get
            {
                return this.scanenabled;
            }
            set
            {
                SetValue(ref this.scanenabled, value);
            }
        }

        #region Commands
        public ICommand ActiveScan
        {
            get
            {
                return new RelayCommand(Scan);
            }

        }

        private async void Scan()
        {

            var status = PermissionStatus.Unknown;

            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (status != PermissionStatus.Granted)
            {

                status = await this.Permiso.validatePermission(Permission.Camera);


            }
            else
            {
                
                var ScannerPage = new ZXingScannerPage();
                ScannerPage.DefaultOverlayShowFlashButton = true;
                ScannerPage.DefaultOverlayTopText = "Alinear el código de barras dentro del marco";
                ScannerPage.Title = "Escanear Codigo QR";

                ScannerPage.OnScanResult += (result) =>
                {
                    ScannerPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Navigator.PopAsync();
                        this.finalDate = this.Codificator.EncodeTo64(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        this.ValidateQr(result.Text);  
                    });
                };

                await App.Navigator.PushAsync(ScannerPage);

            }
        }

        private async void ValidateQr(string imei)
        {

            this.Loading = true;
            this.ScanEnabled = false;

            var connection = await this.Api.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.Loading = false;
                this.ScanEnabled = true;

                DependencyService.Get<ToastMessage>().Show(connection.Message);
                return;

            }

            var apiUrl = Application.Current.Resources["APIUrlDev"].ToString();

            string parameters = "?imei="+imei+"&fic="+this.initialDate+"&ffc="+this.finalDate;

            var response = await this.Api.validateMobile(
                apiUrl,
                "/Controller",
                "/ValidacionSelloController.php",
                parameters,
                this.token,
                this.user);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");

          
                this.Loading = false;
                this.ScanEnabled = true;

                return;


            }

        }
        #endregion


        #region Constructor
        public QrWizardViewModel()
        {
            this.Loading = false;
            this.ScanEnabled = true;

            this.Permiso = new PermissionValidator();
            this.Api = new ApiService();
            this.Codificator = new EncodeBase64();

            var mainViewModel = MainViewModel.GetInstance();
            this.token = mainViewModel.Token;
            this.user = mainViewModel.User;

            this.initialDate = this.Codificator.EncodeTo64(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    

        }
        #endregion

    }
}
