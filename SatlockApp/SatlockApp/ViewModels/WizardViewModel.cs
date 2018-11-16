namespace SatlockApp.ViewModels
{

    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WizardViewModel : BaseViewModel
    {
        
        public WizardViewModel()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.QrWizard = new QrWizardViewModel();

        }
    }
}
