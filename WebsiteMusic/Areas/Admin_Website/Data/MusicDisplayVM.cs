using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteMusic.Areas.Admin_Website.Data
{
    public class MusicDisplayVM
    {
        [DisplayName("Ảnh bìa")]
        public string MusicImage { get; set; }

        [DisplayName("Tên bài hát")]
        public string MusicName { get; set; }

        [DisplayName("Tên ca sĩ/nghệ sĩ")]
        public string MusicSinger { get; set; }

        [DisplayName("Thể loại")]
        public string CategoryName { get; set; }

        [DisplayName("Ngày phát hành")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? MusicDate { get; set; }

        [DisplayName("Link nhạc")]
        public string MusicLink { get; set; }
    }
}