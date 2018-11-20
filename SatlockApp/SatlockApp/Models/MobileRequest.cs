namespace SatlockApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class MobileRequest
    {
        [JsonProperty("latitudUnidad")]
        public string LatitudUnidad { get; set; }

        [JsonProperty("longitudUnidad")]
        public string LongitudUnidad { get; set; }

        [JsonProperty("listaEventos")]
        public MobileEventsRequest[] ListaEventos { get; set; }

    }
}
