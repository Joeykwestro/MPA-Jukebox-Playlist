using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MPA_Jukebox_Playlist.Controllers
{

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


            return View("Songs");
        }


        public IActionResult Queue()
        {

            queuelist.Add(HttpContext.Session.GetString("Queue"));

            if (queuelist != null)
            {
                ViewBag.Queue = (JsonConvert.DeserializeObject(queuelist.ToString()));

            }


            return View("Queue");
        }


        [Route("AddtoQueue/{id}")]
        public IActionResult AddtoQueue(int ID)
        {
            string Song = MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql($@"select Title from Songs where ID = {ID}", "MPA_Jukebox_Playlist", "SELECT");

            HttpContext.Session.SetString("Queue", JsonConvert.SerializeObject(Song));

            return View("Index");
        }


        [Route("songsDetails/{id}")]
        public IActionResult songsDetails(int id)
        {
            ViewBag.detailsID = id;

            return View("songDetails");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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


        [Route("GoToGenre/{id}")]
        public IActionResult GoToGenre(int id)
        { 

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