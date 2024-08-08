namespace WebsiteMusic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Music
    {
        [Key]
        public int music_id { get; set; }

        [StringLength(255)]
        public string music_image { get; set; }

        [StringLength(255)]
        public string music_name { get; set; }

        [StringLength(255)]
        public string music_singer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? music_date { get; set; }

        public int? music_likes { get; set; }

        [StringLength(255)]
        public string music_data { get; set; }

        public int? category_id { get; set; }

        public int? nation_id { get; set; }

        public int? music_listen { get; set; }

        public virtual Category Category { get; set; }

        public virtual Nation Nation { get; set; }
    }
}
