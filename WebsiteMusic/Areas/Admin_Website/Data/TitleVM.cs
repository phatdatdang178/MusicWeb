using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class TitleVM
    {
        [DisplayName("Mã tiêu đề")]
        public int TitleId { get; set; }

        [DisplayName("Tên tiêu đề")]
        public string TitleName { get; set; }

        [DisplayName("Icon tiêu đề")]
        public HttpPostedFileBase TitleIcon { get; set; }
        public string TitleIconFileName { get; set; }

        [DisplayName("Loại tiêu đề")]
        public int? TitleType { get; set; }
    }
}