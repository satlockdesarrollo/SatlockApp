namespace SatlockApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class LoginRequest
    {
        [JsonProperty(PropertyName = "user")]
        public string User
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "ip")]
        public string Ip
        {
            get;
            set;
        }
    }
}
