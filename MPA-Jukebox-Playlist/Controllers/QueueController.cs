using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models.Database;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class QueueController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MpaContext _context;
        public List<Songs> songslist { get; set; }

        public QueueController(ILogger<HomeController> logger, MpaContext context)
        {
            _logger = logger;
            _context = context;
            songslist = new List<Songs>();
        }

        
        [Route("AddtoQueue/{id}")]
        public IActionResult AddtoQueue(int id)
        {

            List<Songs> dbSong;
            List<Songs> oldQueue;
            List<Songs> dbQueue;

            oldQueue = null;

            var queuelist = HttpContext.Session.GetString("queue");

            if (queuelist != null)
            {
                var newSong = JsonConvert.DeserializeObject<List<Songs>>(queuelist);

                oldQueue = newSong;

            }
            if (oldQueue != null)
            {
                foreach (var song in oldQueue)
                {
                    songslist.Add(song);
                }
            }  

            dbSong = _context.Songs.Where(e => e.ID == id).ToList();

            songslist.AddRange(dbSong);

            HttpContext.Session.SetString("queue", JsonConvert.SerializeObject(songslist));


            return View("../Home/Index");
        }

        [Route("DeleteFromQueue/{id}")]
        public IActionResult DeleteFromQueue(int id)
        {

            List<Songs> queue;
            List<Songs> dbSong;
            List<Songs> dbQueue;

            queue = null;

            var queuelist = HttpContext.Session.GetString("queue");

            queue = JsonConvert.DeserializeObject<List<Songs>>(queuelist);

            foreach (var item in queue)
            {
                if (item.ID == id)
                {
                    queue.Remove(item);
                    break;

                }
            }


            HttpContext.Session.SetString("queue", JsonConvert.SerializeObject(queue));

            return View("../Home/Index");

        }

    }
}
