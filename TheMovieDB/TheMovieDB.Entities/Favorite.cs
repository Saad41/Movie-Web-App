using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
    [Serializable]

    public class Favorite
    {
        [JsonProperty(PropertyName = "page")]
        public string Page { get; set; }
        [JsonProperty(PropertyName = "results")]
        public List<Movie> Results { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public string TotalPages { get; set; }
        [JsonProperty(PropertyName = "total_results")]
        public string TotalResults { get; set; }
    }
}
