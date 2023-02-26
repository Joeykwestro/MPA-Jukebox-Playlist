using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models;
using System.Data;
using System.Collections.Generic;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models.Database;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MpaContext _context;


        public HomeController(ILogger<HomeController> logger, MpaContext context)
        {
            _logger = logger;
            _context = context;

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

        public IActionResult insertnewdata()
        {
            DataSeed();

            return View("Index");
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

        public IActionResult SelectPlaylist()
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
                return View("Login");
            }

            return View("SelectPlaylist");
        }



        public IActionResult PlaylistName()
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

            return View("PlaylistName");
        }


        public IActionResult Queue()
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
            var queuelist = HttpContext.Session.GetString("queue");

            if (queuelist != null)
            {
                var queue = JsonConvert.DeserializeObject(queuelist);

                ViewBag.Queue = queue;
            }




            return View("Queue");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        private void DataSeed()
        {
            try
            {
                _context.Genres.Add(new MPA_Jukebox_Playlist.Models.Database.Genres() { Type = "Pop" });
                _context.Genres.Add(new MPA_Jukebox_Playlist.Models.Database.Genres() { Type = "Rock" });
                _context.Genres.Add(new MPA_Jukebox_Playlist.Models.Database.Genres() { Type = "clasic" });
                _context.Genres.Add(new MPA_Jukebox_Playlist.Models.Database.Genres() { Type = "HipHop" });
                _context.Genres.Add(new MPA_Jukebox_Playlist.Models.Database.Genres() { Type = "Hardstyle" });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 1, Artist = "test1", Title = "test1", Duration = 4 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 2, Artist = "test2", Title = "test2", Duration = 2 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 3, Artist = "test3", Title = "test3", Duration = 1 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 4, Artist = "test4", Title = "test4", Duration = 3 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 5, Artist = "test5", Title = "test5", Duration = 5 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 1, Artist = "test6", Title = "test6", Duration = 6 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 2, Artist = "test7", Title = "test7", Duration = 4 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 3, Artist = "test8", Title = "test8", Duration = 2 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 4, Artist = "test9", Title = "test9", Duration = 3 });
                _context.Songs.Add(new MPA_Jukebox_Playlist.Models.Database.Songs() { GenreID = 5, Artist = "test10", Title = "test10", Duration = 7 });
                _context.Users.Add(new MPA_Jukebox_Playlist.Models.Database.Users() { Username = "Joey", Password = "Joey" });
                _context.Users.Add(new MPA_Jukebox_Playlist.Models.Database.Users() { Username = "Test", Password = "Test" });
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}