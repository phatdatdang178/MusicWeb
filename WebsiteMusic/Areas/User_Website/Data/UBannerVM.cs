using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.User_Website.Data
{
    public class UBannerVM
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