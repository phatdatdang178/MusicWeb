using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMusic.Areas.Admin_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.Admin_Website.Controllers
{
    public class HomeController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/HomeAdmin
        public ActionResult Index()
        {
            var model = new HomeDisplayVM();

            // Lấy tổng số bài hát
            model.TotalSongs = db.Musics.Count();

            // Tính tổng lượt nghe, xử lý giá trị null
            model.TotalListens = db.Musics.Sum(m => (int?)m.music_listen) ?? 0;

            // Tính tổng lượt thích, xử lý giá trị null
            model.TotalLikes = db.Musics.Sum(m => (int?)m.music_likes) ?? 0;

            // Lấy top 6 bài hát theo lượt nghe
            var topSongs = db.Musics
                .OrderByDescending(m => m.music_listen)
                .Take(6)
                .Select(m => new { m.music_name, m.music_listen })
                .ToList();

            model.TopSongs = topSongs.Select(ts => ts.music_name).ToList();
            model.TopListens = topSongs.Select(ts => ts.music_listen ?? 0).ToList();

            // Đếm số bài hát theo thể loại (category)
            model.SongsByCategory = db.Categories
                .Select(c => new
                {
                    c.category_name,
                    Count = db.Musics.Count(m => m.category_id == c.category_id)
                })
                .ToDictionary(x => x.category_name, x => x.Count);

            return View(model);
        }
    }
}