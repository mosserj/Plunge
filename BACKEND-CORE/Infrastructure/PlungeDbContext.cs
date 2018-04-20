using Microsoft.EntityFrameworkCore;
using backend.Core.Model;

namespace backend.Infrastructure
{
  public class PlungeDbContext : DbContext
  {
        public DbSet<Product> _Products { get; set; }
        public DbSet<Category> _Categories { get; set; }
        public DbSet<AppUser> _Users { get; set; }
        public DbSet<AppUserRole> _Roles { get; set; }

        private const string CONN =
                      @"Server=Localhost;
                     Database=PlungePTC;
                     Trusted_Connection=True;
                     MultipleActiveResultSets=true";

        protected override void OnConfiguring(
                DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(CONN);
    }
  }
}
