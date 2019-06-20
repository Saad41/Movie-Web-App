using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
    [Serializable]
   public class Langauges
    {
        [JsonProperty(PropertyName = "iso_639_1")]
        public string iso_639_1 { get; set; }

        [JsonProperty(PropertyName = "english_name")]
        public string EnglishName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
