using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TheMovieDB.Entities;

namespace TheMovieDB.Business
{
    public class Movies
    {
        private static string TMDBUserKey;
        private static string UserName;
        private static string Password;

        public Movies()
        {
            TMDBUserKey = ConfigurationManager.AppSettings["TMDBUserKey"].ToString();
            UserName = ConfigurationManager.AppSettings["UserName"].ToString();
            Password = ConfigurationManager.AppSettings["Password"].ToString();
        }

        public List<Result> GetLatestMovies()
        {
            string url = $"https://api.themoviedb.org/3/movie/now_playing?api_key={ TMDBUserKey }&language=en-US&page=1";

            var client = new RestClient(url);
            var response = client.Execute<List<Result>>(new RestRequest());
            return response.Data;
        }

        public List<Langauges> GetAllLangauges()
        {
            string url = $"https://api.themoviedb.org/3/configuration/languages?api_key={ TMDBUserKey }";

            var client = new RestClient(url);
            var response = client.Execute<List<Langauges>>(new RestRequest());
            return response.Data;
        }

        public List<Genre> GetAllGenres()
        {
            string url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={ TMDBUserKey }&language=en-US";

            var client = new RestClient(url);
            var response = client.Execute<List<Genre>>(new RestRequest());
            return response.Data;
        }

        public List<MovieDetail> GetMovieDetails(string movieid)
        {
            string url = $"https://api.themoviedb.org/3/movie/{ movieid }?api_key={ TMDBUserKey }&language=en-US";
            var client = new RestClient(url);
            var response = client.Execute<List<MovieDetail>>(new RestRequest());
            return response.Data;
        }

        public List<Result> SearchMovies(string title)
        {
            string url = $"https://api.themoviedb.org/3/search/movie?api_key={ TMDBUserKey }&query={ title }";

            var client = new RestClient(url);
            var response = client.Execute<List<Result>>(new RestRequest());
            return response.Data;
        }

        public Favorite GetListOfFavoriteMovies()
        {
            string sessionId = CreateSessionID();
            string url = $"https://api.themoviedb.org/3/account/{{account_id}}/favorite/movies?api_key={ TMDBUserKey }&session_id={ sessionId }&language=en-US&sort_by=created_at.asc&page=1";

            var client = new RestClient(url);
            var response = client.Execute<Favorite>(new RestRequest());
            return response.Data;
        }

        public void AddRemoveMovieToFavorites(string movieId, bool status)
        {
            string sessionId = CreateSessionID();
            var client = new RestClient($"https://api.themoviedb.org/3/account/{{account_id}}/favorite?api_key={ TMDBUserKey }&session_id={ sessionId }");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json;charset=utf-8");

            if (status)
                request.AddParameter("application/json;charset=utf-8", "{\"media_type\":\"movie\",\"media_id\":" + movieId + ",\"favorite\":true}", ParameterType.RequestBody);
            else
                request.AddParameter("application/json;charset=utf-8", "{\"media_type\":\"movie\",\"media_id\":" + movieId + ",\"favorite\":false}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
        }


        private string CreateTemporaryRequestToken()
        {
            string url = $"https://api.themoviedb.org/3/authentication/token/new?api_key={ TMDBUserKey }";
            var client = new RestClient(url);
            var response = client.Execute<RequestToken>(new RestRequest());
            return response.Data.request_token;
        }

        private string CreateRequestFinalRequestToken()
        {
            var TemporaryRequestToken = CreateTemporaryRequestToken();
            UserInfo userInfo = new UserInfo() { username = UserName  , password = Password, request_token = TemporaryRequestToken };

            var json = new JavaScriptSerializer().Serialize(userInfo);


            string url = $"https://api.themoviedb.org/3/authentication/token/validate_with_login?api_key={ TMDBUserKey }";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute<RequestToken>(request);
            return response.Data.request_token;
        }

        public string CreateSessionID()
        {
            var FinalRequestToken = CreateRequestFinalRequestToken();
            Token TokenInfo = new Token() { request_token = FinalRequestToken };
            var json = new JavaScriptSerializer().Serialize(TokenInfo);

            var client = new RestClient($"https://api.themoviedb.org/3/authentication/session/new?api_key={ TMDBUserKey }");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute<UserSession>(request);
            return response.Data.session_id;
        }
    }
}


