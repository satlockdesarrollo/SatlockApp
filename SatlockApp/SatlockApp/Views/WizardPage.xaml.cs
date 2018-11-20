namespace SatlockApp.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Renders;
    using SatlockApp.ViewModels;

    public partial class WizardPage : ContentPage
    {

        public View[] views;
        public WizardViewModel wizard;

        public WizardPage()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            this.wizard = mainViewModel.Wizard;
            this.wizard.PrevEnabled = false;
            this.wizard.NextEnabled = true;
            this.views = this.wizard._views;
        }

        private void OnCarouselPositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            var currentView = this.views[e.NewValue];
            
            if (currentView is IAnimated animatedView)
            {
                animatedView.StartAnimation();
            }
        }

        private void nextPage(object sender, EventArgs e)
        {

           if(carouselWizard.Position == 0)
            {

                this.wizard.PrevEnabled = false;
                if (carouselWizard.Position + 1 != this.views.Length)
                {
                    carouselWizard.Position = carouselWizard.Position + 1;
                    this.wizard.PrevEnabled = true;
                }
                //Valida si ya escaneo el qr

            }
            else
            {
                if (carouselWizard.Position + 1 != this.views.Length)
                {
                    carouselWizard.Position = carouselWizard.Position + 1;
                    this.wizard.PrevEnabled = true;
                }

            }

           
           //Device.OpenUri(new Uri("https://www.ggogle.com"));

        }
        private void prevPage(object sender, EventArgs e)
        {
            if (carouselWizard.Position != 0)
            {
                carouselWizard.Position = carouselWizard.Position - 1;

                if(carouselWizard.Position == 0)
                {

                    this.wizard.PrevEnabled = false;

                }
            }
            
        }

    }
}