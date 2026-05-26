using Microsoft.EntityFrameworkCore;
using TelecomAnalyticsAPI.Models;

namespace TelecomAnalyticsAPI.Data
{
    public class AppDbContext : DbContext
    {
      
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

        public DbSet<User> Users { get; set; }
        public DbSet<CdrRecord> CdrRecords { get; set; }

    }
}
