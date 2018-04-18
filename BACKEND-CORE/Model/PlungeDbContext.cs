using Microsoft.EntityFrameworkCore;

namespace backend.Model
{
  public class PlungeDbContext : DbContext
  {
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppUserRole> Roles { get; set; }

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
