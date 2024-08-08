using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class BannerVM
    {
        [DisplayName("Banner ID")]
        public int BannerId { get; set; }

        [DisplayName("Hình ảnh")]
        public HttpPostedFileBase BannerImage { get; set; }

        public string BannerImageFileName { get; set; }

        [DisplayName("Mô tả")]
        public string BannerDetaill { get; set; }
    }
}