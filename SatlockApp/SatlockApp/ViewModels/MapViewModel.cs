namespace SatlockApp.ViewModels
{
    using System;
    using Newtonsoft.Json;

    public class MapViewModel : BaseViewModel
    {
        #region Propperties
        public TripItemViewModel Trip
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string MapUrl
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MapViewModel(TripItemViewModel TripItem)
        {
            this.Trip = TripItem;
            this.Title = TripItem.Sello;



            var temp1 = JsonConvert.SerializeObject(TripItem);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(temp1);
            var temp2 = System.Convert.ToBase64String(plainTextBytes);


            this.MapUrl = "https://www.ultrackonline.com/pagos/webview/mapa1.html?data%" + temp2;

        }
        #endregion
    }
}
