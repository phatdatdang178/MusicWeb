namespace WebsiteMusic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Title
    {
        [Key]
        public int title_id { get; set; }

        [StringLength(255)]
        public string title_icon { get; set; }

        [StringLength(255)]
        public string title_name { get; set; }

        public int? title_type { get; set; }
    }
}
