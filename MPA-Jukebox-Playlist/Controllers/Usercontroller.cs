using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class Usercontroller : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Usercontroller(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                return View("../Home/Index", true);

            }
            else
            {
                return View("../Home/Login", false);
            }


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
                    return View("../Home/Create", false);
                }

            }
            else
            {
                return View("../Home/Create", false);
            }

            return View("../Home/Login", true);
        }

    }
}
