using Microsoft.EntityFrameworkCore;
using WebApplication4.Model;
namespace WebApplication4.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration _config { get; set; }
        public AppDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }

        

        public DbSet<User> Users { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<ShiftWithCapacity> ShiftWithCapacity { get; set; }

    }
}
