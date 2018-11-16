namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;

    public class MainViewModel
    {
        #region Propierties
        public List<TripsRequest> TripsList
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public MapViewModel Map
        {
            get;
            set;
        }

        public ProfileViewModel Profile
        {
            get;
            set;
        }

        public HomeViewModel Home
        {
            get;
            set;
        }

        public TravelViewModel Travel
        {
            get;
            set;
        }

        public WizardViewModel Wizard
        {
            get;
            set;
        }
        #endregion

        #region Contructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();

        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {

            if (instance == null)
            {

                return new MainViewModel();

            }

            return instance;

        }
        #endregion

        #region Methods
        private void LoadMenu()
        {

            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-user",
                PageName = "ProfilePage",
                Title = "Mi Perfil"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-cogs",
                PageName = "ConfigurationPage",
                Title = "Opciones"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-comments",
                PageName = "SupportPage",
                Title = "Soporte"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-info-circle",
                PageName = "LegalPage",
                Title = "Acerca de"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-door-open",
                PageName = "LoginPage",
                Title = "Cerrar Sesión"

            });
        }
        #endregion
    }
}
