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
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    public class QrWizardViewModel : BaseViewModel
    {
        private PermissionValidator Permiso;
        private ApiService Api;
        private EncodeBase64 Codificator;
        private WizardViewModel Wizard;

        private string token;
        private string user;
        private bool loading;
        private bool scanEnabled;
        private bool isAnalyzing;
        private bool isScanning;
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
                return this.scanEnabled;
            }
            set
            {
                SetValue(ref this.scanEnabled, value);
            }
        }

        public bool IsScanning
        {
            get
            {
                return this.isScanning;
            }
            set
            {
                SetValue(ref this.isScanning, value);
            }

        }

        public bool IsAnalyzing
        {
            get
            {
                return this.isAnalyzing;
            }
            set
            {
                SetValue(ref this.isAnalyzing, value);
            }
        }

        public ZXing.Result Result
        {
            get;
            set;
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
                var expectedFormat = ZXing.BarcodeFormat.QR_CODE;
                var opts = new ZXing.Mobile.MobileBarcodeScanningOptions
                {
                    PossibleFormats = new List<ZXing.BarcodeFormat> { expectedFormat }
                };

                var ScannerPage = new ZXingScannerPage(opts);
                               
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

            //string parameters = "?imei="+imei+"&fic="+this.initialDate+"&ffc="+this.finalDate;
            string parameters = "?imei="+imei+ "&fic=MjAxOC0xMS0xMyAwMDowMDowMA==&ffc=MjAxOC0xMS0xNSAyMzo1OTo1OQ==";

            var response = await this.Api.validateMobile<Response>(
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

            await Application.Current.MainPage.DisplayAlert(
                   "Exito",
                   response.Message,
                   "Aceptar");

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EventsMobile = (MobileRequest) response.Data;

            var temp1 = JsonConvert.SerializeObject(mainViewModel.EventsMobile);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(temp1);
            var temp2 = System.Convert.ToBase64String(plainTextBytes);

            //this.MapWizard.MapUrl = "https://www.ultrackonline.com/pagos/webview/mapa2.html?data%" + temp2;
            
            this.Loading = false;
            this.ScanEnabled = true;

            this.Wizard.NextEnabled = true;

            


        }

        public ICommand QRScanResult
        {
            get
            {
                return new RelayCommand(ResultScan);
            }

        }

        private void ResultScan()
        {
            this.IsAnalyzing = false;
            this.IsScanning = false;

            Device.BeginInvokeOnMainThread(() =>
            {
                this.finalDate = this.Codificator.EncodeTo64(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                this.ValidateQr(Result.Text);
            });

        }
        #endregion


        #region Constructor
        public QrWizardViewModel()
        {
            this.Loading = false;
            this.ScanEnabled = true;

            this.IsAnalyzing = false;
            this.IsScanning = false;

            this.Permiso = new PermissionValidator();
            this.Api = new ApiService();
            this.Codificator = new EncodeBase64();

            var mainViewModel = MainViewModel.GetInstance();
            this.token = mainViewModel.Token;
            this.user = mainViewModel.User;
            this.Wizard = mainViewModel.Wizard;
           

            this.initialDate = this.Codificator.EncodeTo64(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    

        }
        #endregion

    }
}
