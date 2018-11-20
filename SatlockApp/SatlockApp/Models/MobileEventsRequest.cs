namespace SatlockApp.Models
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class MobileEventsRequest
    {

        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        [JsonProperty("rumbo")]
        public string Rumbo { get; set; }

        [JsonProperty("velocidad")]
        public string Velocidad { get; set; }

        [JsonProperty("latitud")]
        public string Latitud { get; set; }

        [JsonProperty("longitud")]
        public string Longitud { get; set; }

        [JsonProperty("localizacion")]
        public string Localizacion { get; set; }

        [JsonProperty("validez")]
        public bool Validez { get; set; }

        [JsonProperty("bateria")]
        public string Bateria { get; set; }

        [JsonProperty("eventof")]
        public string Eventof { get; set; }

        [JsonProperty("puertas")]
        public string Puertas { get; set; }

        [JsonProperty("cargador")]
        public string Cargador { get; set; }

        [JsonProperty("movimiento")]
        public string Movimiento { get; set; }

        [JsonProperty("comgps")]
        public string Comgps { get; set; }

        [JsonProperty("sguaya")]
        public string Sguaya { get; set; }

        [JsonProperty("fechaservidor")]
        public string Fechaservidor { get; set; }

        [JsonProperty("altura")]
        public string Altura { get; set; }

        [JsonProperty("kilometraje")]
        public string Kilometraje { get; set; }

        [JsonProperty("gid")]
        public string Gid { get; set; }
    }
}
