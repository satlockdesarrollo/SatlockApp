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

    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        #region Constructor
        public HomeViewModel()
        {

            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-check-double",
                PageName = "WizardPage",
                Title = "Verificación de Sellos"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-route",
                PageName = "NewTravelPage",
                Title = "Iniciar Nuevo Viaje"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-history",
                PageName = "TravelPage",
                Title = "Historial de Viajes"

            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "fas-question-circle",
                PageName = "QuestionPage",
                Title = "Preguntas Frecuentes"

            });
        }
        #endregion

    }
}
