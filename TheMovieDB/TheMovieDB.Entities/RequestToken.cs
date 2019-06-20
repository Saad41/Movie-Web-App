using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheMovieDB.Entities
{
    [Serializable]
   public class RequestToken
    {
        [JsonProperty(PropertyName = "success")]
        public string success { get; set; }
        [JsonProperty(PropertyName = "expires_at")]
        public string expires_at { get; set; }
        [JsonProperty(PropertyName = "request_token")]
        public string request_token { get; set; }


    }
}
