using System;
using System.Linq;
using System.Web.Mvc;
using WebsiteMusic.Areas.User_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.User_Website.Controllers
{
    public class BXHController : Controller
    {
        private ModelMusic db = new ModelMusic();

        // GET: User_Website/BXH
        public ActionResult BXH()
        {
            // Lấy các bài hát theo lượt nghe (BXH)
            var topMusicByListen = db.Musics
                .OrderByDescending(m => m.music_listen) // Sắp xếp theo lượt nghe
                .Select(m => new UMusicVM
                {
                    MusicId = m.music_id,
                    MusicName = m.music_name,
                    MusicSinger = m.music_singer,
                    MusicDate = m.music_date,
                    MusicImageFileName = m.music_image
                })
                .ToList();

            ViewBag.TopMusicByListen = topMusicByListen;
            return View();
        }
    }
}
