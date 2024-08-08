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
    public class TitleController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/Title
        public ActionResult Title_Index()
        {
            var titles = db.Titles.Select(t => new TitleVM
            {
                TitleId = t.title_id,
                TitleName = t.title_name,
                TitleIconFileName = t.title_icon,
                TitleType = t.title_type
            }).ToList();

            return View(titles);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTitle(TitleVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.TitleName = formData.TitleName?.Trim();
                formData.TitleType = formData.TitleType;

                var title = new Title
                {
                    title_name = formData.TitleName,
                    title_type = formData.TitleType
                };

                // Save title image if provided
                if (formData.TitleIcon != null && formData.TitleIcon.ContentLength > 0)
                {
                    try
                    {
                        var imageFileName = Path.GetFileName(formData.TitleIcon.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Title/"), imageFileName);

                        // Save the uploaded image
                        formData.TitleIcon.SaveAs(imagePath);
                        title.title_icon = imageFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Không thể lưu hình ảnh: " + ex.Message);
                        return View(formData);
                    }
                }

                db.Titles.Add(title);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Tiêu đề đã được thêm thành công!";
                return RedirectToAction("Title_Index");
            }

            return View(formData);
        }

        [HttpGet]
        public ActionResult EditTitle(int id)
        {
            var title = db.Titles.Find(id);
            if (title != null)
            {
                var titleVM = new TitleVM
                {
                    TitleId = title.title_id,
                    TitleName = title.title_name,
                    TitleIconFileName = title.title_icon,
                    TitleType = title.title_type
                };
                return Json(titleVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTitle(TitleVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.TitleName = formData.TitleName?.Trim();
                formData.TitleType = formData.TitleType;

                var title = db.Titles.Find(formData.TitleId);
                if (title != null)
                {
                    title.title_name = formData.TitleName;
                    title.title_type = formData.TitleType;

                    // Update title image if a new one is uploaded
                    if (formData.TitleIcon != null && formData.TitleIcon.ContentLength > 0)
                    {
                        try
                        {
                            var imageFileName = Path.GetFileName(formData.TitleIcon.FileName);
                            var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Title/"), imageFileName);

                            // Save the uploaded image
                            formData.TitleIcon.SaveAs(imagePath);
                            title.title_icon = imageFileName;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Không thể lưu hình ảnh: " + ex.Message);
                            return View(formData);
                        }
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Tiêu đề đã được cập nhật thành công!";
                    return RedirectToAction("Title_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy Tiêu đề để chỉnh sửa.");
                }
            }

            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteTitle(int titleId)
        {
            var title = db.Titles.Find(titleId);
            if (title != null)
            {
                try
                {
                    db.Titles.Remove(title);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Tiêu đề đã được xóa thành công!";
                    return RedirectToAction("Title_Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Không thể xóa Tiêu đề: " + ex.Message);
                    return RedirectToAction("Title_Index");
                }
            }

            return HttpNotFound();
        }
    }
}