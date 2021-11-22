using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlantSwap.Models
{
  public class PlantSwapContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Trade> Trades { get; set; }

    public PlantSwapContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}