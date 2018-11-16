namespace SatlockApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class TripsRequest
    {
        [JsonProperty(PropertyName = "idviaje")]
        public int Idviaje { get; set; }

        [JsonProperty(PropertyName = "ultimalocalizacion")]
        public string Ultimalocalizacion { get; set; }

        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [JsonProperty(PropertyName = "sello")]
        public string Sello { get; set; }

        [JsonProperty(PropertyName = "contenedor")]
        public string Contenedor { get; set; }

        [JsonProperty(PropertyName = "subgrupo")]
        public string Subgrupo { get; set; }

        [JsonProperty(PropertyName = "tipo")]
        public string Tipo { get; set; }

        [JsonProperty(PropertyName = "fecha_inicial")]
        public string Fecha_inicial { get; set; }

        [JsonProperty(PropertyName = "hora_inicial")]
        public string Hora_inicial { get; set; }

        [JsonProperty(PropertyName = "hora_final")]
        public string Hora_final { get; set; }

        [JsonProperty(PropertyName = "floc")]
        public string Floc { get; set; }

        [JsonProperty(PropertyName = "uloc")]
        public string Uloc { get; set; }

        [JsonProperty(PropertyName = "estado")]
        public string Estado { get; set; }

        [JsonProperty(PropertyName = "motonave")]
        public string Motonave { get; set; }

        [JsonProperty(PropertyName = "destino")]
        public string Destino { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public string Longitud { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public string Latitud { get; set; }

        [JsonProperty(PropertyName = "bateria")]
        public string Bateria { get; set; }

        [JsonProperty(PropertyName = "tamcontenedor")]
        public string Tamcontenedor { get; set; }

        [JsonProperty(PropertyName = "diasmora")]
        public string Diasmora { get; set; }

        [JsonProperty(PropertyName = "patio")]
        public string Patio { get; set; }

        [JsonProperty(PropertyName = "lineanaviera")]
        public string Lineanaviera { get; set; }

        [JsonProperty(PropertyName = "motonaven")]
        public string Motonaven { get; set; }


    }
}
