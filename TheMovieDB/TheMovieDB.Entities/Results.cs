using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Entities
{
   public class Result
    {
        [JsonProperty(PropertyName = "results")]
        public List<Movie> Results { get; set; }

    }
}
