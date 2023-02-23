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
    }
}
