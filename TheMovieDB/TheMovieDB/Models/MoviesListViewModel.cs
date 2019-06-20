using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheMovieDB.Models
{
    public class MoviesListViewModel
    {
        public string MovieID { get; set; }
        public string MovieName { get; set; }
        public String ReleaseDate { get; set; }
        public string Langauge { get; set; }

    }
}