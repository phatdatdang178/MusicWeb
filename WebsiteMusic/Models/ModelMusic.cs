using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebsiteMusic.Models
{
    public partial class ModelMusic : DbContext
    {
        public ModelMusic()
            : base("name=ModelMusic")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Nation> Nations { get; set; }
        public virtual DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
