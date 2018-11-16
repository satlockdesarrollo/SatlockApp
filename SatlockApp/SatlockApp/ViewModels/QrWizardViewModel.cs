namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using ZXing.Net.Mobile.Forms;

    public class QrWizardViewModel : BaseViewModel
    {
        private PermissionValidator Permiso;

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

                ScannerPage.Title = "Lector de QR";
                ScannerPage.OnScanResult += (result) =>
                {
                    ScannerPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        App.Navigator.PopAsync();
                        Application.Current.MainPage.DisplayAlert(
                            "Resultado de scan",
                            result.Text,
                            "Aceptar");
                    });
                };

                await App.Navigator.PushAsync(ScannerPage);

            }
        }
        #endregion


        #region Constructor
        public QrWizardViewModel()
        {
            this.Permiso = new PermissionValidator();

        }
        #endregion

    }
}
