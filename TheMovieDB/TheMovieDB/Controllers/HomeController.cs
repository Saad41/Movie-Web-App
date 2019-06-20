using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheMovieDB.Business;
using TheMovieDB.Models;
using System.Text;

namespace TheMovieDB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            List<MoviesListViewModel> ml = new List<MoviesListViewModel>();
            var moviesList = new Movies();

            ///test
            //var SessionID = moviesList.CreateSessionID();
            ///test



            var latestMoviesList = moviesList.GetLatestMovies();
            var LanaguesList = moviesList.GetAllLangauges();

            //            var GenreList = moviesList.GetAllGenres();
      


            foreach (var item in latestMoviesList)
            {
                foreach (var movies in item.Results)
                {
                    MoviesListViewModel mlvm = new MoviesListViewModel();
                    DateTime mdt= DateTime.ParseExact(movies.ReleaseDate,
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);


                    mlvm.MovieID = movies.ID;
                    mlvm.MovieName = movies.Title;
                    mlvm.ReleaseDate = String.Format("{0:dd/MM/yyyy}", mdt);
                    mlvm.Langauge = LanaguesList.Where(l => l.iso_639_1 == movies.OriginalLanguage).Select(l => l.EnglishName).SingleOrDefault().ToString(); 
                    ml.Add(mlvm);
                }
                
            }

            return View(ml);
        }

        [HttpGet]
        public ActionResult GetMovieDetails(string id, string source, string searchString)
        {
            MovieDetailViewModel movieDetail = new MovieDetailViewModel();

            var moviesList = new Movies();
            var moviedetail = moviesList.GetMovieDetails(id);

            foreach (var item in moviedetail)
            {
                StringBuilder genrelist = new StringBuilder();
                int genreCount = item.Genres.Count();
                int counter=0;

                movieDetail.MovieName = item.OriginalTitle;
                movieDetail.MovideDescription = item.OverView;
                movieDetail.Poster = item.PosterPath;

                foreach (var genre in item.Genres)
                {
                    counter++;
                    genrelist.Append(genre.Name);

                    if (genreCount > 0 && counter != genreCount)
                    genrelist.Append(",");
                }

                movieDetail.Genres = genrelist.ToString();
            }
            
            ViewBag.ReturnSource = source;
            ViewBag.SearchString = searchString;

            return View(movieDetail);
        }

        [HttpPost]
        public ActionResult Search(string Title)
        {

            List<MoviesListViewModel> ml = new List<MoviesListViewModel>();

            if (Title=="")
            {
                ViewBag.Message = "Put you Text to Search";
                return View(ml);
            }
                        
            var moviesList = new Movies();
            var LanaguesList = moviesList.GetAllLangauges();
            var searchedMoviesList = moviesList.SearchMovies(Title);

            foreach (var item in searchedMoviesList)
            {
                foreach (var movies in item.Results)
                {
                    MoviesListViewModel mlvm = new MoviesListViewModel();

                    if(movies.ReleaseDate == "")
                    {
                        mlvm.ReleaseDate = "N/A";

                    }
                    else
                    {
                        DateTime mdt = DateTime.ParseExact(movies.ReleaseDate,
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);
                        mlvm.ReleaseDate = String.Format("{0:dd/MM/yyyy}", mdt);
                    }

                    mlvm.MovieID = movies.ID;
                    mlvm.MovieName = movies.Title;
                    mlvm.Langauge = LanaguesList.Where(l => l.iso_639_1 == movies.OriginalLanguage).Select(l => l.EnglishName).SingleOrDefault().ToString();
                    ml.Add(mlvm);
                }

            }

            TempData["SearchString"] = Title;

            return View(ml);
        }

        [HttpGet]
        public ActionResult Search()
        {
            List<MoviesListViewModel> ml = new List<MoviesListViewModel>();
            return View(ml);
        }

        
        [HttpGet]
        public ActionResult FavoritesList()
        {
            List<MoviesListViewModel> ml = new List<MoviesListViewModel>();
            var moviesList = new Movies();
            var favoritesMovieList = moviesList.GetListOfFavoriteMovies();
            var LanaguesList = moviesList.GetAllLangauges();
            foreach (var item in favoritesMovieList.Results)
            {
                    MoviesListViewModel mlvm = new MoviesListViewModel();
                    DateTime mdt = DateTime.ParseExact(item.ReleaseDate,
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);
                    mlvm.MovieID = item.ID;
                    mlvm.MovieName = item.Title;
                    mlvm.ReleaseDate = String.Format("{0:dd/MM/yyyy}", mdt);
                    mlvm.Langauge = LanaguesList.Where(l => l.iso_639_1 == item.OriginalLanguage).Select(l => l.EnglishName).SingleOrDefault().ToString();
                    ml.Add(mlvm);
            }
            return View(ml);
        }

        [HttpGet]
        public ActionResult RemoveFavorite(string id)
        {
            var moviesList = new Movies();
            moviesList.AddRemoveMovieToFavorites(id, false);
            return RedirectToAction("FavoritesList","Home");
        }
        [HttpGet]
        public ActionResult AddFavorite(string id)
        {
            var moviesList = new Movies();
            moviesList.AddRemoveMovieToFavorites(id, true);
            return RedirectToAction("FavoritesList", "Home");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}