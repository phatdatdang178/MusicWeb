using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.User_Website.Data
{
    public class UAccountVM
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        [Display(Name = "Tên tài khoản")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string AccountPassword { get; set; }

        [Display(Name = "Vai trò")]
        public string AccountRole { get; set; }

        [Display(Name = "Hình ảnh")]
        public string AccountImage { get; set; }

        public HttpPostedFileBase AccountImageFile { get; set; }

        public string AccountImageFileName { get; set; }

        public string AccountLikes { get; set; }

        public string AccountListmusic { get; set; }
    }
}