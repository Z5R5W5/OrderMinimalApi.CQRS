using Microsoft.EntityFrameworkCore;
namespace OrderMinimalApi.Data
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Order> Orders => Set<Models.Order>();
    }
}
