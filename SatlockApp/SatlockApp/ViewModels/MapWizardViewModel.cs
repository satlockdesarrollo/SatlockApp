using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

using SatlockApp.Helpers;

namespace SatlockApp.ViewModels
{
    public class MapWizardViewModel : BaseViewModel
    {

        private string mapUrl;

        public string MapUrl
        {
            get
            {
                return this.mapUrl;
            }
            set
            {
                SetValue(ref this.mapUrl, value);
            }
        }

        public MapWizardViewModel()
        {

            this.MapUrl = "https://google.com.co";
           
        }
    }
}
