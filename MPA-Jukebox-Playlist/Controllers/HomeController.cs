using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models;
using System.Data;
using System.Collections.Generic;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class HomeController : Controller
    {

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
    }
}