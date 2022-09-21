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
        public string Username;
        public int GenreID;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            var user = JsonConvert.DeserializeObject(sessionUser);

            ViewBag.user = user;
            return View("Index");
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Genre()
        {
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

        public IActionResult toCreate([FromBody] string nothing)
        {

            return View("Create");

        }

        public IActionResult ToGenre([FromBody] int ID)
        {

            return View("Genre");

        }
        
        [HttpPost]
        public int ToLogin([FromBody] int ID)
        {
            string stringquery = $@"select Username from Users where ID = {ID + 1}";
            Username = MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT");

            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(Username));


            ViewBag.username = Username;


            return ID;
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