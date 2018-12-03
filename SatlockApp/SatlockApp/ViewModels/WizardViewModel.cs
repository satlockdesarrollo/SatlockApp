namespace SatlockApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using SatlockApp.Views;
    using SatlockApp.Renders;
    using SatlockApp.Models;
    using SatlockApp.Helpers;
    using SatlockApp.Services;
    using Plugin.Permissions.Abstractions;
    using Plugin.Permissions;
    using ZXing.Net.Mobile.Forms;

    public class WizardViewModel : BaseViewModel
    {

        private bool nextenabled;
        private bool prevenabled;
        private bool closeEnabled;
        private bool skipEnabled;
        private bool checkEnabled;
        private bool loading;
        private int positionWizard;
        private int optionWizard;

        private PermissionValidator Permiso;
        private ApiService Api;
        private EncodeBase64 Codificator;

        private string token;
        private string user;
        private bool isAnalyzing;
        private bool isScanning;
        //private string initialDate;
        private string finalDate;


        #region Propierties
        public ObservableCollection<WizardModel> _views
        {
            get;
            set;
        }

        public int PositionWizard
        {
            get
            {
                return this.positionWizard;
            }
            set
            {
                SetValue(ref this.positionWizard, value);
            }
        }

        public bool NextEnabled
        {
            get
            {
                return this.nextenabled;
            }
            set
            {
                SetValue(ref this.nextenabled, value);
            }
        }

        public bool PrevEnabled
        {
            get
            {
                return this.prevenabled;
            }
            set
            {
                SetValue(ref this.prevenabled, value);
            }
        }

        public bool CloseEnabled
        {
            get
            {
                return this.closeEnabled;
            }
            set
            {
                SetValue(ref this.closeEnabled, value);
            }
        }

        public bool SkipEnabled
        {
            get
            {
                return this.skipEnabled;
            }
            set
            {
                SetValue(ref this.skipEnabled, value);
            }

        }

        public bool CheckEnabled
        {
            get
            {
                return this.checkEnabled;
            }
            set
            {
                SetValue(ref this.checkEnabled, value);
            }
        }

        public int OptionWizard
        {
            get
            {
                return this.optionWizard;
            }
            set
            {
                SetValue(ref this.optionWizard, value);
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

        public ZXing.Result Result
        {
            get;
            set;
        }

        #endregion

        #region Commands
        public ICommand NextSlide
        {
            get
            {
                return new RelayCommand(Next);
            }

        }

        private void Next()
        {
            if (this.PositionWizard != 0)
            {

                if (this.PositionWizard + 1 == this._views.Count - 1)
                {
                    this.NextEnabled = false;
                    this.SkipEnabled = false;
                    this.CheckEnabled = true;

                }

                this.PositionWizard = this.PositionWizard + 1;

            }
            else
            {
                this.PrevEnabled = true;
                this.CloseEnabled = false;

                this.PositionWizard = this.PositionWizard + 1;
            }
        }

        public ICommand PrevSlide
        {
            get
            {
                return new RelayCommand(Prev);
            }

        }

        private void Prev()
        {
            
            if (this.PositionWizard != 0)
            {
                this.PositionWizard = this.PositionWizard - 1;

                if (this.PositionWizard != this._views.Count - 1)
                {
                    this.NextEnabled = true;
                    this.SkipEnabled = true;
                    this.CheckEnabled = false;
                }
                

                if (this.PositionWizard == 0)
                {

                    this.PrevEnabled = false;
                    this.CloseEnabled = true;

                }
            }
          
        }

        public ICommand SkipSlide
        {
            get
            {
                return new RelayCommand(Skip);
            }

        }

        private void Skip()
        {
            this.PositionWizard = this._views.Count - 1;
            this.NextEnabled = false;
            this.PrevEnabled = true;
            this.SkipEnabled = false;
            this.CloseEnabled = false;
            this.CheckEnabled = true;
        }

        public ICommand CloseSlide
        {
            get
            {
                return new RelayCommand(Close);
            }

        }

        private async void Close()
        {

            await App.Navigator.PopAsync();

        }

        public ICommand OpenQr
        {
            get
            {
                return new RelayCommand(Qr);
            }

        }

        private async void Qr()
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

        private async void ValidateQr(string imei)
        {

            this.Loading = true;
            this.CheckEnabled = false;

            var connection = await this.Api.CheckConnection();

            if (!connection.IsSuccess)
            {
                DependencyService.Get<ToastMessage>().Show(connection.Message);
                return;

            }

            var apiUrl = Application.Current.Resources["APIUrlDev"].ToString();

            //string parameters = "?imei="+imei+"&fic="+this.initialDate+"&ffc="+this.finalDate;
            string parameters = "?imei=" + imei + "&fic=MjAxOC0xMS0xMyAwMDowMDowMA==&ffc=MjAxOC0xMS0xNSAyMzo1OTo1OQ==";

            var response = await this.Api.ValidateMobile<Response>(
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
               this.checkEnabled = true;

                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                   "Exito",
                   response.Message,
                   "Aceptar");

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EventsMobile = (MobileRequest)response.Data;
            mainViewModel.OptionMap = this.OptionWizard;
            mainViewModel.Map = new MapViewModel();
            mainViewModel.MobileImei = imei;

            this.Loading = false;
            this.CheckEnabled = true;

            await App.Navigator.PushAsync(new MapPage());


        }
        #endregion


        #region Constructor
        public WizardViewModel()
        {
            
            this.PrevEnabled = false;
            this.NextEnabled = true;
            this.CloseEnabled = true;
            this.CheckEnabled = false;
            this.SkipEnabled = true;
            this.Loading = false;

            this.Permiso = new PermissionValidator();
            this.Api = new ApiService();
            this.Codificator = new EncodeBase64();


            this.IsAnalyzing = false;
            this.IsScanning = false;

            var mainViewModel = MainViewModel.GetInstance();
            this.token = mainViewModel.Token;
            this.user = mainViewModel.User;



            this.PositionWizard = 0;
            this.LoadSlides();

        }
        #endregion

        #region Methods
        private void LoadSlides()
        {
            this._views = new ObservableCollection<WizardModel>();

            this._views.Add(new WizardModel
            {
                ImageUrl="Paso01.json",
                Title="Apertura de caja",
                Description=""
            });

            this._views.Add(new WizardModel
            {
                ImageUrl = "Paso02.json",
                Title = "Escanear Codigo QR",
                Description = ""
            });


            this._views.Add(new WizardModel
            {
                ImageUrl = "Paso03.json",
                Title = "Encender Unidad",
                Description = ""
            });

            this._views.Add(new WizardModel
            {
                ImageUrl = "Paso03A.json",
                Title = "Validación de Leds",
                Description = ""
            });

            this._views.Add(new WizardModel
            {
                ImageUrl = "Paso03B.json",
                Title = "Verificación de Batería",
                Description = ""
            });

            this._views.Add(new WizardModel
            {
                ImageUrl = "Paso04.json",
                Title = "Instalación en Contenedor",
                Description = ""
            });


        }

         #endregion
    }
}
