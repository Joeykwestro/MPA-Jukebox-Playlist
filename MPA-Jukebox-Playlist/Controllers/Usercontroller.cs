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
    public class Usercontroller : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MpaContext _context;

        public Usercontroller(ILogger<HomeController> logger, MpaContext context)
        {
            _logger = logger;
            _context = context;

        }


        [HttpPost]
        public ActionResult form1(string txtUsername, string txtPassword)
        {

            //string stringquery = $@"select Count(*) from Users where Username = '{txtUsername}' and Password = '{txtPassword}'";
            //int amount = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

            int amount = _context.Users.Where(u => u.Username == txtUsername && u.Password == txtPassword).Count();

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

        [HttpPost]
        public ActionResult form2(string txtUsername, string txtPassword, string txtpassword2)
        {

            if (txtPassword == txtpassword2)
            {
                //string stringQry = $@"select count(*) from Users where Username = '{txtUsername}'";
                //int amount = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringQry, "MPA_Jukebox_Playlist", "SELECT"));

                int amount = _context.Users.Where(e => e.Username == txtUsername).Count();

                if (amount == 0)
                {
                    //string stringquery = $@"insert into Users (Username, Password) values ('{txtUsername}', '{txtPassword}')";
                    //MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "INSERT");

                    Users user = new Users();
                    user.Username = txtUsername;
                    user.Password = txtPassword;
                    _context.Users.Add(user);
                    _context.SaveChanges();

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
