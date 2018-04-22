using Microsoft.EntityFrameworkCore;
using backend.Core.Model;

namespace backend.Infrastructure
{
  public class PlungeDbContext : DbContext
  {
        public PlungeDbContext(DbContextOptions<PlungeDbContext> dbContextOptions) :
            base(dbContextOptions)
        {
        }

        public DbSet<Product> _Products { get; set; }
        public DbSet<Category> _Categories { get; set; }
        public DbSet<AppUser> _Users { get; set; }
        public DbSet<AppUserRole> _Roles { get; set; }
  }
}
