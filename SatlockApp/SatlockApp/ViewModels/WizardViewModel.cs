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
        private bool closeEnabled;
        private bool skipEnabled;
        private bool checkEnabled;
        private int positionWizard;

        #region Propierties
        public View[] _views
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

                if (this.PositionWizard + 1 == this._views.Length - 1)
                {
                    this.NextEnabled = false;
                    this.SkipEnabled = false;

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

                if (this.PositionWizard != this._views.Length - 1)
                {
                    this.NextEnabled = true;
                    this.SkipEnabled = true;
                }
                else
                {
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
            this.PositionWizard = this._views.Length - 1;
            this.NextEnabled = false;
            this.PrevEnabled = true;
            this.SkipEnabled = false;
            this.CloseEnabled = false;
            this.CheckEnabled = false;
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
        #endregion


        #region Constructor
        public WizardViewModel()
        {
            
            this.PrevEnabled = false;
            this.NextEnabled = true;
            this.CloseEnabled = true;
            this.CheckEnabled = false;
            this.SkipEnabled = true;
            this.PositionWizard = 0;
            this.LoadSlides();

        }
        #endregion

        #region Methods
        private void LoadSlides()
        {
            this._views = new View[]
            {
                new Wizard1View(),
                new Wizard1View(),
                new QrWizardView(),
     
              
            };

        }

         #endregion
    }
}
