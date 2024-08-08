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
    public class NationController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/Nation
        public ActionResult Nation_Index()
        {
            var nations = db.Nations.Select(c => new NationVM
            {
                NationId = c.nation_id,
                NationName = c.nation_name,
                NationImageFileName = c.nation_image,
            }).ToList();

            return View(nations);
        }

        [HttpPost]
        public ActionResult AddNation(NationVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.NationName = formData.NationName?.Trim();

                if (string.IsNullOrEmpty(formData.NationName))
                {
                    ModelState.AddModelError("", "Tên quốc gia không được để trống.");
                    return View(formData);
                }

                var nation = new Nation
                {
                    nation_name = formData.NationName,
                };

                // Save Nation image if provided
                if (formData.NationImage != null && formData.NationImage.ContentLength > 0)
                {
                    var imageFileName = Path.GetFileName(formData.NationImage.FileName);
                    var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Nation/"), imageFileName);

                    try
                    {
                        formData.NationImage.SaveAs(imagePath);
                        nation.nation_image = imageFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi khi lưu hình ảnh: {ex.Message}");
                        return View(formData);
                    }
                }

                db.Nations.Add(nation);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Quốc gia đã được thêm thành công!";
                return RedirectToAction("Nation_Index");
            }

            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin nhập vào.");
            return View(formData);
        }

        [HttpGet]
        public ActionResult EditNation(int id)
        {
            var nation = db.Nations.Find(id);
            if (nation != null)
            {
                var nationVM = new NationVM
                {
                    NationId = nation.nation_id,
                    NationName = nation.nation_name,
                    NationImageFileName = nation.nation_image,
                };
                return Json(nationVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound("Không tìm thấy quốc gia.");
        }

        [HttpPost]
        public ActionResult EditNation(NationVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.NationName = formData.NationName?.Trim();

                if (string.IsNullOrEmpty(formData.NationName))
                {
                    ModelState.AddModelError("", "Tên quốc gia không được để trống.");
                    return View(formData);
                }

                var nation = db.Nations.Find(formData.NationId);
                if (nation != null)
                {
                    nation.nation_name = formData.NationName;

                    // Update Nation image if a new one is uploaded
                    if (formData.NationImage != null && formData.NationImage.ContentLength > 0)
                    {
                        var imageFileName = Path.GetFileName(formData.NationImage.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Nation/"), imageFileName);

                        try
                        {
                            formData.NationImage.SaveAs(imagePath);
                            nation.nation_image = imageFileName;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Lỗi khi lưu hình ảnh: {ex.Message}");
                            return View(formData);
                        }
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Quốc gia đã được cập nhật thành công!";
                    return RedirectToAction("Nation_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy quốc gia.");
                }
            }

            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin nhập vào.");
            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteNation(int nationId)
        {
            var nation = db.Nations.Find(nationId);
            if (nation != null)
            {
                // Cập nhật nation_id của tất cả các bài hát liên quan thành null
                var musics = db.Musics.Where(m => m.nation_id == nationId).ToList();
                foreach (var music in musics)
                {
                    music.nation_id = null;
                }

                db.SaveChanges(); // Lưu thay đổi để cập nhật giá trị nation_id của các bài hát

                // Xóa quốc gia
                db.Nations.Remove(nation);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Quốc gia đã được xóa thành công!";
                return RedirectToAction("Nation_Index");
            }

            return HttpNotFound("Không tìm thấy quốc gia.");
        }

    }
}