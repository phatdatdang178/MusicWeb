using System.Linq;
using System.Web.Mvc;
using WebsiteMusic.Models;
using WebsiteMusic.Areas.User_Website.Data;
using System.Collections.Generic;
using System.Drawing.Design;

namespace WebsiteMusic.Areas.User_Website.Controllers
{
    public class KhamphaController : Controller
    {
        private ModelMusic db = new ModelMusic();

        // GET: User_Website/Khampha
        public ActionResult Khampha()
        {
            // Lấy danh sách bài hát mới phát hành
            var latestReleases = db.Musics
                .OrderByDescending(m => m.music_date)
                .Take(9)
                .Select(m => new UMusicVM
                {
                    MusicId = m.music_id,
                    MusicName = m.music_name,
                    MusicSinger = m.music_singer,
                    MusicDate = m.music_date,
                    MusicImageFileName = m.music_image
                })
                .ToList();

            // Lấy danh sách thể loại
            var categories = db.Categories
                .Select(c => new UCategoryVM
                {
                    CategoryId = c.category_id,
                    CategoryName = c.category_name,
                    CategoryImageFileName = c.category_image
                })
                .ToList();

            // Lấy danh sách các quốc gia
            var nations = db.Nations
                .Select(n => new UNationVM
                {
                    NationId = n.nation_id,
                    NationName = n.nation_name,
                    NationImageFileName = n.nation_image
                })
                .ToList();

            // Lấy các bài hát theo thể loại
            var categoryMusic = new Dictionary<int, List<UMusicVM>>();
            foreach (var category in categories)
            {
                var musicByCategory = db.Musics
                    .Where(m => m.category_id == category.CategoryId)
                    .Take(4)
                    .Select(m => new UMusicVM
                    {
                        MusicId = m.music_id,
                        MusicName = m.music_name,
                        MusicSinger = m.music_singer,
                        MusicDate = m.music_date,
                        MusicImageFileName = m.music_image,
                        CategoryId = m.category_id
                    })
                    .ToList();

                categoryMusic[category.CategoryId] = musicByCategory;
            }

            var topMusic = db.Musics
        .OrderByDescending(m => m.music_likes)
        .Take(9)
        .Select(m => new UMusicVM
        {
            MusicId = m.music_id,
            MusicName = m.music_name,
            MusicSinger = m.music_singer,
            MusicDate = m.music_date,
            MusicImageFileName = m.music_image
        })
        .ToList();

            ViewBag.LatestReleases = latestReleases;
            ViewBag.Categories = categories;
            ViewBag.Nations = nations;
            ViewBag.CategoryMusic = categoryMusic;
            ViewBag.TopMusic = topMusic;


            // Lấy các bài hát theo lượt nghe (BXH)
            var topMusicByListen = db.Musics
                .OrderByDescending(m => m.music_listen)
                .Take(9)
                .Select(m => new UMusicVM
                {
                    MusicId = m.music_id,
                    MusicName = m.music_name,
                    MusicSinger = m.music_singer,
                    MusicDate = m.music_date,
                    MusicImageFileName = m.music_image
                })
                .ToList();

            ViewBag.LatestReleases = latestReleases;
            ViewBag.Categories = categories;
            ViewBag.CategoryMusic = categoryMusic;
            ViewBag.TopMusicByListen = topMusicByListen;

            return View();
        }
    }
}
