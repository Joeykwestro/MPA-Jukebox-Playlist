using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.Models;
using System.Diagnostics;

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
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
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

        public ActionResult form2(string txtUsername, string txtPassword, string txtpassword2)
        {
            ViewBag.Username = txtUsername;
            ViewBag.Password = txtPassword;
            ViewBag.Password2 = txtpassword2;

            if (txtPassword == txtpassword2)
            {
                string stringquery = $@"insert into Users (Username, Password) values ('{txtUsername}', '{txtPassword}')";
                MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "INSERT");
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