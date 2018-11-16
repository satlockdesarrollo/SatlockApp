namespace SatlockApp.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Renders;

    public partial class WizardPage : ContentPage
    {
        private View[] _views;

        public WizardPage()
        {
            InitializeComponent();

            _views = new View[]
            {
                new QrWizardView(),
                new Wizard1View()
                //new SoExcitedView(),
                //new BikingCoolView()
            };

            Carousel.ItemsSource = _views;
        }
        private void OnCarouselPositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            var currentView = _views[e.NewValue];

            if (currentView is IAnimated animatedView)
            {
                animatedView.StartAnimation();
            }
        }

    }
}