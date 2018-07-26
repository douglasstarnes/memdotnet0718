using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<User> Users { get; set; }
    }
}