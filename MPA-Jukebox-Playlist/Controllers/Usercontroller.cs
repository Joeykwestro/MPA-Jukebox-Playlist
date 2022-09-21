using Microsoft.AspNetCore.Mvc;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class Usercontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
