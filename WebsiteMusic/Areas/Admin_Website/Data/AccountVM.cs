using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class AccountVM
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        [Display(Name = "Tên tài khoản")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string AccountEmail { get; set; }

        [Display(Name = "Mật khẩu")]
        public string AccountPassword { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        [Display(Name = "Vai trò")]
        public string AccountRole { get; set; }

        [Display(Name = "Hình ảnh")]
        public HttpPostedFileBase AccountImage { get; set; }
        public string AccountImageFileName { get; set; }
        public string AccountLikes { get; set; }
        public string AccountListmusic { get; set; }
    }
}