using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
    [Serializable]
  public  class UserSession
    {
        [JsonProperty(PropertyName = "success")]
        public string success { get; set; }
        [JsonProperty(PropertyName = "session_id")]
        public string session_id { get; set; }
        
    }
}
