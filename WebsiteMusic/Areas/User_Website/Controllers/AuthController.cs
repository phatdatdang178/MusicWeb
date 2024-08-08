using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteMusic.Areas.User_Website.Data;
using WebsiteMusic.Models;

namespace WebsiteMusic.Areas.User_Website.Controllers
{
    public class AuthController : Controller
    {
        private ModelMusic db = new ModelMusic();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Accounts.FirstOrDefault(u => u.account_email == email && u.account_password == password);

            if (user != null)
            {
                // Store user details in the session
                Session["UserEmail"] = user.account_email;
                Session["UserName"] = user.account_name;
                Session["UserImage"] = user.account_image;

                if (user.account_role == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin_Website" });
                }
                else if (user.account_role == "User")
                {
                    return RedirectToAction("Khampha", "Khampha", new { area = "User_Website" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginErrorMessage = "Invalid email or password.";
                return View("Index");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UAccountVM formData)
        {
            if (ModelState.IsValid)
            {
                // Trim and remove extra spaces
                formData.AccountName = System.Text.RegularExpressions.Regex.Replace(formData.AccountName.Trim(), @"\s+", " ");
                formData.AccountEmail = formData.AccountEmail.Trim();

                // Check if any field contains only spaces
                if (string.IsNullOrWhiteSpace(formData.AccountName) || string.IsNullOrWhiteSpace(formData.AccountEmail) || string.IsNullOrWhiteSpace(formData.AccountPassword))
                {
                    ModelState.AddModelError("", "Không được chứa toàn bộ khoảng trắng.");
                    return View(formData);
                }

                var account = new Account
                {
                    account_name = formData.AccountName,
                    account_email = formData.AccountEmail,
                    account_password = formData.AccountPassword,
                    account_role = "User", // Default role as "User" if not provided
                    account_likes = string.Empty,
                    account_listmusic = string.Empty
                };

                // Save account image if provided
                if (formData.AccountImageFile != null && formData.AccountImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(formData.AccountImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/Images_Account"), fileName);
                    formData.AccountImageFile.SaveAs(path);
                    account.account_image = fileName;
                }
                else
                {
                    account.account_image = "default_account.png";
                }

                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("Index");
            }

            return View(formData);
        }



        // Đăng xuất
        public ActionResult Logout()
        {
            // Clear all session data
            Session.Clear();
            Session.Abandon(); // Optional: End the current session

            // Redirect to the login page
            return RedirectToAction("Index");
        }

    }
}