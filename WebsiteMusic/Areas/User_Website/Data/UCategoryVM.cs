using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.User_Website.Data
{
    public class UCategoryVM
    {
        [DisplayName("Mã thể loại")]
        public int CategoryId { get; set; }

        [DisplayName("Tên thể loại")]
        [Required(ErrorMessage = "Tên thể loại là bắt buộc.")]
        public string CategoryName { get; set; }

        [DisplayName("Ảnh thể loại")]
        public HttpPostedFileBase CategoryImage { get; set; }

        public string CategoryImageFileName { get; set; }
    }
}