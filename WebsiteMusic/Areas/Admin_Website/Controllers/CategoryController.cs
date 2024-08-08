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
    public class CategoryController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/Category
        public ActionResult Category_Index()
        {
            var categories = db.Categories.Select(c => new CategoryVM
            {
                CategoryId = c.category_id,
                CategoryName = c.category_name,
                CategoryImageFileName = c.category_image,
            }).ToList();

            return View(categories);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.CategoryName = formData.CategoryName?.Trim();

                if (string.IsNullOrEmpty(formData.CategoryName))
                {
                    ModelState.AddModelError("", "Tên thể loại không được để trống.");
                    return View(formData);
                }

                var category = new Category
                {
                    category_name = formData.CategoryName,
                };

                // Save category image if provided
                if (formData.CategoryImage != null && formData.CategoryImage.ContentLength > 0)
                {
                    var imageFileName = Path.GetFileName(formData.CategoryImage.FileName);
                    var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Category/"), imageFileName);

                    try
                    {
                        formData.CategoryImage.SaveAs(imagePath);
                        category.category_image = imageFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi khi lưu hình ảnh: {ex.Message}");
                        return View(formData);
                    }
                }

                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thể loại đã được thêm thành công!";
                return RedirectToAction("Category_Index");
            }

            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin nhập vào.");
            return View(formData);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                var categoryVM = new CategoryVM
                {
                    CategoryId = category.category_id,
                    CategoryName = category.category_name,
                    CategoryImageFileName = category.category_image,
                };
                return Json(categoryVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound("Không tìm thấy thể loại.");
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryVM formData)
        {
            if (ModelState.IsValid)
            {
                formData.CategoryName = formData.CategoryName?.Trim();

                if (string.IsNullOrEmpty(formData.CategoryName))
                {
                    ModelState.AddModelError("", "Tên thể loại không được để trống.");
                    return View(formData);
                }

                var category = db.Categories.Find(formData.CategoryId);
                if (category != null)
                {
                    category.category_name = formData.CategoryName;

                    // Update category image if a new one is uploaded
                    if (formData.CategoryImage != null && formData.CategoryImage.ContentLength > 0)
                    {
                        var imageFileName = Path.GetFileName(formData.CategoryImage.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Category/"), imageFileName);

                        try
                        {
                            formData.CategoryImage.SaveAs(imagePath);
                            category.category_image = imageFileName;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Lỗi khi lưu hình ảnh: {ex.Message}");
                            return View(formData);
                        }
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Thể loại đã được cập nhật thành công!";
                    return RedirectToAction("Category_Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy thể loại để chỉnh sửa.");
                }
            }

            ModelState.AddModelError("", "Vui lòng kiểm tra lại thông tin nhập vào.");
            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                // Cập nhật category_id của tất cả các bài hát liên quan thành null
                var musics = db.Musics.Where(m => m.category_id == categoryId).ToList();
                foreach (var music in musics)
                {
                    music.category_id = null;
                }

                db.SaveChanges(); // Lưu thay đổi để cập nhật giá trị category_id của các bài hát

                // Xóa thể loại
                db.Categories.Remove(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thể loại đã được xóa thành công!";
                return RedirectToAction("Category_Index");
            }

            return HttpNotFound("Không tìm thấy thể loại để xóa.");
        }

    }
}