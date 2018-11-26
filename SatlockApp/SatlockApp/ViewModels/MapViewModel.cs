namespace SatlockApp.ViewModels
{
    using System;
    using Newtonsoft.Json;

    public class MapViewModel : BaseViewModel
    {
        private bool isLoading;


        #region Propperties
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                SetValue(ref this.isLoading, value);
            }
        }


        #endregion

        #region Constructors
        public MapViewModel()
        {

           


        }
        #endregion
    }
}
