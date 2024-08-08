using Microsoft.EntityFrameworkCore;
using WebsiteMusic.Models;
namespace WebsiteMusic.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Taikhoan { get; set; }
        public DbSet<Banner> Anhbia { get; set; }
        public DbSet<Category> Theloai { get; set; }
        public DbSet<Music> Amnhac { get; set; }
        public DbSet<Nation> Quocgia { get; set; }
        public DbSet<Title> Tieude { get; set; }
    }
}
