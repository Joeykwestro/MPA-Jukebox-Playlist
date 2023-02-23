using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models;
using System.Data;
using System.Collections.Generic;

namespace MPA_Jukebox_Playlist.Controllers
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            var returnval = JsonConvert.DeserializeObject<T>(value);

            return returnval;
        }
    }
    public class HomeController : Controller
    {
        public bool login = false;
        List<string> queuelist = new List<string>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {           
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Index");
        }

        [ResponseCache(Duration = 1000, Location = ResponseCacheLocation.None, NoStore = false)]


        public IActionResult Login()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Login");
        }



        public IActionResult Create()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Create");
        }

        [Route("PlaylistSongs/{id}")]
        public IActionResult PlaylistSongs(int id)
        {

            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            ViewBag.PlaylistID = id;

            return View("PlaylistSongs");

        }


        public IActionResult AddPlaylist()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("AddPlaylist");
        }


        [Route("DeletePlaylist/{id}")]
        public IActionResult DeletePlaylist(int id)
        {

            string stringquery = $@"delete from [Playlists] where ID = {id}";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "DELETE");

            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Playlists");
        }

        public IActionResult Genre()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Genre");
        }


        public IActionResult Playlists()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Playlists");
        }


        [Route("Songs/{id}")]
        public IActionResult Songs(int id)
        {
            ViewBag.GenreType = id;

            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("Songs");
        }


        public IActionResult Queue()
        {

            var queuelist = HttpContext.Session.GetObjectFromJson<Song>("queue");

            ViewBag.Queue = new List<string>();

            if (queuelist != null)
            {
                //foreach(var item in queuelist)
                //{
                //    ViewBag.Queue.Add(item);
                //}

            }




            return View("Queue");
        }



        [Route("AddtoQueue/{id}")]
        public IActionResult AddtoQueue(int id)
        {
            Song songslist = new Song();
            DataTable dt  = MPA_Jukebox_Playlist.Models.SqlFunctions.executeSqlGetDataTable($@"select * from Songs where ID = {id}", "MPA_Jukebox_Playlist");

            foreach(DataRow dr in dt.Rows)
            {
                
                songslist = (new Song { ID = (int)dr["ID"], Title = (string)dr["Title"], Artist = (string)dr["Artist"], Duration = (int)dr["Duration"] });
                HttpContext.Session.SetObjectAsJson( , songslist);
            }

            HttpContext.Session.SetObjectAsJson("queue", songslist);

            return View("Index");
        }





        [Route("songsDetails/{id}")]
        public IActionResult songsDetails(int id)
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            ViewBag.detailsID = id;

            return View("songDetails");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("DeleteSongPlaylist/{id}")]
        public IActionResult DeleteSongPlaylist(int id)
        {
            string stringqry = $@"select PlaylistID from Saved_Songs where id = {id}";
            int id2 = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "SELECT"));


            string stringquery = $@"delete from [Saved_Songs] where ID = {id}";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "DELETE");

            ViewBag.PlaylistID = id2;

            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            return View("PlaylistSongs");
        }


        [HttpPost]
        public ActionResult form1(string txtUsername, string txtPassword)
        {

            string stringquery = $@"select Count(*) from Users where Username = '{txtUsername}' and Password = '{txtPassword}'";

            int amount = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

            if (amount > 0)
            {
                ViewBag.User = txtUsername;
                ViewBag.Password = txtPassword;

                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(txtUsername));
                login = true;
                return View("Index", true);

            }
            else
            {
                return View(false);
            }

            
        }


        public ActionResult form3(string txtAddPlaylist)
        {
            var user = "";
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                user = (string)JsonConvert.DeserializeObject(sessionUser);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            if (txtAddPlaylist != null)
            {
            string stringquery = $@"select [ID] from [Users] where [Username] = '{user}'";
            int userid = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

            string stringqry = $@"insert into [Playlists] ([UserID], [Title]) values ({userid}, '{txtAddPlaylist}')";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "INSERT");

            return View("Playlists");
            }
            else
            {
                return View("AddPlaylist", false);
            }

        }


        [Route("GoToGenre/{id}")]
        public IActionResult GoToGenre(int id)
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            ViewBag.GenreType = id;


            return View("Songs");

        }
        


        public ActionResult form2(string txtUsername, string txtPassword, string txtpassword2)
        {

            if (txtPassword == txtpassword2)
            {
                string stringQry = $@"select count(*) from Users where Username = '{txtUsername}'";
                int amount = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringQry, "MPA_Jukebox_Playlist", "SELECT"));

                if (amount == 0)
                {
                    string stringquery = $@"insert into Users (Username, Password) values ('{txtUsername}', '{txtPassword}')";
                    MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "INSERT");
                }
                else
                {
                    return View("Create", false);
                }
                
            }
            else
            {
                return View("Create", false);
            }

            return View("Login");
        }

    }
}