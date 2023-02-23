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



    }
}
