namespace WebsiteMusic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [Key]
        public int account_id { get; set; }

        [StringLength(255)]
        public string account_image { get; set; }

        [StringLength(255)]
        public string account_name { get; set; }

        [StringLength(255)]
        public string account_email { get; set; }

        [StringLength(255)]
        public string account_password { get; set; }

        [StringLength(255)]
        public string account_role { get; set; }

        public string account_likes { get; set; }

        public string account_listmusic { get; set; }
    }
}
