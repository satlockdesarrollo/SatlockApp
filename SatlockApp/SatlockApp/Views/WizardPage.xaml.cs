namespace SatlockApp.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Renders;
    using SatlockApp.ViewModels;

    public partial class WizardPage : ContentPage
    {

        public WizardPage()
        {
            InitializeComponent();
        }

        private void OnSuccess(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("image is success");
        }

    }
}