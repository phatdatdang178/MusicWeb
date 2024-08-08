namespace WebsiteMusic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Banner
    {
        [Key]
        public int banner_id { get; set; }

        [StringLength(255)]
        public string banner_image { get; set; }

        [StringLength(255)]
        public string banner_detail { get; set; }
    }
}
