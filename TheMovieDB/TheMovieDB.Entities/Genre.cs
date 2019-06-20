using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{

    [Serializable]
    public class Genre
    {
        [JsonProperty(PropertyName = "genres")]
        public List<GenreDetail> Genres { get; set; }
    }
}
