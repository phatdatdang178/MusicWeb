using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteMusic.Areas.Admin_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.Admin_Website.Controllers
{
    public class MusicController : Controller
    {
        private ModelMusic db = new ModelMusic();

        // GET: Admin_Website/Music
        public ActionResult Music_Index()
        {
            var musicList = db.Musics.Select(m => new MusicVM
            {
                MusicId = m.music_id,
                MusicName = m.music_name,
                MusicSinger = m.music_singer,
                MusicDate = m.music_date,
                MusicLikes = m.music_likes,
                MusicListen = m.music_listen,
                MusicImageFileName = m.music_image,
                MusicLink = m.music_data,
                CategoryId = m.category_id,
                CategoryName = m.Category != null ? m.Category.category_name : "",
                NationId = m.nation_id,
                NationName = m.Nation != null ? m.Nation.nation_name : ""
            }).ToList();

            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(c => c.category_name), "category_id", "category_name");
            ViewBag.NationId = new SelectList(db.Nations.OrderBy(n => n.nation_name), "nation_id", "nation_name");

            return View(musicList);
        }

        [HttpGet]
        public ActionResult AddMusic()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(c => c.category_name), "category_id", "category_name");
            ViewBag.NationId = new SelectList(db.Nations.OrderBy(n => n.nation_name), "nation_id", "nation_name");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddMusic(MusicVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.MusicName = System.Text.RegularExpressions.Regex.Replace(formData.MusicName.Trim(), @"\s+", " ");
                formData.MusicSinger = System.Text.RegularExpressions.Regex.Replace(formData.MusicSinger.Trim(), @"\s+", " ");

                if (string.IsNullOrWhiteSpace(formData.MusicName) || string.IsNullOrWhiteSpace(formData.MusicSinger))
                {
                    ModelState.AddModelError("", "Không được chứa toàn bộ khoảng trắng.");
                    return View(formData);
                }

                // Lấy link download trực tiếp từ Google Drive
                var directDownloadUrl = ConvertGoogleDriveLinkToDirectDownloadUrl(formData.MusicLink);

                // Tạo một bản ghi bài hát mới để lấy ID
                var music = new Music
                {
                    music_name = formData.MusicName,
                    music_singer = formData.MusicSinger,
                    music_date = formData.MusicDate.HasValue ? formData.MusicDate.Value.Date : (DateTime?)null,
                    music_likes = 0,
                    music_listen = 0,
                    category_id = formData.CategoryId,
                    nation_id = formData.NationId
                };

                if (formData.MusicImage != null && formData.MusicImage.ContentLength > 0)
                {
                    var imageFileName = Path.GetFileName(formData.MusicImage.FileName);
                    var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Music/"), imageFileName);
                    formData.MusicImage.SaveAs(imagePath);
                    music.music_image = imageFileName;
                }
                else
                {
                    music.music_image = "default_music.png";
                }

                db.Musics.Add(music);
                await db.SaveChangesAsync();

                // Lấy ID của bài hát vừa thêm
                var musicId = music.music_id;

                // Tải file từ Google Drive và đổi tên theo ID
                var musicFilePath = await DownloadFileFromGoogleDrive(directDownloadUrl, musicId);

                // Cập nhật thông tin file trong cơ sở dữ liệu
                music.music_data = musicFilePath;
                db.Entry(music).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Bài hát đã được thêm thành công!";
                return RedirectToAction("Music_Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(c => c.category_name), "category_id", "category_name");
            ViewBag.NationId = new SelectList(db.Nations.OrderBy(n => n.nation_name), "nation_id", "nation_name");
            return View(formData);
        }

        private async Task<string> DownloadFileFromGoogleDrive(string fileUrl, int musicId)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return null;

            var httpClient = new HttpClient();
            var mp3FileName = $"{musicId}.mp3";
            var filePath = Path.Combine(Server.MapPath("~/Musics/"), mp3FileName);

            try
            {
                var response = await httpClient.GetAsync(fileUrl);
                response.EnsureSuccessStatusCode();

                // Đảm bảo thư mục tồn tại
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }

                return mp3FileName;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu tải file không thành công
                System.Diagnostics.Debug.WriteLine($"Lỗi: {ex.Message}");
                throw new Exception("Không thể tải file từ Google Drive.", ex);
            }
        }

        [HttpGet]
        public ActionResult EditMusic(int id)
        {
            var music = db.Musics.Find(id);
            if (music != null)
            {
                var musicVM = new MusicVM
                {
                    MusicId = music.music_id,
                    MusicName = music.music_name,
                    MusicSinger = music.music_singer,
                    MusicDate = music.music_date,
                    MusicImageFileName = music.music_image,
                    MusicLink = music.music_data,
                    CategoryId = music.category_id,
                    NationId = music.nation_id,
                };
                return Json(musicVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditMusic(MusicVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.MusicName = System.Text.RegularExpressions.Regex.Replace(formData.MusicName.Trim(), @"\s+", " ");
                formData.MusicSinger = System.Text.RegularExpressions.Regex.Replace(formData.MusicSinger.Trim(), @"\s+", " ");

                if (string.IsNullOrWhiteSpace(formData.MusicName) || string.IsNullOrWhiteSpace(formData.MusicSinger))
                {
                    ModelState.AddModelError("", "Không được chứa toàn bộ khoảng trắng.");
                    return View(formData);
                }

                var music = db.Musics.Find(formData.MusicId);
                if (music != null)
                {
                    music.music_name = formData.MusicName;
                    music.music_singer = formData.MusicSinger;
                    music.music_date = formData.MusicDate.HasValue ? formData.MusicDate.Value.Date : (DateTime?)null;
                    music.category_id = formData.CategoryId;
                    music.nation_id = formData.NationId;
                    music.music_data = ConvertGoogleDriveLinkToDirectDownloadUrl(formData.MusicLink);

                    if (formData.MusicImage != null && formData.MusicImage.ContentLength > 0)
                    {
                        var imageFileName = Path.GetFileName(formData.MusicImage.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Music/"), imageFileName);
                        formData.MusicImage.SaveAs(imagePath);
                        music.music_image = imageFileName;
                    }

                    db.Entry(music).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Bài hát đã được cập nhật thành công!";
                    return RedirectToAction("Music_Index");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(c => c.category_name), "category_id", "category_name");
            ViewBag.NationId = new SelectList(db.Nations.OrderBy(n => n.nation_name), "nation_id", "nation_name");
            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteMusic(int musicId)
        {
            var music = db.Musics.Find(musicId);
            if (music != null)
            {
                db.Musics.Remove(music);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Bài hát đã được xóa thành công!";
                return RedirectToAction("Music_Index");
            }

            return HttpNotFound();
        }

        private string ConvertGoogleDriveLinkToDirectDownloadUrl(string link)
        {
            if (string.IsNullOrEmpty(link))
                return link;

            var parts = link.Split(new[] { "/d/", "/view" }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                var fileId = parts[1];
                return $"https://drive.google.com/uc?export=download&id={fileId}";
            }
            return link;
        }
    }
}
