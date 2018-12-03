namespace SatlockApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class InstallationRequest
    {
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("contenedor")]
        public string Contenedor { get; set; }

        [JsonProperty("idtamcontenedor")]
        public int Idtamcontenedor { get; set; }

        [JsonProperty("idpuertocontrol")]
        public int Idpuertocontrol { get; set; }

        [JsonProperty("motonave")]
        public string Motonave { get; set; }

        [JsonProperty("eta")]
        public string Eta { get; set; }

        [JsonProperty("f_cargue")]
        public string FCargue { get; set; }

        [JsonProperty("fecha_despacho")]
        public string FechaDespacho { get; set; }

        [JsonProperty("direccion_origen")]
        public string DireccionOrigen { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }

        [JsonProperty("observaciones")]
        public string Observaciones { get; set; }

    }
}
