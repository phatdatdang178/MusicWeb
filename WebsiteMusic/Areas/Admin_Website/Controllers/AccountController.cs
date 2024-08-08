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
    public class AccountController : Controller
    {
        private ModelMusic db = new ModelMusic();
        // GET: Admin_Website/Account
        public ActionResult Account_Index()
        {
            var accounts = db.Accounts.Select(a => new AccountVM
            {
                AccountId = a.account_id,
                AccountName = a.account_name,
                AccountEmail = a.account_email,
                AccountRole = a.account_role,
                AccountImageFileName = a.account_image
            }).ToList();

            return View(accounts);
        }

        [HttpPost]
        public ActionResult AddAccount(AccountVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.AccountName = System.Text.RegularExpressions.Regex.Replace(formData.AccountName.Trim(), @"\s+", " ");
                formData.AccountEmail = formData.AccountEmail.Trim();
                formData.AccountRole = formData.AccountRole.Trim();

                // Check if any field contains only spaces
                if (string.IsNullOrWhiteSpace(formData.AccountName) || string.IsNullOrWhiteSpace(formData.AccountEmail) || string.IsNullOrWhiteSpace(formData.AccountRole))
                {
                    ModelState.AddModelError("", "Không được chứa toàn bộ khoảng trắng.");
                    return View(formData);
                }

                var account = new Account
                {
                    account_name = formData.AccountName,
                    account_email = formData.AccountEmail,
                    account_password = formData.AccountPassword,
                    account_role = formData.AccountRole,
                    account_likes = string.Empty,
                    account_listmusic = string.Empty
                };

                // Save account image if provided
                if (formData.AccountImage != null && formData.AccountImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(formData.AccountImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/Images_Account"), fileName);
                    formData.AccountImage.SaveAs(path);
                    account.account_image = fileName;
                }
                else
                {
                    account.account_image = "default_account.png";
                }

                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Tài khoản đã được thêm thành công!";
                return RedirectToAction("Account_Index");
            }

            return View(formData);
        }

        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            var account = db.Accounts.Find(id);
            if (account != null)
            {
                var accountVM = new AccountVM
                {
                    AccountId = account.account_id,
                    AccountName = account.account_name,
                    AccountEmail = account.account_email,
                    AccountRole = account.account_role,
                    AccountImageFileName = account.account_image
                };
                return Json(accountVM, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditAccount(AccountVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.AccountName = System.Text.RegularExpressions.Regex.Replace(formData.AccountName.Trim(), @"\s+", " ");
                formData.AccountEmail = formData.AccountEmail.Trim();
                formData.AccountRole = formData.AccountRole.Trim();

                // Check if any field contains only spaces
                if (string.IsNullOrWhiteSpace(formData.AccountName) || string.IsNullOrWhiteSpace(formData.AccountEmail) || string.IsNullOrWhiteSpace(formData.AccountRole))
                {
                    ModelState.AddModelError("", "Không được chứa toàn bộ khoảng trắng.");
                    return View(formData);
                }

                var account = db.Accounts.Find(formData.AccountId);
                if (account != null)
                {
                    account.account_name = formData.AccountName;
                    account.account_email = formData.AccountEmail;
                    account.account_role = formData.AccountRole;

                    // Update account password if necessary
                    if (!string.IsNullOrEmpty(formData.AccountPassword))
                    {
                        account.account_password = formData.AccountPassword;
                    }

                    // Update account image if a new one is uploaded
                    if (formData.AccountImage != null && formData.AccountImage.ContentLength > 0)
                    {
                        var imageFileName = Path.GetFileName(formData.AccountImage.FileName);
                        var imagePath = Path.Combine(Server.MapPath("~/Images/Images_Account/"), imageFileName);

                        // Overwrite the existing image if it exists
                        formData.AccountImage.SaveAs(imagePath);
                        account.account_image = imageFileName;
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Tài khoản đã được cập nhật thành công!";
                    return RedirectToAction("Account_Index");
                }
            }

            return View(formData);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int accountId)
        {
            var account = db.Accounts.Find(accountId);
            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Tài khoản đã được xóa thành công!";
                return RedirectToAction("Account_Index");
            }

            return HttpNotFound();
        }
    }
}