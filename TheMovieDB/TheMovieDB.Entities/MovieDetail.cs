using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
    [Serializable]
    public class MovieDetail
    {
        [JsonProperty(PropertyName = "original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string OverView { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<GenreDetail> Genres { get; set; }
    }
}
