namespace SatlockApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;
    using Helpers;
    using System.Threading.Tasks;

    public class MenuItemViewModel
    {
        #region Propierties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        public string Color { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {

            get
            {
                return new RelayCommand(Navigate);
            }

        }

        private async void Navigate()
        {
            App.Master.IsPresented = false;

            switch (this.PageName)
            {
                case "LoginPage":

                    var logout = await Application.Current.MainPage.DisplayAlert("Atención", "Desea realmente cerrar la sesión actual", "Cerrar", "Cancelar");

                    if (logout)
                    {

                        Settings.Token = string.Empty;

                        var mainViewModel = MainViewModel.GetInstance();
                        mainViewModel.Token = string.Empty;
                        mainViewModel.User = string.Empty;

                        Application.Current.MainPage = new NavigationPage(new LoginPage());

                    }
                    break;
                case "TravelPage":

                    MainViewModel.GetInstance().Travel = new TravelViewModel();
                    await App.Navigator.PushAsync(new TravelPage());

                    break;
                case "WizardPage":

                    MainViewModel.GetInstance().Wizard = new WizardViewModel();
                    await App.Navigator.PushAsync(new WizardPage());

                    break;
                case "ProfilePage":

                    MainViewModel.GetInstance().Profile = new ProfileViewModel();
                    await App.Navigator.PushAsync(new ProfilePage());

                    break;
                default:

                    break;
            }


        }

        #endregion

    }
}
