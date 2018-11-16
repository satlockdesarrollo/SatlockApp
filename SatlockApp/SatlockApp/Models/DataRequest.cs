namespace SatlockApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class DataRequest<T>
    {
        [JsonProperty(PropertyName = "data")]
        public T Data
        {
            get;
            set;
        }

        public DataRequest(T data)
        {

            this.Data = data;

        }

    }
}
