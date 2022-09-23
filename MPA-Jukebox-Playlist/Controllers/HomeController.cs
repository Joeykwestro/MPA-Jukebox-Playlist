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
        public int GenreID;

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
                ViewBag.user = "Login";
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
                ViewBag.user = "Login";
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
                ViewBag.user = "Login";
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
                ViewBag.user = "Login";
            }

            return View("Genre");
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpPost]
        public ActionResult form1(string txtUsername, string txtPassword)
        {
            ViewBag.Username = txtUsername;
            ViewBag.Password = txtPassword;

            Console.WriteLine(txtUsername);
            Console.WriteLine(txtPassword);



            return View("Index");
        }



        public IActionResult GoToGenre([FromBody] string name )
        {

            HttpContext.Session.SetString("Genre", JsonConvert.SerializeObject(name));


            ViewBag.GenreType = name;


            return View("Songs");

        }
        


        public ActionResult form2(string txtUsername, string txtPassword, string txtpassword2)
        {
            ViewBag.Username = txtUsername;
            ViewBag.Password = txtPassword;
            ViewBag.Password2 = txtpassword2;

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


            Console.WriteLine(txtUsername);
            Console.WriteLine(txtPassword);

            return View("Index");
        }

    }
}