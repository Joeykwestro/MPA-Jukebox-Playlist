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
        public int ToLogin([FromBody] int ID)
        {
            string stringquery = $@"select Username from Users where ID = {ID}";
            string Username = MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT");

            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(Username));


            ViewBag.username = Username;


            return ID;
        }

        public IActionResult toCreate([FromBody] string nothing)
        {

            return View("Create");

        }
    }
}
