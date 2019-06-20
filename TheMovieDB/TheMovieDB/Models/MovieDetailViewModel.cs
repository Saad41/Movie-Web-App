using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMovieDB.Models
{
    public class MovieDetailViewModel
    {
        public string MovieName { get; set; }
        public string Poster { get; set; }
        public string MovideDescription { get; set; }
        public string Genres { get; set; }
    }
}