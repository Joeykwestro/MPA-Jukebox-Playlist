using System;
using System.Collections.Generic;

namespace MPA_Jukebox_Playlist.MPA_Jukebox_Playlist.Models
{
    public partial class Song
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
    }
}
