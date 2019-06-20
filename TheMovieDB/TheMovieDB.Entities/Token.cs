using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
    [Serializable]
   public class Token
    {
        [JsonProperty(PropertyName = "request_token")]
        public string request_token { get; set; }

    }
}
