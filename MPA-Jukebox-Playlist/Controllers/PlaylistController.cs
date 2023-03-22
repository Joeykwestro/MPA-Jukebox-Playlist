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
    public class PlaylistController : Controller
    {
        public List<Song> queue = new List<Song>();
        private readonly ILogger<HomeController> _logger;
        private MpaContext _context;


        public PlaylistController(ILogger<PlaylistController> logger, MpaContext context)
        {
            //_logger = logger;
            _context = context;

        }
        public string getUser()
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                return user.ToString();
            }
            else
            {
                return "";
            }
        }

        [Route("AddtoPlaylist/{id}")]
        public IActionResult AddtoPlaylist(int id)
        {
            var usr = "";
            ViewBag.AddToPlaylistID = id;
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                usr = User.ToString();
                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
                return View("../Home/Login");
            }

            string stringqry = $@"select [ID] from Users where [Username] = '{usr}'";
            int userid = Int32.Parse(SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "SELECT"));

            //string stringquery = $@"Select [Title] from [Playlists] where [UserID] = {userid}";
            //DataTable dt = MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models.SqlFunctions.executeSqlGetDataTable(stringquery, "MPA_Jukebox_Playlist");

            List<Playlists> playlists = _context.Playlists.Where(e => e.UserID == userid).ToList();

            ViewBag.playlists = playlists;
            return View("../Home/SelectPlaylist");
        }



        [Route("songsDetails/{id}")]
        public IActionResult songsDetails(int id)
        {
            ViewBag.user = getUser();

            ViewBag.detailsID = id;

            string stringquery = $@"select * from Songs inner join Genres on Songs.GenreID = Genres.ID where Songs.ID = {id}";
            DataTable dt = SqlFunctions.executeSqlGetDataTable(stringquery, "MPA_Jukebox_Playlist");

            ViewBag.details = dt;

            return View("../Home/songDetails");
        }

        public IActionResult AddPlaylist()
        {
            ViewBag.user = getUser();


            return View("../Home/AddPlaylist");
        }




        [Route("DeleteSongPlaylist/{id}")]
        public IActionResult DeleteSongPlaylist(int id)
        {
            string stringqry = $@"select PlaylistID from Saved_Songs where id = {id}";
            int id2 = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "SELECT"));


            string stringquery = $@"delete from [Saved_Songs] where ID = {id}";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "DELETE");

            ViewBag.PlaylistID = id2;

            ViewBag.user = getUser();


            return View("../Home/PlaylistSongs");
        }

        [Route("PlaylistSongs/{id}")]
        public IActionResult PlaylistSongs(int id)
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

            string stringquery = $@"select [Saved_Songs].[ID], [PlaylistID], [Songs].[Title], [Songs].[Artist], [Songs].[Duration], [Genres].[Type] from [Playlists] 
inner join [Saved_Songs] on [Playlists].[ID] = [Saved_Songs].[PlaylistID] 
inner join [Songs] on [Saved_Songs].[SongID] = [Songs].[ID]
inner join [Genres] on [Songs].[GenreID] = [Genres].[ID]
where [Playlists].[ID] = {id}";
            DataTable dt = SqlFunctions.executeSqlGetDataTable(stringquery, "MPA_Jukebox_Playlist");

            ViewBag.PlaylistSongs = dt;

            return View("../Home/PlaylistSongs");

        }

        [Route("DeletePlaylist/{id}")]
        public IActionResult DeletePlaylist(int id)
        {

            string stringquery = $@"delete from [Playlists] where ID = {id}";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "DELETE");

            ViewBag.user = getUser();


            return View("../Home/Index");
        }


        public IActionResult AddQueuetoPlaylist()
        {
            ViewBag.user = getUser();


            return View("../Home/PlaylistName");
        }

        public ActionResult form5(string txtAddPlaylist)
        {
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);
                var queuelist = HttpContext.Session.GetString("queue");

                if (queuelist != null)
                {
                    queue = JsonConvert.DeserializeObject<List<Song>>(queuelist);
                    try
                    {
                        string stringquery = $@"select [ID] from Users where Username = '{user}'";
                        int userid = Int32.Parse(SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

                        Playlists playlist = new Playlists();
                        playlist.Title = txtAddPlaylist;
                        playlist.UserID = userid;
                        _context.Playlists.Add(playlist);
                        _context.SaveChanges();

                        string stringqry = $@"use MPA_Jukebox_Playlist; select [ID] from [Playlists] where [Title] = '{txtAddPlaylist}'";
                        int playlistid = Int32.Parse(SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "SELECT"));

                        foreach (var song in queue)
                        {

                            Saved_Songs SavedSongs = new Saved_Songs();
                            SavedSongs.SongID = song.ID;
                            SavedSongs.PlaylistID = playlistid;

                            _context.Saved_Songs.Add(SavedSongs);

                        }
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return View("../Home/Index");

                        throw;
                    }

                }
            }
            else
            {
                return View("../Home/Login");

            }

            return View("../Home/Index");
        }


        public ActionResult form4(string txtTitle, int txtSongID)
        {

            string stringquery = $@"select ID from Playlists where Title = '{txtTitle}'";
            int id = Int32.Parse(SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

            Saved_Songs Songs = new Saved_Songs();
            Songs.SongID = txtSongID;
            Songs.PlaylistID = id;
            _context.Saved_Songs.Add(Songs);
            _context.SaveChanges();
            return View("../Home/Index");
        }

        public ActionResult form3(string txtAddPlaylist)
        {
            var user = getUser();

            ViewBag.user = user;
            
            if (txtAddPlaylist != null)
            {
                string stringquery = $@"select [ID] from [Users] where [Username] = '{user}'";
                int userid = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

                //string stringqry = $@"insert into [Playlists] ([UserID], [Title]) values ({userid}, '{txtAddPlaylist}')";
                //MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "INSERT");

                Playlists playlists = new Playlists();
               
                playlists.Title = txtAddPlaylist;
                playlists.UserID = userid;
                
                _context.Playlists.Add(playlists);
                _context.SaveChanges();

                return View("../Home/Index");
            }
            else
            {
                return View("../Home/AddPlaylists", false);
            }
        }
    }
}
