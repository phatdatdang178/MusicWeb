using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class HomeDisplayVM
    {
        public int TotalSongs { get; set; }
        public int TotalListens { get; set; }
        public int TotalLikes { get; set; }
        public List<string> TopSongs { get; set; }
        public List<int> TopListens { get; set; }
        public Dictionary<string, int> SongsByCategory { get; set; }

        public HomeDisplayVM()
        {
            TopSongs = new List<string>();
            TopListens = new List<int>();
            SongsByCategory = new Dictionary<string, int>();
        }
    }
}