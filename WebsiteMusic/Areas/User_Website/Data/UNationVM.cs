using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.User_Website.Data
{
    public class UNationVM
    {
        [DisplayName("Mã quốc gia")]
        public int NationId { get; set; }

        [DisplayName("Tên quốc gia")]
        [Required(ErrorMessage = "Tên quốc gia là bắt buộc.")]
        public string NationName { get; set; }

        [DisplayName("Ảnh quốc gia")]
        public HttpPostedFileBase NationImage { get; set; }

        public string NationImageFileName { get; set; }
    }
}