using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMusic.Areas.User_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.User_Website.Controllers
{
    public class MoiphathanhController : Controller
    {
        private ModelMusic db = new ModelMusic();

        // GET: User_Website/Moiphathanh
        public ActionResult Moiphathanh()
        {
            // Lấy các bài hát theo ngày thêm gần nhất
            var newReleases = db.Musics
                .OrderByDescending(m => m.music_date) // Sắp xếp theo ngày thêm gần nhất
                .Select(m => new UMusicVM
                {
                    MusicId = m.music_id,
                    MusicName = m.music_name,
                    MusicSinger = m.music_singer,
                    MusicDate = m.music_date,
                    MusicImageFileName = m.music_image
                })
                .ToList();

            ViewBag.NewReleases = newReleases;
            return View();
        }
    }
}
