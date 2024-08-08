using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteMusic.Areas.Admin_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.Admin_Website.Controllers
{
    public class BannerController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/Banner
        public ActionResult Banner_Index()
        {
            var banners = db.Banners.Select(t => new BannerVM
            {
                BannerId = t.banner_id,
                BannerImageFileName = t.banner_image,
                BannerDetaill = t.banner_detail
            }).ToList();

            return View(banners);
        }

        [HttpPost]
        public ActionResult AddBanner(BannerVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.BannerDetaill = formData.BannerDetaill?.Trim();

                // Ensure an image is provided
                if (formData.BannerImage == null || formData.BannerImage.ContentLength <= 0)
                {
                    ModelState.AddModelError("", "Hình ảnh không được để trống.");
                    return View(formData);
                }

                var banner = new Banner
                {
                    banner_detail = formData.BannerDetaill
                };

                // Save banner image
                var imageFileName = Path.GetFileName(formData.BannerImage.FileName);
                var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Banner/"), imageFileName);

                // Save the uploaded image
                formData.BannerImage.SaveAs(imagePath);
                banner.banner_image = imageFileName;

                db.Banners.Add(banner);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Banner đã được thêm thành công!";
                return RedirectToAction("Banner_Index");
            }

            return View(formData);
        }

        [HttpGet]
        public ActionResult EditBanner(int id)
        {
            var banner = db.Banners.Find(id);
            if (banner != null)
            {
                var bannerVM = new BannerVM
                {
                    BannerId = banner.banner_id,
                    BannerImageFileName = banner.banner_image,
                    BannerDetaill = banner.banner_detail
                };
                return Json(bannerVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditBanner(BannerVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.BannerDetaill = formData.BannerDetaill?.Trim();

                var banner = db.Banners.Find(formData.BannerId);
                if (banner != null)
                {
                    banner.banner_detail = formData.BannerDetaill;

                    // Update banner image if a new one is uploaded
                    if (formData.BannerImage != null && formData.BannerImage.ContentLength > 0)
                    {
                        var imageFileName = Path.GetFileName(formData.BannerImage.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Banner/"), imageFileName);

                        // Save the uploaded image
                        formData.BannerImage.SaveAs(imagePath);
                        banner.banner_image = imageFileName;
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Banner đã được cập nhật thành công!";
                    return RedirectToAction("Banner_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy Banner");
                }
            }

            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteBanner(int bannerId)
        {
            var banner = db.Banners.Find(bannerId);
            if (banner != null)
            {
                db.Banners.Remove(banner);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Banner đã được xóa thành công!";
                return RedirectToAction("Banner_Index");
            }

            return HttpNotFound();
        }
    }
}