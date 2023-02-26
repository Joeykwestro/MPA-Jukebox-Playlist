using Microsoft.AspNetCore.Mvc;
using MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models;
using Newtonsoft.Json;
using System.Collections;

namespace MPA_Jukebox_Playlist.Controllers
{
    public class PlaylistController : Controller
    {
        public List<Song> queue = new List<Song>();

        [Route("AddtoPlaylist/{id}")]
        public IActionResult AddtoPlaylist(int id)
        {
            ViewBag.AddToPlaylistID = id;
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
                var user = JsonConvert.DeserializeObject(sessionUser);

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
                return View("../Home/Login");
            }


            return View("../Home/SelectPlaylist");
        }



        [Route("songsDetails/{id}")]
        public IActionResult songsDetails(int id)
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

            ViewBag.detailsID = id;

            return View("../Home/songDetails");
        }

        public IActionResult AddPlaylist()
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

            ViewBag.PlaylistID = id;

            return View("../Home/PlaylistSongs");

        }

        [Route("DeletePlaylist/{id}")]
        public IActionResult DeletePlaylist(int id)
        {

            string stringquery = $@"delete from [Playlists] where ID = {id}";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "DELETE");

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

            return View("../Home/Playlists");
        }


        public IActionResult AddQueuetoPlaylist()
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
                return View("../Home/Login");

            }

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
                        string stringquery = $@"insert into [Playlists] (UserID, Title) values ((select [ID] from Users where Username = '{user}'), '{txtAddPlaylist}')";
                        MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "INSERT");
                        foreach (var song in queue)
                        {

                            string stringqry = $@"insert into [Saved_Songs] (PlaylistID, SongID) values ((select [ID] from [Playlists] where [UserID] = (select ID from [Users] where Username = '{user}') and Title = '{txtAddPlaylist}'), {song.ID})";
                            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "INSERT");

                        }
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

            string stringquery = $@"insert into Saved_Songs (PlaylistID, SongID) values ((select ID from Playlists where Title = '{txtTitle}'), {txtSongID})";
            MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "INSERT");



            return View("../Home/Index");
        }

        public ActionResult form3(string txtAddPlaylist)
        {
            var user = "";
            var sessionUser = HttpContext.Session.GetString("User");

            if (sessionUser != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                user = (string)JsonConvert.DeserializeObject(sessionUser);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = "";
            }

            if (txtAddPlaylist != null)
            {
                string stringquery = $@"select [ID] from [Users] where [Username] = '{user}'";
                int userid = Int32.Parse(MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringquery, "MPA_Jukebox_Playlist", "SELECT"));

                string stringqry = $@"insert into [Playlists] ([UserID], [Title]) values ({userid}, '{txtAddPlaylist}')";
                MPA_Jukebox_Playlist.Models.SqlFunctions.executeSql(stringqry, "MPA_Jukebox_Playlist", "INSERT");

                return View("../Home/Playlists");
            }
            else
            {
                return View("../Home/AddPlaylist", false);
            }

        }


    }
}
