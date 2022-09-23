using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class QueueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddtoQueue(int ID)
        {
            string Song = MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql($@"select Title from Song where ID = {ID}", "MPA_Jukebox_Playlist", "SELECT");

            HttpContext.Session.SetString("Queue", JsonConvert.SerializeObject(Song));

             
            return View();
        }
    }
}
