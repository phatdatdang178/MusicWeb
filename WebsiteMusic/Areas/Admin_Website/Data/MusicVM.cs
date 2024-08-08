using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class MusicVM
    {
        [DisplayName("Mã bài hát")]
        public int MusicId { get; set; }
        [DisplayName("Tên bài hát")]
        public string MusicName { get; set; }

        [DisplayName("Tên ca sĩ/nghệ sĩ")]
        public string MusicSinger { get; set; }

        [DisplayName("Mã thể loại")]
        public int? CategoryId { get; set; }

        [DisplayName("Tên thể loại")]
        public string CategoryName { get; set; }

        [DisplayName("Mã quốc gia")]
        public int? NationId { get; set; }

        [DisplayName("Tên quốc gia")]
        public string NationName { get; set; }

        [DisplayName("Ngày phát hành")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? MusicDate { get; set; }

        [DisplayName("Ảnh bìa")]
        public HttpPostedFileBase MusicImage { get; set; }
        public string MusicImageFileName { get; set; }

        [DisplayName("Link bài hát")]
        public string MusicLink { get; set; }

        [DisplayName("Lượt thích")]
        public int? MusicLikes { get; set; } = 0;

        [DisplayName("Lượt nghe")]
        public int? MusicListen { get; set; } = 0;
    }
}