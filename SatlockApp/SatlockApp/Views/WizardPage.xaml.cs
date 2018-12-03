namespace SatlockApp.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Renders;
    using SatlockApp.ViewModels;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

    public partial class WizardPage : ContentPage
    {

        public WizardPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

      
    }
}