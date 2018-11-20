namespace SatlockApp.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using SatlockApp.Views;
    using SatlockApp.Renders;

    public class WizardViewModel : BaseViewModel
    {

        private bool nextenabled;
        private bool prevenabled;

        #region Propierties
        public View[] _views
        {
            get;
            set;
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
        #endregion


        #region Constructor
        public WizardViewModel()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.QrWizard = new QrWizardViewModel();
            this.LoadSlides();

        }
        #endregion

        #region Methods
        private void LoadSlides()
        {
            this._views = new View[]
            {
                new QrWizardView(),
                new Wizard1View()
              
            };

        }

        #endregion
    }
}
